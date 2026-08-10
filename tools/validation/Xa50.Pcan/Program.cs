using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Xa50.Pcan
{
    internal static class Program
    {
        private const ushort Channel = 0x51;       // PCAN_USBBUS1
        private const ushort Baud500K = 0x001C;   // PCAN_BAUD_500K
        private const uint TxCanId = 0x7D2;
        private const uint RxCanId = 0x7DA;
        private const uint ObdTxCanId = 0x7DF;
        private const uint ObdRxCanIdFirst = 0x7E8;
        private const uint ObdRxCanIdLast = 0x7EF;
        private const byte StandardMessage = 0x00;
        private const byte ListenOnlyParameter = 0x08;
        private const byte MessageFilterParameter = 0x04;
        private const uint ParameterOff = 0;
        private const uint FilterClose = 0;
        private const uint FilterOpen = 1;

        private const uint ErrorOk = 0x00000;
        private const uint ErrorQueueReceiveEmpty = 0x00020;

        private static readonly byte[] TesterPresentFrame =
            { 0x02, 0x3E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        private static readonly byte[] ObdSupportedPidsFrame =
            { 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        private static readonly byte[] TesterPresent = { 0x3E, 0x00 };
        private static readonly byte[] DefaultSession = { 0x10, 0x01 };
        private static readonly byte[] ExtendedSession = { 0x10, 0x03 };
        private static readonly byte[] ClearF301 = { 0x2C, 0x03, 0xF3, 0x01 };
        private static readonly byte[] ClearF302 = { 0x2C, 0x03, 0xF3, 0x02 };
        private static readonly byte[] DefineF301 =
        {
            0x2C, 0x01, 0xF3, 0x01,
            0x10, 0xA0, 0x01, 0x02,
            0x10, 0xA1, 0x01, 0x02,
            0x10, 0x60, 0x01, 0x01,
            0x1F, 0x01, 0x01, 0x01
        };
        private static readonly byte[] PollF301 = { 0x22, 0xF3, 0x01 };
        private static readonly byte[] ReadWheelSpeeds = { 0x22, 0x10, 0x75 };
        private static readonly byte[] ReadSteeringAngle = { 0x22, 0x10, 0x90 };
        private static readonly byte[] DefineF302Brake =
        {
            0x2C, 0x01, 0xF3, 0x02,
            0x10, 0x43, 0x01, 0x02,
            0x10, 0x67, 0x01, 0x02,
            0x10, 0x69, 0x01, 0x04
        };
        private static readonly byte[] PollF302 = { 0x22, 0xF3, 0x02 };

        private static readonly byte[][] AllAllowedUdsRequests =
        {
            TesterPresent,
            DefaultSession,
            ExtendedSession,
            ClearF301,
            ClearF302,
            DefineF301,
            PollF301,
            ReadWheelSpeeds,
            ReadSteeringAngle,
            DefineF302Brake,
            PollF302
        };

        private static volatile bool stopRequested;

        private static int Main(string[] args)
        {
            try
            {
                Options options = Options.Parse(args);
                if (options.ShowHelp)
                {
                    PrintHelp();
                    return 0;
                }

                RunSelfChecks();
                PrintPlan(options);

                if (!options.Live)
                {
                    Console.WriteLine("Dry run only: PCAN-Basic was not called and nothing was transmitted.");
                    Console.WriteLine("Close PCAN-View, keep the vehicle stationary in Park and IG-ON/READY, then add --live.");
                    return 0;
                }

                RefuseConcurrentPcanView();
                Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs eventArgs)
                {
                    eventArgs.Cancel = true;
                    stopRequested = true;
                    Console.Error.WriteLine("Stop requested; closing the PCAN session...");
                };
                return options.IsTelemetryMode ? RunTelemetry(options) : RunProbe(options);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("ERROR: " + ex.Message);
                return 1;
            }
        }

        private static int RunTelemetry(Options options)
        {
            bool initialized = false;
            try
            {
                uint off = ParameterOff;
                Check(PcanBasic.SetValue(Channel, ListenOnlyParameter, ref off, sizeof(uint)),
                    "disable listen-only preset");
                Check(PcanBasic.Initialize(Channel, Baud500K, 0, 0, 0),
                    "initialize PCAN_USBBUS1 at 500 kbit/s");
                initialized = true;
                Check(PcanBasic.GetStatus(Channel), "initial CAN status");

                uint filterClose = FilterClose;
                Check(PcanBasic.SetValue(Channel, MessageFilterParameter, ref filterClose, sizeof(uint)),
                    "close receive filter");
                Check(PcanBasic.FilterMessages(Channel, RxCanId, RxCanId, StandardMessage),
                    "allow only standard ID 0x7DA");
                DrainReceiveQueue();

                byte[][] modeAllowlist = GetModeAllowlist(options);
                PcanIsoTpTransport transport = new PcanIsoTpTransport(
                    options.TimeoutMs,
                    options.Verbose,
                    modeAllowlist);
                transport.Exchange(TesterPresent, new byte[] { 0x7E, 0x00 }, "tester present");
                transport.Exchange(DefaultSession, new byte[] { 0x50, 0x01 }, "default session");
                transport.Exchange(ExtendedSession, new byte[] { 0x50, 0x03 }, "extended session");

                if (options.Motion)
                {
                    RunMotion(transport, options);
                    Check(PcanBasic.GetStatus(Channel), "final CAN status");
                    return 0;
                }

                if (options.F302)
                {
                    transport.Exchange(ClearF302, new byte[] { 0x6C, 0x03, 0xF3, 0x02 }, "clear F302");
                    transport.Exchange(
                        DefineF302Brake,
                        new byte[] { 0x6C, 0x01, 0xF3, 0x02 },
                        "define brake F302");
                    Console.WriteLine("Captured packed-brake F302 setup accepted over PCAN raw-CAN ISO-TP.");
                    RunF302(transport, options);
                    Check(PcanBasic.GetStatus(Channel), "final CAN status");
                    return 0;
                }

                transport.Exchange(ClearF301, new byte[] { 0x6C, 0x03, 0xF3, 0x01 }, "clear F301");
                transport.Exchange(ClearF302, new byte[] { 0x6C, 0x03, 0xF3, 0x02 }, "clear F302");
                transport.Exchange(DefineF301, new byte[] { 0x6C, 0x01, 0xF3, 0x01 }, "define F301");
                Console.WriteLine("Captured Hybrid Control setup accepted over PCAN raw-CAN ISO-TP.");

                Stopwatch schedule = Stopwatch.StartNew();
                long nextRequestNotBefore = 0;
                for (int sample = 1; sample <= options.Samples && !stopRequested; sample++)
                {
                    WaitUntil(schedule, nextRequestNotBefore);
                    if (stopRequested)
                    {
                        break;
                    }
                    long requestStartedAt = schedule.ElapsedMilliseconds;

                    ExchangeResult result = transport.Exchange(
                        PollF301,
                        new byte[] { 0x62, 0xF3, 0x01 },
                        "poll F301");
                    if (result.Payload.Length < 9)
                    {
                        throw new InvalidOperationException(
                            "F301 response was shorter than 9 bytes: " + Hex(result.Payload, result.Payload.Length));
                    }

                    int mg1 = ReadUInt16BigEndian(result.Payload, 3) - 32768;
                    int mg2 = ReadUInt16BigEndian(result.Payload, 5) - 32768;
                    double accelerator = result.Payload[7] * 0.5;
                    byte auxiliary = result.Payload[8];

                    Console.WriteLine(
                        "{0:HH:mm:ss.fff} sample={1:D4} MG1={2,6} rpm  MG2={3,6} rpm  accel={4,5:F1}%  aux=0x{5:X2}  latency={6,3} ms",
                        DateTime.Now,
                        sample,
                        mg1,
                        mg2,
                        accelerator,
                        auxiliary,
                        result.LatencyMs);

                    nextRequestNotBefore = requestStartedAt + options.PeriodMs;
                }

                if (stopRequested)
                {
                    Console.WriteLine("Sampling stopped by user.");
                }
                Check(PcanBasic.GetStatus(Channel), "final CAN status");
                return 0;
            }
            finally
            {
                if (initialized)
                {
                    uint cleanup = PcanBasic.Uninitialize(Channel);
                    if (cleanup != ErrorOk)
                    {
                        Console.Error.WriteLine("WARNING: PCAN uninitialize failed: " + Describe(cleanup));
                    }
                    else
                    {
                        Console.WriteLine("PCAN raw-CAN ISO-TP session closed.");
                    }
                }
            }
        }

        private static void RunMotion(PcanIsoTpTransport transport, Options options)
        {
            Console.WriteLine("Direct motion mode: DID 1075 wheel speeds, DID 1090 steering angle.");
            Stopwatch schedule = Stopwatch.StartNew();
            long nextRequestNotBefore = 0;

            for (int sample = 1; sample <= options.Samples && !stopRequested; sample++)
            {
                WaitUntil(schedule, nextRequestNotBefore);
                if (stopRequested)
                {
                    break;
                }
                long wheelRequestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult wheels = transport.Exchange(
                    ReadWheelSpeeds,
                    new byte[] { 0x62, 0x10, 0x75 },
                    "read wheel speeds");
                nextRequestNotBefore = wheelRequestStartedAt + options.PeriodMs;

                if (wheels.Payload.Length < 11)
                {
                    throw new InvalidOperationException(
                        "DID 1075 response was shorter than 11 bytes: " +
                        Hex(wheels.Payload, wheels.Payload.Length));
                }
                double frontRight = (ReadUInt16BigEndian(wheels.Payload, 3) - 32768) / 100.0;
                double frontLeft = (ReadUInt16BigEndian(wheels.Payload, 5) - 32768) / 100.0;
                double rearRight = (ReadUInt16BigEndian(wheels.Payload, 7) - 32768) / 100.0;
                double rearLeft = (ReadUInt16BigEndian(wheels.Payload, 9) - 32768) / 100.0;

                WaitUntil(schedule, nextRequestNotBefore);
                if (stopRequested)
                {
                    break;
                }
                long steeringRequestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult steering = transport.Exchange(
                    ReadSteeringAngle,
                    new byte[] { 0x62, 0x10, 0x90 },
                    "read steering angle");
                nextRequestNotBefore = steeringRequestStartedAt + options.PeriodMs;

                if (steering.Payload.Length < 5)
                {
                    throw new InvalidOperationException(
                        "DID 1090 response was shorter than 5 bytes: " +
                        Hex(steering.Payload, steering.Payload.Length));
                }
                double steeringAngle = (ReadUInt16BigEndian(steering.Payload, 3) - 32768) / 10.0;

                Console.WriteLine(
                    "{0:HH:mm:ss.fff} sample={1:D4} wheel_kmh FR={2,7:F2} FL={3,7:F2} RR={4,7:F2} RL={5,7:F2}  steering={6,7:F1} deg  latency={7}/{8} ms",
                    DateTime.Now,
                    sample,
                    frontRight,
                    frontLeft,
                    rearRight,
                    rearLeft,
                    steeringAngle,
                    wheels.LatencyMs,
                    steering.LatencyMs);
            }

            if (stopRequested)
            {
                Console.WriteLine("Motion sampling stopped by user.");
            }
        }

        private static void RunF302(PcanIsoTpTransport transport, Options options)
        {
            Console.WriteLine("Packed brake mode: dynamic DID F302 (1043[2] + 1067[2] + 1069[4]).");
            Stopwatch schedule = Stopwatch.StartNew();
            long nextRequestNotBefore = 0;

            for (int sample = 1; sample <= options.Samples && !stopRequested; sample++)
            {
                WaitUntil(schedule, nextRequestNotBefore);
                if (stopRequested)
                {
                    break;
                }
                long requestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult result = transport.Exchange(
                    PollF302,
                    new byte[] { 0x62, 0xF3, 0x02 },
                    "poll brake F302");
                nextRequestNotBefore = requestStartedAt + options.PeriodMs;

                if (result.Payload.Length < 11)
                {
                    throw new InvalidOperationException(
                        "F302 response was shorter than 11 bytes: " +
                        Hex(result.Payload, result.Payload.Length));
                }

                int switchWord = ReadUInt16BigEndian(result.Payload, 3);
                bool brakeCancel = (switchWord & 0x0002) != 0;
                bool stopLight = (switchWord & 0x0001) != 0;
                double masterTorque = DecodeTorque(result.Payload, 5);
                double requestedRegenTorque = DecodeTorque(result.Payload, 7);
                double executedRegenTorque = DecodeTorque(result.Payload, 9);

                Console.WriteLine(
                    "{0:HH:mm:ss.fff} sample={1:D4} stop={2,-3} cancel={3,-3} master={4,8:F3} Nm  regen_req={5,8:F3} Nm  regen_exec={6,8:F3} Nm  switch_raw=0x{7:X4}  latency={8} ms",
                    DateTime.Now,
                    sample,
                    stopLight ? "ON" : "off",
                    brakeCancel ? "ON" : "off",
                    masterTorque,
                    requestedRegenTorque,
                    executedRegenTorque,
                    switchWord,
                    result.LatencyMs);
            }

            if (stopRequested)
            {
                Console.WriteLine("F302 sampling stopped by user.");
            }
        }

        private static double DecodeTorque(byte[] payload, int offset)
        {
            return ReadUInt16BigEndian(payload, offset) * 0.125 - 4096.0;
        }

        private static byte[][] GetModeAllowlist(Options options)
        {
            if (options.Motion)
            {
                return new byte[][]
                {
                    TesterPresent,
                    DefaultSession,
                    ExtendedSession,
                    ReadWheelSpeeds,
                    ReadSteeringAngle
                };
            }
            if (options.F302)
            {
                return new byte[][]
                {
                    TesterPresent,
                    DefaultSession,
                    ExtendedSession,
                    ClearF302,
                    DefineF302Brake,
                    PollF302
                };
            }
            return new byte[][]
            {
                TesterPresent,
                DefaultSession,
                ExtendedSession,
                ClearF301,
                ClearF302,
                DefineF301,
                PollF301
            };
        }

        private static int RunProbe(Options options)
        {
            bool initialized = false;
            Stopwatch latency = new Stopwatch();
            uint transmitId = options.ObdProbe ? ObdTxCanId : TxCanId;
            uint receiveIdFirst = options.ObdProbe ? ObdRxCanIdFirst : RxCanId;
            uint receiveIdLast = options.ObdProbe ? ObdRxCanIdLast : RxCanId;
            int unexpectedFrames = 0;
            long nextStatusCheckMs = 50;

            try
            {
                uint off = ParameterOff;
                Check(PcanBasic.SetValue(Channel, ListenOnlyParameter, ref off, sizeof(uint)),
                    "disable listen-only preset");

                Check(PcanBasic.Initialize(Channel, Baud500K, 0, 0, 0),
                    "initialize PCAN_USBBUS1 at 500 kbit/s");
                initialized = true;

                Check(PcanBasic.GetStatus(Channel), "initial CAN status");

                if (options.OpenFilter)
                {
                    uint filterOpen = FilterOpen;
                    Check(PcanBasic.SetValue(Channel, MessageFilterParameter, ref filterOpen, sizeof(uint)),
                        "open receive filter");
                }
                else
                {
                    uint filterClose = FilterClose;
                    Check(PcanBasic.SetValue(Channel, MessageFilterParameter, ref filterClose, sizeof(uint)),
                        "close receive filter");
                    Check(PcanBasic.FilterMessages(Channel, receiveIdFirst, receiveIdLast, StandardMessage),
                        options.ObdProbe
                            ? "allow only standard IDs 0x7E8 through 0x7EF"
                            : "allow only standard ID 0x7DA");
                }

                DrainReceiveQueue();

                PcanMessage transmit = CreateTransmitFrame(options);
                EnforceTransmitAllowlist(transmit, options);

                latency.Start();
                Check(PcanBasic.Write(Channel, ref transmit), "write the single tester-present frame");
                Console.WriteLine("TX 0x" + transmitId.ToString("X3", CultureInfo.InvariantCulture) +
                    "  " + Hex(transmit.Data, transmit.Length));

                while (latency.ElapsedMilliseconds < options.TimeoutMs)
                {
                    PcanMessage received;
                    PcanTimestamp timestamp;
                    uint status = PcanBasic.Read(Channel, out received, out timestamp);

                    if (status == ErrorQueueReceiveEmpty)
                    {
                        if (latency.ElapsedMilliseconds >= nextStatusCheckMs)
                        {
                            Check(PcanBasic.GetStatus(Channel), "CAN status while awaiting response");
                            nextStatusCheckMs += 50;
                        }
                        Thread.Sleep(1);
                        continue;
                    }

                    Check(status, "read response");
                    if (received.Id < receiveIdFirst || received.Id > receiveIdLast)
                    {
                        unexpectedFrames++;
                        if (unexpectedFrames <= 25)
                        {
                            Console.WriteLine("RX other 0x" +
                                received.Id.ToString("X", CultureInfo.InvariantCulture) + "  " +
                                Hex(received.Data, received.Length) + "  type=0x" +
                                received.MessageType.ToString("X2", CultureInfo.InvariantCulture));
                        }
                        continue;
                    }
                    ValidateResponseFrame(received, options);
                    latency.Stop();

                    Console.WriteLine("RX 0x" + received.Id.ToString("X3", CultureInfo.InvariantCulture) +
                        "  " + Hex(received.Data, received.Length) +
                        "  latency=" + latency.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture) + " ms");
                    if (options.ObdProbe)
                    {
                        uint supportedPids =
                            ((uint)received.Data[3] << 24) |
                            ((uint)received.Data[4] << 16) |
                            ((uint)received.Data[5] << 8) |
                            received.Data[6];
                        Console.WriteLine("Mode-01 PID support bitmap: 0x" +
                            supportedPids.ToString("X8", CultureInfo.InvariantCulture));
                        Console.WriteLine("SUCCESS: standardized OBD-over-CAN transmit and receive both work through PCAN-USB.");
                    }
                    else
                    {
                        Console.WriteLine("SUCCESS: the DLC3 gateway routed the Hybrid Control diagnostic exchange through PCAN-USB.");
                    }
                    Check(PcanBasic.GetStatus(Channel), "final CAN status");
                    return 0;
                }

                latency.Stop();
                uint finalStatus = PcanBasic.GetStatus(Channel);
                if (finalStatus != ErrorOk)
                {
                    throw new InvalidOperationException(
                        "No response in the expected CAN-ID range within " +
                        options.TimeoutMs.ToString(CultureInfo.InvariantCulture) +
                        " ms; CAN status is " + Describe(finalStatus) + ".");
                }

                throw new TimeoutException(
                    "No response in the expected CAN-ID range within " +
                    options.TimeoutMs.ToString(CultureInfo.InvariantCulture) +
                    " ms. The single 0x" + transmitId.ToString("X3", CultureInfo.InvariantCulture) +
                    " frame was sent, but no matching reply reached the adapter." +
                    (options.OpenFilter
                        ? " Other received frames: " + unexpectedFrames.ToString(CultureInfo.InvariantCulture) + "."
                        : String.Empty));
            }
            finally
            {
                if (initialized)
                {
                    uint cleanup = PcanBasic.Uninitialize(Channel);
                    if (cleanup != ErrorOk)
                    {
                        Console.Error.WriteLine("WARNING: PCAN uninitialize failed: " + Describe(cleanup));
                    }
                    else
                    {
                        Console.WriteLine("PCAN channel closed.");
                    }
                }
            }
        }

        private static PcanMessage CreateTransmitFrame(Options options)
        {
            byte[] payload = options.ObdProbe ? ObdSupportedPidsFrame : TesterPresentFrame;
            return new PcanMessage
            {
                Id = options.ObdProbe ? ObdTxCanId : TxCanId,
                MessageType = StandardMessage,
                Length = 8,
                Data = (byte[])payload.Clone()
            };
        }

        private static void EnforceTransmitAllowlist(PcanMessage message, Options options)
        {
            byte[] expectedPayload = options.ObdProbe ? ObdSupportedPidsFrame : TesterPresentFrame;
            uint expectedId = options.ObdProbe ? ObdTxCanId : TxCanId;
            if (message.Id != expectedId ||
                message.MessageType != StandardMessage ||
                message.Length != expectedPayload.Length ||
                message.Data == null ||
                message.Data.Length != expectedPayload.Length)
            {
                throw new InvalidOperationException("Transmit frame failed the PCAN probe allowlist.");
            }

            for (int i = 0; i < expectedPayload.Length; i++)
            {
                if (message.Data[i] != expectedPayload[i])
                {
                    throw new InvalidOperationException("Transmit payload failed the PCAN probe allowlist.");
                }
            }
        }

        private static void ValidateResponseFrame(PcanMessage message, Options options)
        {
            uint firstId = options.ObdProbe ? ObdRxCanIdFirst : RxCanId;
            uint lastId = options.ObdProbe ? ObdRxCanIdLast : RxCanId;
            if (message.Id < firstId || message.Id > lastId)
            {
                throw new InvalidOperationException(
                    "Receive filter returned unexpected CAN ID 0x" + message.Id.ToString("X", CultureInfo.InvariantCulture) + ".");
            }
            if (message.MessageType != StandardMessage)
            {
                throw new InvalidOperationException(
                    "0x7DA was not a standard data frame; message type=0x" +
                    message.MessageType.ToString("X2", CultureInfo.InvariantCulture) + ".");
            }
            if (message.Data == null || message.Data.Length < message.Length)
            {
                throw new InvalidOperationException("Response frame data was shorter than its CAN data length.");
            }

            if (options.ObdProbe)
            {
                if (message.Length < 7 ||
                    message.Data[0] != 0x06 || message.Data[1] != 0x41 || message.Data[2] != 0x00)
                {
                    throw new InvalidOperationException(
                        "Unexpected OBD response: " + Hex(message.Data, message.Length));
                }
            }
            else
            {
                if (message.Length != 8 ||
                    message.Data[0] != 0x02 || message.Data[1] != 0x7E || message.Data[2] != 0x00)
                {
                    throw new InvalidOperationException(
                        "Unexpected 0x7DA response to tester present: " + Hex(message.Data, message.Length));
                }
            }
        }

        private static void DrainReceiveQueue()
        {
            while (true)
            {
                PcanMessage ignoredMessage;
                PcanTimestamp ignoredTimestamp;
                uint status = PcanBasic.Read(Channel, out ignoredMessage, out ignoredTimestamp);
                if (status == ErrorQueueReceiveEmpty)
                {
                    return;
                }
                Check(status, "drain receive queue");
            }
        }

        private sealed class PcanIsoTpTransport
        {
            private readonly int timeoutMs;
            private readonly bool verbose;
            private readonly byte[][] allowedRequests;

            internal PcanIsoTpTransport(int timeoutMs, bool verbose, byte[][] allowedRequests)
            {
                this.timeoutMs = timeoutMs;
                this.verbose = verbose;
                this.allowedRequests = allowedRequests;
            }

            internal ExchangeResult Exchange(byte[] request, byte[] expectedPrefix, string label)
            {
                EnforceUdsAllowlist(request, allowedRequests);
                Stopwatch latency = Stopwatch.StartNew();
                if (verbose)
                {
                    Console.WriteLine("UDS TX " + label + ": " + Hex(request, request.Length));
                }

                SendPayload(request, label);
                byte[] response = ReceivePayload(label);

                if (verbose)
                {
                    Console.WriteLine("UDS RX " + label + ": " + Hex(response, response.Length));
                }
                if (response.Length >= 3 && response[0] == 0x7F && response[1] == request[0])
                {
                    throw new InvalidOperationException(
                        "ECU negative response to " + label + ": service=0x" +
                        response[1].ToString("X2", CultureInfo.InvariantCulture) +
                        ", NRC=0x" + response[2].ToString("X2", CultureInfo.InvariantCulture) +
                        ", payload=" + Hex(response, response.Length));
                }
                if (!StartsWith(response, expectedPrefix))
                {
                    throw new InvalidOperationException(
                        "Unexpected response to " + label + ": " + Hex(response, response.Length) +
                        " (expected prefix " + Hex(expectedPrefix, expectedPrefix.Length) + ")");
                }
                return new ExchangeResult(response, latency.ElapsedMilliseconds);
            }

            private void SendPayload(byte[] payload, string label)
            {
                if (payload.Length <= 7)
                {
                    byte[] singleFrame = new byte[8];
                    singleFrame[0] = (byte)payload.Length;
                    Buffer.BlockCopy(payload, 0, singleFrame, 1, payload.Length);
                    WriteCanFrame(singleFrame, label + " SF");
                    return;
                }
                if (payload.Length > 4095)
                {
                    throw new InvalidOperationException("ISO-TP payload exceeds the Classical-CAN 12-bit length limit.");
                }

                byte[] firstFrame = new byte[8];
                firstFrame[0] = (byte)(0x10 | ((payload.Length >> 8) & 0x0F));
                firstFrame[1] = (byte)(payload.Length & 0xFF);
                Buffer.BlockCopy(payload, 0, firstFrame, 2, 6);
                WriteCanFrame(firstFrame, label + " FF");

                FlowControl flowControl = ReadFlowControl(label);
                int offset = 6;
                int sequence = 1;
                int sentInBlock = 0;
                while (offset < payload.Length)
                {
                    DelaySeparationTime(flowControl.SeparationTime);
                    byte[] consecutiveFrame = new byte[8];
                    consecutiveFrame[0] = (byte)(0x20 | (sequence & 0x0F));
                    int count = Math.Min(7, payload.Length - offset);
                    Buffer.BlockCopy(payload, offset, consecutiveFrame, 1, count);
                    WriteCanFrame(consecutiveFrame, label + " CF" + sequence.ToString(CultureInfo.InvariantCulture));
                    offset += count;
                    sequence = (sequence + 1) & 0x0F;
                    sentInBlock++;

                    if (flowControl.BlockSize != 0 &&
                        sentInBlock >= flowControl.BlockSize && offset < payload.Length)
                    {
                        flowControl = ReadFlowControl(label);
                        sentInBlock = 0;
                    }
                }
            }

            private byte[] ReceivePayload(string label)
            {
                byte[] first = ReadCanFrame(label + " response");
                int frameType = first[0] >> 4;
                if (frameType == 0)
                {
                    int length = first[0] & 0x0F;
                    if (length <= 0 || length > 7)
                    {
                        throw new InvalidOperationException(
                            "Invalid ISO-TP single-frame length in " + Hex(first, first.Length));
                    }
                    return Slice(first, 1, length);
                }
                if (frameType != 1)
                {
                    throw new InvalidOperationException(
                        "Expected ISO-TP response SF/FF, received " + Hex(first, first.Length));
                }

                int totalLength = ((first[0] & 0x0F) << 8) | first[1];
                if (totalLength <= 6 || totalLength > 4095)
                {
                    throw new InvalidOperationException(
                        "Invalid ISO-TP first-frame length in " + Hex(first, first.Length));
                }

                byte[] payload = new byte[totalLength];
                Buffer.BlockCopy(first, 2, payload, 0, 6);
                int offset = 6;

                byte[] continueFlowControl = new byte[8];
                continueFlowControl[0] = 0x30;
                WriteCanFrame(continueFlowControl, label + " FC");

                int expectedSequence = 1;
                while (offset < totalLength)
                {
                    byte[] consecutive = ReadCanFrame(label + " response CF");
                    if ((consecutive[0] >> 4) != 2 ||
                        (consecutive[0] & 0x0F) != expectedSequence)
                    {
                        throw new InvalidOperationException(
                            "ISO-TP consecutive-frame sequence mismatch; expected " +
                            expectedSequence.ToString(CultureInfo.InvariantCulture) +
                            ", received " + Hex(consecutive, consecutive.Length));
                    }
                    int count = Math.Min(7, totalLength - offset);
                    Buffer.BlockCopy(consecutive, 1, payload, offset, count);
                    offset += count;
                    expectedSequence = (expectedSequence + 1) & 0x0F;
                }
                return payload;
            }

            private FlowControl ReadFlowControl(string label)
            {
                byte[] frame = ReadCanFrame(label + " flow control");
                if ((frame[0] >> 4) != 3)
                {
                    throw new InvalidOperationException(
                        "Expected ISO-TP flow control, received " + Hex(frame, frame.Length));
                }
                int status = frame[0] & 0x0F;
                if (status != 0)
                {
                    throw new InvalidOperationException(
                        "ISO-TP flow-control status was " +
                        status.ToString(CultureInfo.InvariantCulture) +
                        " instead of ContinueToSend: " + Hex(frame, frame.Length));
                }
                return new FlowControl(frame[1], frame[2]);
            }

            private byte[] ReadCanFrame(string label)
            {
                Stopwatch wait = Stopwatch.StartNew();
                long nextStatusCheckMs = 50;
                while (wait.ElapsedMilliseconds < timeoutMs && !stopRequested)
                {
                    PcanMessage received;
                    PcanTimestamp timestamp;
                    uint status = PcanBasic.Read(Channel, out received, out timestamp);
                    if (status == ErrorQueueReceiveEmpty)
                    {
                        if (wait.ElapsedMilliseconds >= nextStatusCheckMs)
                        {
                            Check(PcanBasic.GetStatus(Channel), "CAN status while awaiting " + label);
                            nextStatusCheckMs += 50;
                        }
                        Thread.Sleep(1);
                        continue;
                    }
                    Check(status, "read " + label);
                    if (received.Id != RxCanId || received.MessageType != StandardMessage)
                    {
                        throw new InvalidOperationException(
                            "Unexpected raw CAN frame while awaiting " + label +
                            ": ID=0x" + received.Id.ToString("X", CultureInfo.InvariantCulture) +
                            ", type=0x" + received.MessageType.ToString("X2", CultureInfo.InvariantCulture) + ".");
                    }
                    if (received.Length != 8 || received.Data == null || received.Data.Length < 8)
                    {
                        throw new InvalidOperationException(
                            "Raw CAN frame from 0x7DA was not eight bytes while awaiting " + label + ".");
                    }
                    byte[] frame = Slice(received.Data, 0, 8);
                    if (verbose)
                    {
                        Console.WriteLine("CAN RX 0x7DA " + label + ": " + Hex(frame, frame.Length));
                    }
                    return frame;
                }

                if (stopRequested)
                {
                    throw new OperationCanceledException("Stopped by user.");
                }
                throw new TimeoutException(
                    "No raw CAN frame from 0x7DA for " + label + " within " +
                    timeoutMs.ToString(CultureInfo.InvariantCulture) + " ms.");
            }

            private void WriteCanFrame(byte[] data, string label)
            {
                EnforceIsoTpCanFrameAllowlist(data, allowedRequests);
                PcanMessage message = new PcanMessage
                {
                    Id = TxCanId,
                    MessageType = StandardMessage,
                    Length = 8,
                    Data = (byte[])data.Clone()
                };
                Check(PcanBasic.Write(Channel, ref message), "write " + label);
                if (verbose)
                {
                    Console.WriteLine("CAN TX 0x7D2 " + label + ": " + Hex(data, data.Length));
                }
            }

            private static void DelaySeparationTime(byte separationTime)
            {
                if (separationTime <= 0x7F)
                {
                    if (separationTime > 0)
                    {
                        Thread.Sleep(separationTime);
                    }
                    return;
                }
                if (separationTime >= 0xF1 && separationTime <= 0xF9)
                {
                    double targetMicroseconds = (separationTime - 0xF0) * 100.0;
                    long targetTicks = (long)Math.Ceiling(
                        targetMicroseconds * Stopwatch.Frequency / 1000000.0);
                    Stopwatch delay = Stopwatch.StartNew();
                    while (delay.ElapsedTicks < targetTicks)
                    {
                        Thread.SpinWait(20);
                    }
                }
            }

            private sealed class FlowControl
            {
                internal readonly byte BlockSize;
                internal readonly byte SeparationTime;

                internal FlowControl(byte blockSize, byte separationTime)
                {
                    BlockSize = blockSize;
                    SeparationTime = separationTime;
                }
            }
        }

        private static void EnforceUdsAllowlist(byte[] request, byte[][] allowedRequests)
        {
            foreach (byte[] allowed in allowedRequests)
            {
                if (BytesEqual(request, allowed))
                {
                    return;
                }
            }
            throw new InvalidOperationException(
                "UDS request failed the selected telemetry-mode allowlist: " + Hex(request, request.Length));
        }

        private static void AssertUdsRejected(byte[] request, byte[][] allowedRequests, string label)
        {
            try
            {
                EnforceUdsAllowlist(request, allowedRequests);
            }
            catch (InvalidOperationException)
            {
                return;
            }
            throw new InvalidOperationException("Per-mode UDS allowlist self-check failed: " + label + ".");
        }

        private static void EnforceIsoTpCanFrameAllowlist(byte[] frame, byte[][] allowedRequests)
        {
            if (frame == null || frame.Length != 8)
            {
                throw new InvalidOperationException("Raw ISO-TP CAN frames must contain exactly eight bytes.");
            }

            byte[] flowControl = new byte[8];
            flowControl[0] = 0x30;
            if (BytesEqual(frame, flowControl))
            {
                return;
            }

            foreach (byte[] request in allowedRequests)
            {
                foreach (byte[] allowedFrame in BuildIsoTpFrames(request))
                {
                    if (BytesEqual(frame, allowedFrame))
                    {
                        return;
                    }
                }
            }
            throw new InvalidOperationException(
                "Raw CAN frame failed the selected telemetry-mode allowlist: " + Hex(frame, frame.Length));
        }

        private static IEnumerable<byte[]> BuildIsoTpFrames(byte[] payload)
        {
            if (payload.Length <= 7)
            {
                byte[] singleFrame = new byte[8];
                singleFrame[0] = (byte)payload.Length;
                Buffer.BlockCopy(payload, 0, singleFrame, 1, payload.Length);
                yield return singleFrame;
                yield break;
            }

            byte[] firstFrame = new byte[8];
            firstFrame[0] = (byte)(0x10 | ((payload.Length >> 8) & 0x0F));
            firstFrame[1] = (byte)(payload.Length & 0xFF);
            Buffer.BlockCopy(payload, 0, firstFrame, 2, 6);
            yield return firstFrame;

            int offset = 6;
            int sequence = 1;
            while (offset < payload.Length)
            {
                byte[] consecutiveFrame = new byte[8];
                consecutiveFrame[0] = (byte)(0x20 | (sequence & 0x0F));
                int count = Math.Min(7, payload.Length - offset);
                Buffer.BlockCopy(payload, offset, consecutiveFrame, 1, count);
                yield return consecutiveFrame;
                offset += count;
                sequence = (sequence + 1) & 0x0F;
            }
        }

        private static void RefuseConcurrentPcanView()
        {
            Process[] processes = Process.GetProcessesByName("PcanView");
            try
            {
                if (processes.Length != 0)
                {
                    throw new InvalidOperationException(
                        "PCAN-View is running. Close it completely before using --live so there is only one CAN client.");
                }
            }
            finally
            {
                foreach (Process process in processes)
                {
                    process.Dispose();
                }
            }
        }

        private static void RunSelfChecks()
        {
            if (!Environment.Is64BitOperatingSystem)
            {
                throw new PlatformNotSupportedException("The installed PCAN-Basic path is expected on 64-bit Windows.");
            }

            string dllPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "PCANBasic.dll");
            if (!File.Exists(dllPath))
            {
                throw new FileNotFoundException("The x64 PCAN-Basic DLL was not found.", dllPath);
            }

            Options hybridOptions = new Options();
            PcanMessage layoutProbe = CreateTransmitFrame(hybridOptions);
            EnforceTransmitAllowlist(layoutProbe, hybridOptions);
            Options obdOptions = new Options { ObdProbe = true };
            EnforceTransmitAllowlist(CreateTransmitFrame(obdOptions), obdOptions);
            foreach (byte[] request in AllAllowedUdsRequests)
            {
                EnforceUdsAllowlist(request, AllAllowedUdsRequests);
                foreach (byte[] frame in BuildIsoTpFrames(request))
                {
                    EnforceIsoTpCanFrameAllowlist(frame, AllAllowedUdsRequests);
                }
            }
            byte[] flowControl = new byte[8];
            flowControl[0] = 0x30;
            EnforceIsoTpCanFrameAllowlist(flowControl, AllAllowedUdsRequests);
            List<byte[]> definitionFrames = new List<byte[]>(BuildIsoTpFrames(DefineF301));
            if (definitionFrames.Count != 3 ||
                !BytesEqual(definitionFrames[0], new byte[] { 0x10, 0x14, 0x2C, 0x01, 0xF3, 0x01, 0x10, 0xA0 }) ||
                !BytesEqual(definitionFrames[1], new byte[] { 0x21, 0x01, 0x02, 0x10, 0xA1, 0x01, 0x02, 0x10 }) ||
                !BytesEqual(definitionFrames[2], new byte[] { 0x22, 0x60, 0x01, 0x01, 0x1F, 0x01, 0x01, 0x01 }))
            {
                throw new InvalidOperationException("F301 ISO-TP fragmentation failed its byte-exact self-check.");
            }
            List<byte[]> pollFrames = new List<byte[]>(BuildIsoTpFrames(PollF301));
            if (pollFrames.Count != 1 ||
                !BytesEqual(pollFrames[0], new byte[] { 0x03, 0x22, 0xF3, 0x01, 0x00, 0x00, 0x00, 0x00 }))
            {
                throw new InvalidOperationException("F301 poll framing failed its byte-exact self-check.");
            }
            List<byte[]> brakeDefinitionFrames = new List<byte[]>(BuildIsoTpFrames(DefineF302Brake));
            if (brakeDefinitionFrames.Count != 3 ||
                !BytesEqual(brakeDefinitionFrames[0], new byte[] { 0x10, 0x10, 0x2C, 0x01, 0xF3, 0x02, 0x10, 0x43 }) ||
                !BytesEqual(brakeDefinitionFrames[1], new byte[] { 0x21, 0x01, 0x02, 0x10, 0x67, 0x01, 0x02, 0x10 }) ||
                !BytesEqual(brakeDefinitionFrames[2], new byte[] { 0x22, 0x69, 0x01, 0x04, 0x00, 0x00, 0x00, 0x00 }))
            {
                throw new InvalidOperationException("F302 ISO-TP fragmentation failed its byte-exact self-check.");
            }
            List<byte[]> brakePollFrames = new List<byte[]>(BuildIsoTpFrames(PollF302));
            if (brakePollFrames.Count != 1 ||
                !BytesEqual(brakePollFrames[0], new byte[] { 0x03, 0x22, 0xF3, 0x02, 0x00, 0x00, 0x00, 0x00 }))
            {
                throw new InvalidOperationException("F302 poll framing failed its byte-exact self-check.");
            }
            List<byte[]> wheelFrames = new List<byte[]>(BuildIsoTpFrames(ReadWheelSpeeds));
            List<byte[]> steeringFrames = new List<byte[]>(BuildIsoTpFrames(ReadSteeringAngle));
            if (wheelFrames.Count != 1 || steeringFrames.Count != 1 ||
                !BytesEqual(wheelFrames[0], new byte[] { 0x03, 0x22, 0x10, 0x75, 0x00, 0x00, 0x00, 0x00 }) ||
                !BytesEqual(steeringFrames[0], new byte[] { 0x03, 0x22, 0x10, 0x90, 0x00, 0x00, 0x00, 0x00 }))
            {
                throw new InvalidOperationException("Motion DID framing failed its byte-exact self-check.");
            }
            AssertUdsRejected(PollF301, GetModeAllowlist(new Options { Motion = true }), "motion/F301 isolation");
            AssertUdsRejected(PollF302, GetModeAllowlist(new Options { Motion = true }), "motion/F302 isolation");
            AssertUdsRejected(ReadWheelSpeeds, GetModeAllowlist(new Options { F302 = true }), "F302/motion isolation");
            AssertUdsRejected(PollF301, GetModeAllowlist(new Options { F302 = true }), "F302/F301 isolation");
            AssertUdsRejected(PollF302, GetModeAllowlist(new Options { F301 = true }), "F301/F302 isolation");
            int expectedSize = IntPtr.Size == 8 ? 16 : 16;
            if (Marshal.SizeOf(typeof(PcanMessage)) != expectedSize)
            {
                throw new InvalidOperationException(
                    "Unexpected PCAN message structure size: " +
                    Marshal.SizeOf(typeof(PcanMessage)).ToString(CultureInfo.InvariantCulture) + ".");
            }
        }

        private static void PrintPlan(Options options)
        {
            string title = "XA50 single-frame PCAN gateway probe";
            if (options.F301)
            {
                title = "XA50 PCAN raw-CAN ISO-TP F301 poller";
            }
            else if (options.Motion)
            {
                title = "XA50 PCAN wheel-speed and steering poller";
            }
            else if (options.F302)
            {
                title = "XA50 PCAN packed-brake F302 poller";
            }
            else if (options.ObdProbe)
            {
                title = "XA50 standardized OBD-over-CAN probe";
            }
            Console.WriteLine(title);
            Console.WriteLine("Channel:   PCAN_USBBUS1, Classical CAN, 500000 bit/s");
            if (options.IsTelemetryMode)
            {
                Console.WriteLine("Filter:    standard ID 0x7DA only");
                Console.WriteLine("Transport: application-managed ISO-TP, 0x7D2 -> 0x7DA");
                if (options.F301)
                {
                    Console.WriteLine("Setup:     captured sessions plus dynamic DID F301 definition");
                    Console.WriteLine("Poll:      22 F3 01");
                }
                else if (options.Motion)
                {
                    Console.WriteLine("Setup:     captured default and extended sessions");
                    Console.WriteLine("Poll:      alternating 22 10 75 / 22 10 90");
                }
                else
                {
                    Console.WriteLine("Setup:     captured sessions plus dynamic DID F302 definition");
                    Console.WriteLine("Poll:      22 F3 02");
                }
                Console.WriteLine("Schedule:  " +
                    options.Samples.ToString(CultureInfo.InvariantCulture) + " samples, request period >= " +
                    options.PeriodMs.ToString(CultureInfo.InvariantCulture) + " ms");
            }
            else if (options.ObdProbe)
            {
                Console.WriteLine(options.OpenFilter
                    ? "Filter:    open (expected IDs 0x7E8 through 0x7EF)"
                    : "Filter:    standard IDs 0x7E8 through 0x7EF only");
                Console.WriteLine("TX once:   0x7DF  02 01 00 00 00 00 00 00");
                Console.WriteLine("Expected:  0x7E8-0x7EF beginning 06 41 00");
            }
            else
            {
                Console.WriteLine(options.OpenFilter
                    ? "Filter:    open (expected ID 0x7DA)"
                    : "Filter:    standard ID 0x7DA only");
                Console.WriteLine("TX once:   0x7D2  02 3E 00 00 00 00 00 00");
                Console.WriteLine("Expected:  0x7DA  02 7E 00 00 00 00 00 00");
            }
            Console.WriteLine("Timeout:   " + options.TimeoutMs.ToString(CultureInfo.InvariantCulture) + " ms");
            Console.WriteLine("Mode:      " + (options.Live
                ? (options.IsTelemetryMode
                    ? "LIVE - " + options.TelemetryModeName + " allowlist only"
                    : "LIVE - exactly one allowlisted write")
                : "DRY RUN"));
        }

        private static void Check(uint status, string operation)
        {
            if (status != ErrorOk)
            {
                throw new InvalidOperationException(operation + " failed: " + Describe(status));
            }
        }

        private static string Describe(uint status)
        {
            StringBuilder text = new StringBuilder(256);
            uint result = PcanBasic.GetErrorText(status, 0x09, text);
            if (result == ErrorOk && text.Length != 0)
            {
                return text + " (0x" + status.ToString("X", CultureInfo.InvariantCulture) + ")";
            }
            return "PCAN status 0x" + status.ToString("X", CultureInfo.InvariantCulture);
        }

        private static string Hex(byte[] data, int length)
        {
            if (data == null)
            {
                return "<null>";
            }
            int count = Math.Min(length, data.Length);
            StringBuilder text = new StringBuilder(count * 3);
            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                {
                    text.Append(' ');
                }
                text.Append(data[i].ToString("X2", CultureInfo.InvariantCulture));
            }
            return text.ToString();
        }

        private static bool BytesEqual(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length)
            {
                return false;
            }
            for (int i = 0; i < left.Length; i++)
            {
                if (left[i] != right[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool StartsWith(byte[] data, byte[] prefix)
        {
            if (data == null || prefix == null || data.Length < prefix.Length)
            {
                return false;
            }
            for (int i = 0; i < prefix.Length; i++)
            {
                if (data[i] != prefix[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static byte[] Slice(byte[] source, int offset, int count)
        {
            byte[] result = new byte[count];
            Buffer.BlockCopy(source, offset, result, 0, count);
            return result;
        }

        private static int ReadUInt16BigEndian(byte[] data, int offset)
        {
            if (data == null || offset < 0 || offset + 1 >= data.Length)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            return (data[offset] << 8) | data[offset + 1];
        }

        private static void WaitUntil(Stopwatch schedule, long targetMilliseconds)
        {
            while (!stopRequested)
            {
                long remaining = targetMilliseconds - schedule.ElapsedMilliseconds;
                if (remaining <= 0)
                {
                    return;
                }
                Thread.Sleep((int)Math.Min(remaining, 10));
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("Usage: xa50-pcan.exe [--dry-run] [--live] [mode] [options]");
            Console.WriteLine();
            Console.WriteLine("Without --live the program performs only local checks and prints the exact planned frame.");
            Console.WriteLine("--live sends the tester-present frame once and waits only for the validated 0x7DA reply.");
            Console.WriteLine("--obd-probe instead sends one standardized Mode-01/PID-00 support query at 0x7DF.");
            Console.WriteLine("--f301 runs the captured Hybrid Control setup and polls MG1/MG2/accelerator.");
            Console.WriteLine("--motion alternates direct wheel-speed and steering-angle reads.");
            Console.WriteLine("--f302 defines and polls synchronized brake/regen dynamic DID F302.");
            Console.WriteLine("--samples N and --period-ms N configure telemetry sampling (defaults: 5 and 100).");
            Console.WriteLine("--verbose prints every UDS payload and raw ISO-TP CAN frame.");
            Console.WriteLine("--open-filter receives all CAN IDs locally and reports unrelated frames; it does not alter transmission.");
        }

        private sealed class Options
        {
            internal bool Live;
            internal bool ObdProbe;
            internal bool F301;
            internal bool Motion;
            internal bool F302;
            internal bool OpenFilter;
            internal bool Verbose;
            internal bool ShowHelp;
            internal int TimeoutMs = 1500;
            internal int Samples = 5;
            internal int PeriodMs = 100;

            internal bool IsTelemetryMode
            {
                get { return F301 || Motion || F302; }
            }

            internal string TelemetryModeName
            {
                get { return F301 ? "F301" : (Motion ? "motion" : "F302"); }
            }

            internal static Options Parse(string[] args)
            {
                Options options = new Options();
                for (int i = 0; i < args.Length; i++)
                {
                    string argument = args[i];
                    if (argument == "--live")
                    {
                        options.Live = true;
                    }
                    else if (argument == "--dry-run")
                    {
                        options.Live = false;
                    }
                    else if (argument == "--obd-probe")
                    {
                        options.ObdProbe = true;
                    }
                    else if (argument == "--f301")
                    {
                        options.F301 = true;
                    }
                    else if (argument == "--motion")
                    {
                        options.Motion = true;
                    }
                    else if (argument == "--f302")
                    {
                        options.F302 = true;
                    }
                    else if (argument == "--verbose")
                    {
                        options.Verbose = true;
                    }
                    else if (argument == "--open-filter")
                    {
                        options.OpenFilter = true;
                    }
                    else if (argument == "--help" || argument == "-h" || argument == "/?")
                    {
                        options.ShowHelp = true;
                    }
                    else if (argument == "--timeout-ms")
                    {
                        if (++i >= args.Length ||
                            !int.TryParse(args[i], NumberStyles.None, CultureInfo.InvariantCulture, out options.TimeoutMs) ||
                            options.TimeoutMs < 100 || options.TimeoutMs > 10000)
                        {
                            throw new ArgumentException("--timeout-ms must be an integer from 100 through 10000.");
                        }
                    }
                    else if (argument == "--samples")
                    {
                        options.Samples = ParseInt(NextValue(args, ref i, argument), argument, 1, 10000);
                    }
                    else if (argument == "--period-ms")
                    {
                        options.PeriodMs = ParseInt(NextValue(args, ref i, argument), argument, 70, 5000);
                    }
                    else
                    {
                        throw new ArgumentException("Unknown argument: " + argument);
                    }
                }
                int selectedModes =
                    (options.ObdProbe ? 1 : 0) +
                    (options.F301 ? 1 : 0) +
                    (options.Motion ? 1 : 0) +
                    (options.F302 ? 1 : 0);
                if (selectedModes > 1)
                {
                    throw new ArgumentException("--obd-probe, --f301, --motion, and --f302 are mutually exclusive.");
                }
                if (options.IsTelemetryMode && options.OpenFilter)
                {
                    throw new ArgumentException(
                        "Telemetry modes require the strict 0x7DA filter and cannot use --open-filter.");
                }
                return options;
            }

            private static string NextValue(string[] args, ref int index, string option)
            {
                index++;
                if (index >= args.Length)
                {
                    throw new ArgumentException("Missing value after " + option + ".");
                }
                return args[index];
            }

            private static int ParseInt(string text, string option, int minimum, int maximum)
            {
                int value;
                if (!Int32.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out value) ||
                    value < minimum || value > maximum)
                {
                    throw new ArgumentException(
                        option + " must be between " +
                        minimum.ToString(CultureInfo.InvariantCulture) + " and " +
                        maximum.ToString(CultureInfo.InvariantCulture) + ".");
                }
                return value;
            }
        }

        private sealed class ExchangeResult
        {
            internal readonly byte[] Payload;
            internal readonly long LatencyMs;

            internal ExchangeResult(byte[] payload, long latencyMs)
            {
                Payload = payload;
                LatencyMs = latencyMs;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PcanMessage
    {
        internal uint Id;
        [MarshalAs(UnmanagedType.U1)]
        internal byte MessageType;
        internal byte Length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        internal byte[] Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct PcanTimestamp
    {
        internal uint Milliseconds;
        internal ushort MillisecondsOverflow;
        internal ushort Microseconds;
    }

    internal static class PcanBasic
    {
        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Initialize")]
        internal static extern uint Initialize(
            [MarshalAs(UnmanagedType.U2)] ushort channel,
            [MarshalAs(UnmanagedType.U2)] ushort bitRate,
            byte deprecated1,
            uint deprecated2,
            ushort deprecated3);

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Uninitialize")]
        internal static extern uint Uninitialize([MarshalAs(UnmanagedType.U2)] ushort channel);

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_GetStatus")]
        internal static extern uint GetStatus([MarshalAs(UnmanagedType.U2)] ushort channel);

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Read")]
        internal static extern uint Read(
            [MarshalAs(UnmanagedType.U2)] ushort channel,
            out PcanMessage message,
            out PcanTimestamp timestamp);

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_Write")]
        internal static extern uint Write(
            [MarshalAs(UnmanagedType.U2)] ushort channel,
            ref PcanMessage message);

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_FilterMessages")]
        internal static extern uint FilterMessages(
            [MarshalAs(UnmanagedType.U2)] ushort channel,
            uint fromId,
            uint toId,
            [MarshalAs(UnmanagedType.U1)] byte mode);

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_SetValue")]
        internal static extern uint SetValue(
            [MarshalAs(UnmanagedType.U2)] ushort channel,
            [MarshalAs(UnmanagedType.U1)] byte parameter,
            ref uint value,
            uint valueLength);

        [DllImport("PCANBasic.dll", EntryPoint = "CAN_GetErrorText")]
        internal static extern uint GetErrorText(
            [MarshalAs(UnmanagedType.U4)] uint error,
            ushort language,
            StringBuilder text);
    }
}
