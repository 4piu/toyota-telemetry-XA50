using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Xa50.J2534
{
    internal static class Program
    {
        private const uint Iso15765 = 6;
        private const uint Can = 5;
        private const uint PassFilter = 1;
        private const uint FlowControlFilter = 3;
        private const uint Iso15765FramePad = 0x40;
        private const uint StartOfMessage = 0x02;
        private const uint ClearRxBuffer = 0x08;
        private const uint BaudRate = 500000;
        private const uint TxCanId = 0x7D2;
        private const uint RxCanId = 0x7DA;

        private const int StatusNoError = 0x00;
        private const int ErrorTimeout = 0x09;
        private const int ErrorBufferEmpty = 0x10;

        private static readonly byte[] TesterPresent = Bytes(0x3E, 0x00);
        private static readonly byte[] DefaultSession = Bytes(0x10, 0x01);
        private static readonly byte[] ExtendedSession = Bytes(0x10, 0x03);
        private static readonly byte[] ClearF301 = Bytes(0x2C, 0x03, 0xF3, 0x01);
        private static readonly byte[] ClearF302 = Bytes(0x2C, 0x03, 0xF3, 0x02);
        private static readonly byte[] DefineF301 = Bytes(
            0x2C, 0x01, 0xF3, 0x01,
            0x10, 0xA0, 0x01, 0x02,
            0x10, 0xA1, 0x01, 0x02,
            0x10, 0x60, 0x01, 0x01,
            0x1F, 0x01, 0x01, 0x01);
        private static readonly byte[] PollF301 = Bytes(0x22, 0xF3, 0x01);
        private static readonly byte[] ReadWheelSpeeds = Bytes(0x22, 0x10, 0x75);
        private static readonly byte[] ReadSteeringAngle = Bytes(0x22, 0x10, 0x90);
        private static readonly byte[] ReadBrakeSwitches = Bytes(0x22, 0x10, 0x43);
        private static readonly byte[] ReadMasterCylinderTorque = Bytes(0x22, 0x10, 0x67);
        private static readonly byte[] ReadRegenerativeTorque = Bytes(0x22, 0x10, 0x69);
        private static readonly byte[] DefineF302Brake = Bytes(
            0x2C, 0x01, 0xF3, 0x02,
            0x10, 0x43, 0x01, 0x02,
            0x10, 0x67, 0x01, 0x02,
            0x10, 0x69, 0x01, 0x04);
        private static readonly byte[] PollF302 = Bytes(0x22, 0xF3, 0x02);

        private static readonly HashSet<string> TransmitAllowlist = new HashSet<string>(StringComparer.Ordinal)
        {
            HexKey(TesterPresent),
            HexKey(DefaultSession),
            HexKey(ExtendedSession),
            HexKey(ClearF301),
            HexKey(ClearF302),
            HexKey(DefineF301),
            HexKey(PollF301),
            HexKey(ReadWheelSpeeds),
            HexKey(ReadSteeringAngle),
            HexKey(ReadBrakeSwitches),
            HexKey(ReadMasterCylinderTorque),
            HexKey(ReadRegenerativeTorque),
            HexKey(DefineF302Brake),
            HexKey(PollF302)
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

                RunSelfChecks(options.DllPath);
                PrintPlan(options);

                if (!options.Live)
                {
                    Console.WriteLine("Dry run only: the adapter was not opened and nothing was transmitted.");
                    Console.WriteLine("Add --live when the vehicle is stationary, IG-ON/READY, and Techstream is closed.");
                    return 0;
                }

                Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs eventArgs)
                {
                    eventArgs.Cancel = true;
                    stopRequested = true;
                    Console.Error.WriteLine("Stop requested; closing the J2534 session...");
                };

                if (options.RawProbe)
                {
                    return RunRawProbe(options);
                }
                return options.RawIsoTp ? RunRawIsoTp(options) : RunLive(options);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("ERROR: " + ex.Message);
                return 1;
            }
        }

        private static int RunLive(Options options)
        {
            uint deviceId = 0;
            uint channelId = 0;
            uint filterId = 0;
            bool deviceOpen = false;
            bool channelOpen = false;
            bool filterOpen = false;

            using (J2534Api api = new J2534Api(options.DllPath))
            {
                try
                {
                    Check(api, api.Open(out deviceId), "PassThruOpen");
                    deviceOpen = true;
                    Console.WriteLine("Adapter opened: device " + deviceId.ToString(CultureInfo.InvariantCulture));

                    string firmware;
                    string dllVersion;
                    string apiVersion;
                    int versionStatus = api.ReadVersion(deviceId, out firmware, out dllVersion, out apiVersion);
                    if (versionStatus == StatusNoError)
                    {
                        Console.WriteLine("Versions: firmware=" + firmware + ", DLL=" + dllVersion + ", API=" + apiVersion);
                    }
                    else
                    {
                        Console.WriteLine("Version query unavailable: " + DescribeStatus(api, versionStatus));
                    }

                    Check(api, api.Connect(deviceId, Iso15765, 0, BaudRate, out channelId), "PassThruConnect");
                    channelOpen = true;
                    Console.WriteLine("Connected ISO15765 at 500000 bit/s: channel " + channelId.ToString(CultureInfo.InvariantCulture));

                    Check(api, api.Ioctl(channelId, ClearRxBuffer), "PassThruIoctl(CLEAR_RX_BUFFER)");

                    PassThruMsg mask = MakeMessage(Iso15765, 0, Bytes(0xFF, 0xFF, 0xFF, 0xFF));
                    PassThruMsg pattern = MakeMessage(Iso15765, 0, CanIdBytes(RxCanId));
                    PassThruMsg flowControl = MakeMessage(Iso15765, Iso15765FramePad, CanIdBytes(TxCanId));
                    Check(
                        api,
                        api.StartMessageFilter(channelId, FlowControlFilter, ref mask, ref pattern, ref flowControl, out filterId),
                        "PassThruStartMsgFilter(FLOW_CONTROL_FILTER)");
                    filterOpen = true;
                    Console.WriteLine("Flow-control filter installed: 0x7DA receive / 0x7D2 transmit");

                    Exchange(api, channelId, TesterPresent, Bytes(0x7E, 0x00), "tester present", options.TimeoutMs, options.Verbose);
                    Exchange(api, channelId, DefaultSession, Bytes(0x50, 0x01), "default session", options.TimeoutMs, options.Verbose);
                    Exchange(api, channelId, ExtendedSession, Bytes(0x50, 0x03), "extended session", options.TimeoutMs, options.Verbose);

                    if (options.PackedBrake)
                    {
                        Exchange(api, channelId, ClearF302, Bytes(0x6C, 0x03, 0xF3, 0x02), "clear F302", options.TimeoutMs, options.Verbose);
                        Exchange(api, channelId, DefineF302Brake, Bytes(0x6C, 0x01, 0xF3, 0x02), "define brake F302", options.TimeoutMs, options.Verbose);
                        RunPackedBrakeProbe(api, channelId, options);
                        return 0;
                    }
                    if (options.MotionProbe)
                    {
                        RunMotionProbe(api, channelId, options);
                        return 0;
                    }
                    if (options.BrakeProbe)
                    {
                        RunBrakeProbe(api, channelId, options);
                        return 0;
                    }

                    Exchange(api, channelId, ClearF301, Bytes(0x6C, 0x03, 0xF3, 0x01), "clear F301", options.TimeoutMs, options.Verbose);
                    Exchange(api, channelId, ClearF302, Bytes(0x6C, 0x03, 0xF3, 0x02), "clear F302", options.TimeoutMs, options.Verbose);
                    Exchange(api, channelId, DefineF301, Bytes(0x6C, 0x01, 0xF3, 0x01), "define F301", options.TimeoutMs, options.Verbose);
                    Console.WriteLine("Captured Hybrid Control setup accepted.");

                    Stopwatch schedule = Stopwatch.StartNew();
                    long nextRequestNotBefore = 0;
                    for (int sample = 1; sample <= options.Samples && !stopRequested; sample++)
                    {
                        WaitUntil(schedule, nextRequestNotBefore);
                        long requestStartedAt = schedule.ElapsedMilliseconds;

                        ExchangeResult result = Exchange(
                            api,
                            channelId,
                            PollF301,
                            Bytes(0x62, 0xF3, 0x01),
                            "poll F301",
                            options.TimeoutMs,
                            options.Verbose);

                        if (result.Payload.Length < 9)
                        {
                            throw new InvalidOperationException(
                                "F301 response was shorter than 9 bytes: " + Hex(result.Payload));
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

                    return 0;
                }
                finally
                {
                    if (filterOpen)
                    {
                        ReportCleanup(api, api.StopMessageFilter(channelId, filterId), "PassThruStopMsgFilter");
                    }
                    if (channelOpen)
                    {
                        ReportCleanup(api, api.Disconnect(channelId), "PassThruDisconnect");
                    }
                    if (deviceOpen)
                    {
                        ReportCleanup(api, api.Close(deviceId), "PassThruClose");
                    }
                    if (deviceOpen || channelOpen || filterOpen)
                    {
                        Console.WriteLine("J2534 session closed.");
                    }
                }
            }
        }

        private static void RunMotionProbe(J2534Api api, uint channelId, Options options)
        {
            Console.WriteLine("Direct motion probe: DID 1075 wheel speeds, DID 1090 steering angle.");
            Stopwatch schedule = Stopwatch.StartNew();
            long nextRequestNotBefore = 0;

            for (int sample = 1; sample <= options.Samples && !stopRequested; sample++)
            {
                WaitUntil(schedule, nextRequestNotBefore);
                long wheelRequestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult wheels = Exchange(
                    api,
                    channelId,
                    ReadWheelSpeeds,
                    Bytes(0x62, 0x10, 0x75),
                    "read wheel speeds",
                    options.TimeoutMs,
                    options.Verbose);
                nextRequestNotBefore = wheelRequestStartedAt + options.PeriodMs;

                if (wheels.Payload.Length < 11)
                {
                    throw new InvalidOperationException(
                        "DID 1075 response was shorter than 11 bytes: " + Hex(wheels.Payload));
                }

                double frontRight = (ReadUInt16BigEndian(wheels.Payload, 3) - 32768) / 100.0;
                double frontLeft = (ReadUInt16BigEndian(wheels.Payload, 5) - 32768) / 100.0;
                double rearRight = (ReadUInt16BigEndian(wheels.Payload, 7) - 32768) / 100.0;
                double rearLeft = (ReadUInt16BigEndian(wheels.Payload, 9) - 32768) / 100.0;

                WaitUntil(schedule, nextRequestNotBefore);
                long steeringRequestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult steering = Exchange(
                    api,
                    channelId,
                    ReadSteeringAngle,
                    Bytes(0x62, 0x10, 0x90),
                    "read steering angle",
                    options.TimeoutMs,
                    options.Verbose);
                nextRequestNotBefore = steeringRequestStartedAt + options.PeriodMs;

                if (steering.Payload.Length < 5)
                {
                    throw new InvalidOperationException(
                        "DID 1090 response was shorter than 5 bytes: " + Hex(steering.Payload));
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
        }

        private static void RunBrakeProbe(J2534Api api, uint channelId, Options options)
        {
            Console.WriteLine("Direct brake probe: DIDs 1043 switches, 1067 master torque, 1069 regenerative torque.");
            Stopwatch schedule = Stopwatch.StartNew();
            long nextRequestNotBefore = 0;

            for (int sample = 1; sample <= options.Samples && !stopRequested; sample++)
            {
                WaitUntil(schedule, nextRequestNotBefore);
                long switchRequestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult switches = Exchange(
                    api,
                    channelId,
                    ReadBrakeSwitches,
                    Bytes(0x62, 0x10, 0x43),
                    "read brake switches",
                    options.TimeoutMs,
                    options.Verbose);
                nextRequestNotBefore = switchRequestStartedAt + options.PeriodMs;
                if (switches.Payload.Length < 5)
                {
                    throw new InvalidOperationException("DID 1043 response was shorter than 5 bytes: " + Hex(switches.Payload));
                }

                WaitUntil(schedule, nextRequestNotBefore);
                long masterRequestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult master = Exchange(
                    api,
                    channelId,
                    ReadMasterCylinderTorque,
                    Bytes(0x62, 0x10, 0x67),
                    "read master cylinder torque",
                    options.TimeoutMs,
                    options.Verbose);
                nextRequestNotBefore = masterRequestStartedAt + options.PeriodMs;
                if (master.Payload.Length < 5)
                {
                    throw new InvalidOperationException("DID 1067 response was shorter than 5 bytes: " + Hex(master.Payload));
                }

                WaitUntil(schedule, nextRequestNotBefore);
                long regenRequestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult regen = Exchange(
                    api,
                    channelId,
                    ReadRegenerativeTorque,
                    Bytes(0x62, 0x10, 0x69),
                    "read regenerative torque",
                    options.TimeoutMs,
                    options.Verbose);
                nextRequestNotBefore = regenRequestStartedAt + options.PeriodMs;
                if (regen.Payload.Length < 7)
                {
                    throw new InvalidOperationException("DID 1069 response was shorter than 7 bytes: " + Hex(regen.Payload));
                }

                int switchWord = ReadUInt16BigEndian(switches.Payload, 3);
                bool brakeCancel = (switchWord & 0x0002) != 0;
                bool stopLight = (switchWord & 0x0001) != 0;
                double masterTorque = DecodeTorque(master.Payload, 3);
                double requestedRegenTorque = DecodeTorque(regen.Payload, 3);
                double executedRegenTorque = DecodeTorque(regen.Payload, 5);

                Console.WriteLine(
                    "{0:HH:mm:ss.fff} sample={1:D4} stop={2,-3} cancel={3,-3} master={4,8:F3} Nm  regen_req={5,8:F3} Nm  regen_exec={6,8:F3} Nm  switch_raw=0x{7:X4}  latency={8}/{9}/{10} ms",
                    DateTime.Now,
                    sample,
                    stopLight ? "ON" : "off",
                    brakeCancel ? "ON" : "off",
                    masterTorque,
                    requestedRegenTorque,
                    executedRegenTorque,
                    switchWord,
                    switches.LatencyMs,
                    master.LatencyMs,
                    regen.LatencyMs);
            }
        }

        private static double DecodeTorque(byte[] payload, int offset)
        {
            return ReadUInt16BigEndian(payload, offset) * 0.125 - 4096.0;
        }

        private static void RunPackedBrakeProbe(J2534Api api, uint channelId, Options options)
        {
            Console.WriteLine("Packed brake probe: dynamic DID F302 (1043[2] + 1067[2] + 1069[4]).");
            Stopwatch schedule = Stopwatch.StartNew();
            long nextRequestNotBefore = 0;

            for (int sample = 1; sample <= options.Samples && !stopRequested; sample++)
            {
                WaitUntil(schedule, nextRequestNotBefore);
                long requestStartedAt = schedule.ElapsedMilliseconds;
                ExchangeResult result = Exchange(
                    api,
                    channelId,
                    PollF302,
                    Bytes(0x62, 0xF3, 0x02),
                    "poll brake F302",
                    options.TimeoutMs,
                    options.Verbose);
                nextRequestNotBefore = requestStartedAt + options.PeriodMs;

                if (result.Payload.Length < 11)
                {
                    throw new InvalidOperationException("F302 response was shorter than 11 bytes: " + Hex(result.Payload));
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
        }

        private static int RunRawProbe(Options options)
        {
            uint deviceId = 0;
            uint channelId = 0;
            uint filterId = 0;
            bool deviceOpen = false;
            bool channelOpen = false;
            bool filterOpen = false;

            using (J2534Api api = new J2534Api(options.DllPath))
            {
                try
                {
                    Check(api, api.Open(out deviceId), "PassThruOpen");
                    deviceOpen = true;
                    Console.WriteLine("Adapter opened for raw-CAN probe: device " + deviceId.ToString(CultureInfo.InvariantCulture));

                    Check(api, api.Connect(deviceId, Can, 0, BaudRate, out channelId), "PassThruConnect(CAN)");
                    channelOpen = true;
                    Check(api, api.Ioctl(channelId, ClearRxBuffer), "PassThruIoctl(CLEAR_RX_BUFFER)");

                    PassThruMsg mask = MakeMessage(Can, 0, Bytes(0xFF, 0xFF, 0xFF, 0xFF));
                    PassThruMsg pattern = MakeMessage(Can, 0, CanIdBytes(RxCanId));
                    Check(
                        api,
                        api.StartPassFilter(channelId, PassFilter, ref mask, ref pattern, out filterId),
                        "PassThruStartMsgFilter(PASS_FILTER)");
                    filterOpen = true;
                    Console.WriteLine("Raw CAN pass filter installed for 0x7DA.");

                    byte[] rawCanPayload = Bytes(0x02, 0x3E, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00);
                    PassThruMsg transmit = MakeMessage(Can, 0, Combine(CanIdBytes(TxCanId), rawCanPayload));
                    uint transmitCount = 1;
                    Stopwatch latency = Stopwatch.StartNew();
                    Check(api, api.WriteMessages(channelId, ref transmit, ref transmitCount, 1000), "PassThruWriteMsgs(raw tester present)");
                    if (transmitCount != 1)
                    {
                        throw new InvalidOperationException("Raw probe sent " + transmitCount + " messages instead of 1.");
                    }
                    Console.WriteLine("TX raw 0x7D2: " + Hex(rawCanPayload));

                    while (latency.ElapsedMilliseconds < options.TimeoutMs && !stopRequested)
                    {
                        PassThruMsg received = EmptyMessage();
                        uint receiveCount = 1;
                        int remaining = options.TimeoutMs - (int)latency.ElapsedMilliseconds;
                        uint readTimeout = (uint)Math.Max(1, Math.Min(remaining, 100));
                        int status = api.ReadMessages(channelId, ref received, ref receiveCount, readTimeout);
                        if (status == ErrorTimeout || status == ErrorBufferEmpty)
                        {
                            continue;
                        }
                        Check(api, status, "PassThruReadMsgs(raw probe)");
                        if (receiveCount == 0 || received.Data == null || received.DataSize < 5 || received.DataSize > 4128)
                        {
                            continue;
                        }

                        byte[] receivedData = Slice(received.Data, 0, (int)received.DataSize);
                        if (ReadCanId(receivedData) != RxCanId)
                        {
                            continue;
                        }
                        byte[] canPayload = Slice(receivedData, 4, receivedData.Length - 4);
                        Console.WriteLine("RX raw 0x7DA: " + Hex(canPayload) + "  latency=" + latency.ElapsedMilliseconds + " ms");

                        if (canPayload.Length >= 3 && canPayload[0] == 0x02 && canPayload[1] == 0x7E && canPayload[2] == 0x00)
                        {
                            Console.WriteLine("Raw-CAN probe succeeded: Hybrid Control answered tester present.");
                            return 0;
                        }
                        throw new InvalidOperationException("Unexpected raw 0x7DA response: " + Hex(canPayload));
                    }

                    throw new TimeoutException("No raw CAN frame from 0x7DA within " + options.TimeoutMs + " ms.");
                }
                finally
                {
                    if (filterOpen)
                    {
                        ReportCleanup(api, api.StopMessageFilter(channelId, filterId), "PassThruStopMsgFilter");
                    }
                    if (channelOpen)
                    {
                        ReportCleanup(api, api.Disconnect(channelId), "PassThruDisconnect");
                    }
                    if (deviceOpen)
                    {
                        ReportCleanup(api, api.Close(deviceId), "PassThruClose");
                    }
                    if (deviceOpen || channelOpen || filterOpen)
                    {
                        Console.WriteLine("J2534 raw-probe session closed.");
                    }
                }
            }
        }

        private static int RunRawIsoTp(Options options)
        {
            uint deviceId = 0;
            uint channelId = 0;
            uint filterId = 0;
            bool deviceOpen = false;
            bool channelOpen = false;
            bool filterOpen = false;

            using (J2534Api api = new J2534Api(options.DllPath))
            {
                try
                {
                    Check(api, api.Open(out deviceId), "PassThruOpen");
                    deviceOpen = true;
                    Console.WriteLine("Adapter opened for raw-CAN ISO-TP: device " + deviceId.ToString(CultureInfo.InvariantCulture));

                    Check(api, api.Connect(deviceId, Can, 0, BaudRate, out channelId), "PassThruConnect(CAN)");
                    channelOpen = true;
                    Check(api, api.Ioctl(channelId, ClearRxBuffer), "PassThruIoctl(CLEAR_RX_BUFFER)");

                    PassThruMsg mask = MakeMessage(Can, 0, Bytes(0xFF, 0xFF, 0xFF, 0xFF));
                    PassThruMsg pattern = MakeMessage(Can, 0, CanIdBytes(RxCanId));
                    Check(
                        api,
                        api.StartPassFilter(channelId, PassFilter, ref mask, ref pattern, out filterId),
                        "PassThruStartMsgFilter(PASS_FILTER)");
                    filterOpen = true;
                    Console.WriteLine("Raw CAN pass filter installed for 0x7DA; application ISO-TP enabled.");

                    RawIsoTpTransport transport = new RawIsoTpTransport(api, channelId, options.TimeoutMs, options.Verbose);
                    transport.Exchange(TesterPresent, Bytes(0x7E, 0x00), "tester present");
                    transport.Exchange(DefaultSession, Bytes(0x50, 0x01), "default session");
                    transport.Exchange(ExtendedSession, Bytes(0x50, 0x03), "extended session");
                    transport.Exchange(ClearF301, Bytes(0x6C, 0x03, 0xF3, 0x01), "clear F301");
                    transport.Exchange(ClearF302, Bytes(0x6C, 0x03, 0xF3, 0x02), "clear F302");
                    transport.Exchange(DefineF301, Bytes(0x6C, 0x01, 0xF3, 0x01), "define F301");
                    Console.WriteLine("Captured Hybrid Control setup accepted over raw-CAN ISO-TP.");

                    Stopwatch schedule = Stopwatch.StartNew();
                    long nextRequestNotBefore = 0;
                    for (int sample = 1; sample <= options.Samples && !stopRequested; sample++)
                    {
                        WaitUntil(schedule, nextRequestNotBefore);
                        long requestStartedAt = schedule.ElapsedMilliseconds;

                        ExchangeResult result = transport.Exchange(PollF301, Bytes(0x62, 0xF3, 0x01), "poll F301");
                        if (result.Payload.Length < 9)
                        {
                            throw new InvalidOperationException("F301 response was shorter than 9 bytes: " + Hex(result.Payload));
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

                    return 0;
                }
                finally
                {
                    if (filterOpen)
                    {
                        ReportCleanup(api, api.StopMessageFilter(channelId, filterId), "PassThruStopMsgFilter");
                    }
                    if (channelOpen)
                    {
                        ReportCleanup(api, api.Disconnect(channelId), "PassThruDisconnect");
                    }
                    if (deviceOpen)
                    {
                        ReportCleanup(api, api.Close(deviceId), "PassThruClose");
                    }
                    if (deviceOpen || channelOpen || filterOpen)
                    {
                        Console.WriteLine("J2534 raw-CAN ISO-TP session closed.");
                    }
                }
            }
        }

        private static ExchangeResult Exchange(
            J2534Api api,
            uint channelId,
            byte[] request,
            byte[] expectedPrefix,
            string label,
            int timeoutMs,
            bool verbose)
        {
            EnforceAllowlist(request);

            byte[] frame = Combine(CanIdBytes(TxCanId), request);
            PassThruMsg message = MakeMessage(Iso15765, Iso15765FramePad, frame);
            uint messageCount = 1;
            Stopwatch latency = Stopwatch.StartNew();
            Check(api, api.WriteMessages(channelId, ref message, ref messageCount, 1000), "PassThruWriteMsgs(" + label + ")");
            if (messageCount != 1)
            {
                throw new InvalidOperationException("PassThruWriteMsgs sent " + messageCount + " messages instead of 1.");
            }

            if (verbose)
            {
                Console.WriteLine("TX " + label + ": " + Hex(request));
            }

            while (latency.ElapsedMilliseconds < timeoutMs && !stopRequested)
            {
                PassThruMsg received = EmptyMessage();
                uint receiveCount = 1;
                int remaining = timeoutMs - (int)latency.ElapsedMilliseconds;
                uint readTimeout = (uint)Math.Max(1, Math.Min(remaining, 100));
                int status = api.ReadMessages(channelId, ref received, ref receiveCount, readTimeout);

                if (status == ErrorTimeout || status == ErrorBufferEmpty)
                {
                    continue;
                }
                Check(api, status, "PassThruReadMsgs(" + label + ")");
                if (receiveCount == 0)
                {
                    continue;
                }

                if (received.DataSize > 4128 || received.Data == null)
                {
                    throw new InvalidOperationException("J2534 returned an invalid message length.");
                }
                if ((received.RxStatus & StartOfMessage) != 0 || received.DataSize <= 4)
                {
                    continue;
                }
                // XHorse MVCI32.dll reports ProtocolId=0 on received messages.
                // Accept only that observed quirk or the standards-conforming value;
                // the exact CAN ID and UDS response are still validated below.
                if (received.ProtocolId != 0 && received.ProtocolId != Iso15765)
                {
                    continue;
                }

                byte[] receivedData = Slice(received.Data, 0, (int)received.DataSize);
                uint canId = ReadCanId(receivedData);
                if (canId != RxCanId)
                {
                    continue;
                }

                byte[] payload = Slice(receivedData, 4, receivedData.Length - 4);
                if (verbose)
                {
                    Console.WriteLine("RX " + label + ": " + Hex(payload));
                }

                if (payload.Length >= 3 && payload[0] == 0x7F && payload[1] == request[0])
                {
                    throw new InvalidOperationException(
                        "ECU negative response to " + label + ": service=0x" + payload[1].ToString("X2") +
                        ", NRC=0x" + payload[2].ToString("X2") + ", payload=" + Hex(payload));
                }

                if (!StartsWith(payload, expectedPrefix))
                {
                    throw new InvalidOperationException(
                        "Unexpected response to " + label + ": " + Hex(payload) +
                        " (expected prefix " + Hex(expectedPrefix) + ")");
                }

                return new ExchangeResult(payload, latency.ElapsedMilliseconds);
            }

            if (stopRequested)
            {
                throw new OperationCanceledException("Stopped by user.");
            }
            throw new TimeoutException("No complete 0x7DA response to " + label + " within " + timeoutMs + " ms.");
        }

        private static void RunSelfChecks(string dllPath)
        {
            if (Environment.Is64BitProcess)
            {
                throw new InvalidOperationException("This client must run as x86 to load the 32-bit MVCI DLL.");
            }
            if (Marshal.SizeOf(typeof(PassThruMsg)) != 4152)
            {
                throw new InvalidOperationException(
                    "Unexpected PASSTHRU_MSG size " + Marshal.SizeOf(typeof(PassThruMsg)) + "; expected 4152 bytes.");
            }
            if (!File.Exists(dllPath))
            {
                throw new FileNotFoundException("J2534 DLL not found.", dllPath);
            }

            ushort machine = ReadPeMachine(dllPath);
            if (machine != 0x014C)
            {
                throw new InvalidOperationException(
                    "J2534 DLL is not PE32/x86 (machine 0x" + machine.ToString("X4") + ").");
            }

            byte[][] required =
            {
                TesterPresent, DefaultSession, ExtendedSession, ClearF301,
                ClearF302, DefineF301, PollF301, ReadWheelSpeeds, ReadSteeringAngle,
                ReadBrakeSwitches, ReadMasterCylinderTorque, ReadRegenerativeTorque,
                DefineF302Brake, PollF302
            };
            foreach (byte[] request in required)
            {
                EnforceAllowlist(request);
            }
        }

        private static void PrintPlan(Options options)
        {
            Console.WriteLine("XA50 Hybrid Control J2534 poller");
            Console.WriteLine("Process: x86; PASSTHRU_MSG: 4152 bytes");
            Console.WriteLine("DLL: " + options.DllPath);
            Console.WriteLine("Transport: ISO15765, 500000 bit/s, 0x7D2 -> 0x7DA");
            string acquisition = options.MotionProbe
                ? "direct motion DIDs 1075/1090"
                : (options.BrakeProbe
                    ? "direct brake DIDs 1043/1067/1069"
                    : (options.PackedBrake ? "packed brake dynamic DID F302" : "dynamic DID F301"));
            Console.WriteLine("Acquisition: " + acquisition);
            Console.WriteLine("Mode: " + (options.Live ? "LIVE" : "DRY RUN") +
                "; samples=" + options.Samples + "; period=" + options.PeriodMs + " ms; timeout=" + options.TimeoutMs + " ms");
        }

        private static void PrintHelp()
        {
            Console.WriteLine("XA50 read-only Hybrid Control J2534 client");
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("  xa50-j2534.exe [--dry-run]");
            Console.WriteLine("  xa50-j2534.exe --live [--samples N] [--period-ms N] [--timeout-ms N] [--verbose]");
            Console.WriteLine("  xa50-j2534.exe --live --raw-probe [--timeout-ms N]");
            Console.WriteLine("  xa50-j2534.exe --live --raw-isotp [--samples N] [--period-ms N] [--verbose]");
            Console.WriteLine("  xa50-j2534.exe --live --motion-probe [--samples N] [--period-ms N] [--verbose]");
            Console.WriteLine("  xa50-j2534.exe --live --brake-probe [--samples N] [--period-ms N] [--verbose]");
            Console.WriteLine("  xa50-j2534.exe --live --packed-brake [--samples N] [--period-ms N] [--verbose]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("  --live          Open the adapter and transmit the captured allowlist.");
            Console.WriteLine("  --dry-run       Validate architecture/configuration without opening the adapter (default).");
            Console.WriteLine("  --raw-probe     Send one raw-CAN tester-present frame to isolate ISO-TP handling.");
            Console.WriteLine("  --raw-isotp     Use application-managed ISO-TP over the adapter's raw CAN mode.");
            Console.WriteLine("  --motion-probe  Direct-read wheel speeds (1075) and steering angle (1090).");
            Console.WriteLine("  --brake-probe   Direct-read stop switch (1043) and torque DIDs (1067/1069).");
            Console.WriteLine("  --packed-brake  Define/poll synchronized brake dynamic DID F302.");
            Console.WriteLine("  --samples N     Poll count, 1-10000 (default 20).");
            Console.WriteLine("  --period-ms N   Minimum request-start interval, 70-5000 ms (default 100).");
            Console.WriteLine("  --timeout-ms N  Per-exchange timeout, 100-10000 ms (default 1000).");
            Console.WriteLine("  --dll PATH      Override the configured XHorse MVCI32.dll path.");
            Console.WriteLine("  --verbose       Print every UDS request and response.");
            Console.WriteLine("  --help          Show this help.");
        }

        private static void WaitUntil(Stopwatch stopwatch, long targetMilliseconds)
        {
            while (!stopRequested)
            {
                long remaining = targetMilliseconds - stopwatch.ElapsedMilliseconds;
                if (remaining <= 0)
                {
                    return;
                }
                Thread.Sleep((int)Math.Min(remaining, 20));
            }
        }

        private static void EnforceAllowlist(byte[] request)
        {
            if (!TransmitAllowlist.Contains(HexKey(request)))
            {
                throw new InvalidOperationException("Blocked non-allowlisted transmit payload: " + Hex(request));
            }
        }

        private static void Check(J2534Api api, int status, string operation)
        {
            if (status != StatusNoError)
            {
                throw new InvalidOperationException(operation + " failed: " + DescribeStatus(api, status));
            }
        }

        private static string DescribeStatus(J2534Api api, int status)
        {
            string description = api.GetLastError();
            return "0x" + status.ToString("X2") + (String.IsNullOrEmpty(description) ? String.Empty : " (" + description + ")");
        }

        private static void ReportCleanup(J2534Api api, int status, string operation)
        {
            if (status != StatusNoError)
            {
                Console.Error.WriteLine("Cleanup warning: " + operation + " failed: " + DescribeStatus(api, status));
            }
        }

        private static PassThruMsg EmptyMessage()
        {
            PassThruMsg message = new PassThruMsg();
            message.Data = new byte[4128];
            return message;
        }

        private static PassThruMsg MakeMessage(uint protocolId, uint flags, byte[] data)
        {
            if (data.Length > 4128)
            {
                throw new ArgumentOutOfRangeException("data");
            }
            PassThruMsg message = EmptyMessage();
            message.ProtocolId = protocolId;
            message.TxFlags = flags;
            message.DataSize = (uint)data.Length;
            Buffer.BlockCopy(data, 0, message.Data, 0, data.Length);
            return message;
        }

        private static byte[] CanIdBytes(uint id)
        {
            return Bytes(
                (byte)((id >> 24) & 0xFF),
                (byte)((id >> 16) & 0xFF),
                (byte)((id >> 8) & 0xFF),
                (byte)(id & 0xFF));
        }

        private static uint ReadCanId(byte[] data)
        {
            return ((uint)data[0] << 24) | ((uint)data[1] << 16) | ((uint)data[2] << 8) | data[3];
        }

        private static int ReadUInt16BigEndian(byte[] data, int offset)
        {
            return (data[offset] << 8) | data[offset + 1];
        }

        private static bool StartsWith(byte[] data, byte[] prefix)
        {
            if (data.Length < prefix.Length)
            {
                return false;
            }
            for (int index = 0; index < prefix.Length; index++)
            {
                if (data[index] != prefix[index])
                {
                    return false;
                }
            }
            return true;
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] result = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
            return result;
        }

        private static byte[] Slice(byte[] source, int offset, int count)
        {
            byte[] result = new byte[count];
            Buffer.BlockCopy(source, offset, result, 0, count);
            return result;
        }

        private static byte[] Bytes(params byte[] values)
        {
            return values;
        }

        private static string Hex(byte[] values)
        {
            StringBuilder builder = new StringBuilder(values.Length * 3);
            for (int index = 0; index < values.Length; index++)
            {
                if (index > 0)
                {
                    builder.Append(' ');
                }
                builder.Append(values[index].ToString("X2", CultureInfo.InvariantCulture));
            }
            return builder.ToString();
        }

        private static string HexKey(byte[] values)
        {
            StringBuilder builder = new StringBuilder(values.Length * 2);
            foreach (byte value in values)
            {
                builder.Append(value.ToString("X2", CultureInfo.InvariantCulture));
            }
            return builder.ToString();
        }

        private static ushort ReadPeMachine(string path)
        {
            using (FileStream stream = File.OpenRead(path))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                if (reader.ReadUInt16() != 0x5A4D)
                {
                    throw new InvalidOperationException("J2534 DLL does not have an MZ header.");
                }
                stream.Position = 0x3C;
                int peOffset = reader.ReadInt32();
                stream.Position = peOffset;
                if (reader.ReadUInt32() != 0x00004550)
                {
                    throw new InvalidOperationException("J2534 DLL does not have a PE header.");
                }
                return reader.ReadUInt16();
            }
        }

        private sealed class RawIsoTpTransport
        {
            private readonly J2534Api api;
            private readonly uint channelId;
            private readonly int timeoutMs;
            private readonly bool verbose;

            internal RawIsoTpTransport(J2534Api api, uint channelId, int timeoutMs, bool verbose)
            {
                this.api = api;
                this.channelId = channelId;
                this.timeoutMs = timeoutMs;
                this.verbose = verbose;
            }

            internal ExchangeResult Exchange(byte[] request, byte[] expectedPrefix, string label)
            {
                EnforceAllowlist(request);
                Stopwatch latency = Stopwatch.StartNew();
                if (verbose)
                {
                    Console.WriteLine("UDS TX " + label + ": " + Hex(request));
                }

                SendPayload(request, label);
                byte[] response = ReceivePayload(label);

                if (verbose)
                {
                    Console.WriteLine("UDS RX " + label + ": " + Hex(response));
                }
                if (response.Length >= 3 && response[0] == 0x7F && response[1] == request[0])
                {
                    throw new InvalidOperationException(
                        "ECU negative response to " + label + ": service=0x" + response[1].ToString("X2") +
                        ", NRC=0x" + response[2].ToString("X2") + ", payload=" + Hex(response));
                }
                if (!StartsWith(response, expectedPrefix))
                {
                    throw new InvalidOperationException(
                        "Unexpected response to " + label + ": " + Hex(response) +
                        " (expected prefix " + Hex(expectedPrefix) + ")");
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
                    WriteCanFrame(consecutiveFrame, label + " CF" + sequence);
                    offset += count;
                    sequence = (sequence + 1) & 0x0F;
                    sentInBlock++;

                    if (flowControl.BlockSize != 0 && sentInBlock >= flowControl.BlockSize && offset < payload.Length)
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
                        throw new InvalidOperationException("Invalid ISO-TP single-frame length in " + Hex(first));
                    }
                    return Slice(first, 1, length);
                }
                if (frameType != 1)
                {
                    throw new InvalidOperationException("Expected ISO-TP response SF/FF, received " + Hex(first));
                }

                int totalLength = ((first[0] & 0x0F) << 8) | first[1];
                if (totalLength <= 6 || totalLength > 4095)
                {
                    throw new InvalidOperationException("Invalid ISO-TP first-frame length in " + Hex(first));
                }

                byte[] payload = new byte[totalLength];
                Buffer.BlockCopy(first, 2, payload, 0, 6);
                int offset = 6;

                byte[] continueFlowControl = new byte[8];
                continueFlowControl[0] = 0x30;
                continueFlowControl[1] = 0x00;
                continueFlowControl[2] = 0x00;
                WriteCanFrame(continueFlowControl, label + " FC");

                int expectedSequence = 1;
                while (offset < totalLength)
                {
                    byte[] consecutive = ReadCanFrame(label + " response CF");
                    if ((consecutive[0] >> 4) != 2 || (consecutive[0] & 0x0F) != expectedSequence)
                    {
                        throw new InvalidOperationException(
                            "ISO-TP consecutive-frame sequence mismatch; expected " + expectedSequence +
                            ", received " + Hex(consecutive));
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
                    throw new InvalidOperationException("Expected ISO-TP flow control, received " + Hex(frame));
                }
                int status = frame[0] & 0x0F;
                if (status != 0)
                {
                    throw new InvalidOperationException(
                        "ISO-TP flow-control status was " + status + " instead of ContinueToSend: " + Hex(frame));
                }
                return new FlowControl(frame[1], frame[2]);
            }

            private byte[] ReadCanFrame(string label)
            {
                Stopwatch wait = Stopwatch.StartNew();
                while (wait.ElapsedMilliseconds < timeoutMs && !stopRequested)
                {
                    PassThruMsg received = EmptyMessage();
                    uint receiveCount = 1;
                    int remaining = timeoutMs - (int)wait.ElapsedMilliseconds;
                    uint readTimeout = (uint)Math.Max(1, Math.Min(remaining, 100));
                    int status = api.ReadMessages(channelId, ref received, ref receiveCount, readTimeout);
                    if (status == ErrorTimeout || status == ErrorBufferEmpty)
                    {
                        continue;
                    }
                    Check(api, status, "PassThruReadMsgs(" + label + ")");
                    // XHorse MVCI32.dll reports ProtocolId=0 on RX even on a CAN channel.
                    if (receiveCount == 0 || (received.ProtocolId != 0 && received.ProtocolId != Can) || received.Data == null ||
                        received.DataSize < 5 || received.DataSize > 4128)
                    {
                        continue;
                    }

                    byte[] receivedData = Slice(received.Data, 0, (int)received.DataSize);
                    if (ReadCanId(receivedData) != RxCanId)
                    {
                        continue;
                    }
                    byte[] canPayload = Slice(receivedData, 4, receivedData.Length - 4);
                    if (canPayload.Length < 8)
                    {
                        throw new InvalidOperationException("Raw CAN frame was shorter than 8 data bytes: " + Hex(canPayload));
                    }
                    if (verbose)
                    {
                        Console.WriteLine("CAN RX 0x7DA " + label + ": " + Hex(canPayload));
                    }
                    return canPayload;
                }

                if (stopRequested)
                {
                    throw new OperationCanceledException("Stopped by user.");
                }
                throw new TimeoutException("No raw CAN frame from 0x7DA for " + label + " within " + timeoutMs + " ms.");
            }

            private void WriteCanFrame(byte[] canPayload, string label)
            {
                if (canPayload.Length != 8)
                {
                    throw new InvalidOperationException("Raw ISO-TP CAN frames must contain exactly 8 data bytes.");
                }
                PassThruMsg message = MakeMessage(Can, 0, Combine(CanIdBytes(TxCanId), canPayload));
                uint messageCount = 1;
                Check(api, api.WriteMessages(channelId, ref message, ref messageCount, 1000), "PassThruWriteMsgs(" + label + ")");
                if (messageCount != 1)
                {
                    throw new InvalidOperationException("Raw ISO-TP write sent " + messageCount + " messages instead of 1.");
                }
                if (verbose)
                {
                    Console.WriteLine("CAN TX 0x7D2 " + label + ": " + Hex(canPayload));
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
                    long targetTicks = (long)Math.Ceiling(targetMicroseconds * Stopwatch.Frequency / 1000000.0);
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

        private sealed class Options
        {
            internal bool Live;
            internal bool ShowHelp;
            internal bool Verbose;
            internal bool RawProbe;
            internal bool RawIsoTp;
            internal bool MotionProbe;
            internal bool BrakeProbe;
            internal bool PackedBrake;
            internal int Samples = 20;
            internal int PeriodMs = 100;
            internal int TimeoutMs = 1000;
            internal string DllPath = @"C:\Program Files (x86)\XHorse Electronics\MVCI Driver for TOYOTA TIS\MVCI32.dll";

            internal static Options Parse(string[] args)
            {
                Options options = new Options();
                for (int index = 0; index < args.Length; index++)
                {
                    string argument = args[index];
                    if (argument == "--live")
                    {
                        options.Live = true;
                    }
                    else if (argument == "--dry-run")
                    {
                        options.Live = false;
                    }
                    else if (argument == "--verbose")
                    {
                        options.Verbose = true;
                    }
                    else if (argument == "--raw-probe")
                    {
                        options.RawProbe = true;
                    }
                    else if (argument == "--raw-isotp")
                    {
                        options.RawIsoTp = true;
                    }
                    else if (argument == "--motion-probe")
                    {
                        options.MotionProbe = true;
                    }
                    else if (argument == "--brake-probe")
                    {
                        options.BrakeProbe = true;
                    }
                    else if (argument == "--packed-brake")
                    {
                        options.PackedBrake = true;
                    }
                    else if (argument == "--help" || argument == "-h" || argument == "/?")
                    {
                        options.ShowHelp = true;
                    }
                    else if (argument == "--samples")
                    {
                        options.Samples = ParseInt(NextValue(args, ref index, argument), argument, 1, 10000);
                    }
                    else if (argument == "--period-ms")
                    {
                        options.PeriodMs = ParseInt(NextValue(args, ref index, argument), argument, 70, 5000);
                    }
                    else if (argument == "--timeout-ms")
                    {
                        options.TimeoutMs = ParseInt(NextValue(args, ref index, argument), argument, 100, 10000);
                    }
                    else if (argument == "--dll")
                    {
                        options.DllPath = Path.GetFullPath(NextValue(args, ref index, argument));
                    }
                    else
                    {
                        throw new ArgumentException("Unknown argument: " + argument);
                    }
                }
                if (options.RawProbe && !options.Live)
                {
                    throw new ArgumentException("--raw-probe requires --live.");
                }
                if (options.RawIsoTp && !options.Live)
                {
                    throw new ArgumentException("--raw-isotp requires --live.");
                }
                if (options.RawProbe && options.RawIsoTp)
                {
                    throw new ArgumentException("--raw-probe and --raw-isotp are mutually exclusive.");
                }
                if (options.MotionProbe && (options.RawProbe || options.RawIsoTp))
                {
                    throw new ArgumentException("--motion-probe currently requires native ISO15765 and cannot be combined with a raw mode.");
                }
                if (options.BrakeProbe && (options.RawProbe || options.RawIsoTp))
                {
                    throw new ArgumentException("--brake-probe currently requires native ISO15765 and cannot be combined with a raw mode.");
                }
                if (options.PackedBrake && (options.RawProbe || options.RawIsoTp))
                {
                    throw new ArgumentException("--packed-brake currently requires native ISO15765 and cannot be combined with a raw mode.");
                }
                int acquisitionModes = (options.MotionProbe ? 1 : 0) + (options.BrakeProbe ? 1 : 0) + (options.PackedBrake ? 1 : 0);
                if (acquisitionModes > 1)
                {
                    throw new ArgumentException("--motion-probe, --brake-probe, and --packed-brake are mutually exclusive.");
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
                    throw new ArgumentException(option + " must be between " + minimum + " and " + maximum + ".");
                }
                return value;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct PassThruMsg
    {
        internal uint ProtocolId;
        internal uint RxStatus;
        internal uint TxFlags;
        internal uint Timestamp;
        internal uint DataSize;
        internal uint ExtraDataIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4128)]
        internal byte[] Data;
    }

    internal sealed class ExchangeResult
    {
        internal readonly byte[] Payload;
        internal readonly long LatencyMs;

        internal ExchangeResult(byte[] payload, long latencyMs)
        {
            Payload = payload;
            LatencyMs = latencyMs;
        }
    }

    internal sealed class J2534Api : IDisposable
    {
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruOpenDelegate(IntPtr name, out uint deviceId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruCloseDelegate(uint deviceId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruConnectDelegate(
            uint deviceId, uint protocolId, uint flags, uint baudRate, out uint channelId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruDisconnectDelegate(uint channelId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruReadMsgsDelegate(
            uint channelId, ref PassThruMsg message, ref uint messageCount, uint timeout);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruWriteMsgsDelegate(
            uint channelId, ref PassThruMsg message, ref uint messageCount, uint timeout);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruStartMsgFilterDelegate(
            uint channelId,
            uint filterType,
            ref PassThruMsg mask,
            ref PassThruMsg pattern,
            ref PassThruMsg flowControl,
            out uint filterId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruStartMsgFilterNoFlowDelegate(
            uint channelId,
            uint filterType,
            ref PassThruMsg mask,
            ref PassThruMsg pattern,
            IntPtr flowControl,
            out uint filterId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruStopMsgFilterDelegate(uint channelId, uint filterId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruReadVersionDelegate(
            uint deviceId,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder firmwareVersion,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder dllVersion,
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder apiVersion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruGetLastErrorDelegate(
            [Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder errorDescription);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int PassThruIoctlDelegate(
            uint channelId, uint ioctlId, IntPtr input, IntPtr output);

        private IntPtr module;
        private readonly PassThruOpenDelegate open;
        private readonly PassThruCloseDelegate close;
        private readonly PassThruConnectDelegate connect;
        private readonly PassThruDisconnectDelegate disconnect;
        private readonly PassThruReadMsgsDelegate readMessages;
        private readonly PassThruWriteMsgsDelegate writeMessages;
        private readonly PassThruStartMsgFilterDelegate startMessageFilter;
        private readonly PassThruStartMsgFilterNoFlowDelegate startMessageFilterNoFlow;
        private readonly PassThruStopMsgFilterDelegate stopMessageFilter;
        private readonly PassThruReadVersionDelegate readVersion;
        private readonly PassThruGetLastErrorDelegate getLastError;
        private readonly PassThruIoctlDelegate ioctl;

        internal J2534Api(string path)
        {
            module = NativeMethods.LoadLibrary(path);
            if (module == IntPtr.Zero)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "LoadLibrary failed for " + path);
            }

            open = Load<PassThruOpenDelegate>("PassThruOpen", 8);
            close = Load<PassThruCloseDelegate>("PassThruClose", 4);
            connect = Load<PassThruConnectDelegate>("PassThruConnect", 20);
            disconnect = Load<PassThruDisconnectDelegate>("PassThruDisconnect", 4);
            readMessages = Load<PassThruReadMsgsDelegate>("PassThruReadMsgs", 16);
            writeMessages = Load<PassThruWriteMsgsDelegate>("PassThruWriteMsgs", 16);
            startMessageFilter = Load<PassThruStartMsgFilterDelegate>("PassThruStartMsgFilter", 28);
            startMessageFilterNoFlow = Load<PassThruStartMsgFilterNoFlowDelegate>("PassThruStartMsgFilter", 28);
            stopMessageFilter = Load<PassThruStopMsgFilterDelegate>("PassThruStopMsgFilter", 8);
            readVersion = Load<PassThruReadVersionDelegate>("PassThruReadVersion", 16);
            getLastError = Load<PassThruGetLastErrorDelegate>("PassThruGetLastError", 4);
            ioctl = Load<PassThruIoctlDelegate>("PassThruIoctl", 16);
        }

        internal int Open(out uint deviceId)
        {
            return open(IntPtr.Zero, out deviceId);
        }

        internal int Close(uint deviceId)
        {
            return close(deviceId);
        }

        internal int Connect(uint deviceId, uint protocolId, uint flags, uint baudRate, out uint channelId)
        {
            return connect(deviceId, protocolId, flags, baudRate, out channelId);
        }

        internal int Disconnect(uint channelId)
        {
            return disconnect(channelId);
        }

        internal int ReadMessages(uint channelId, ref PassThruMsg message, ref uint messageCount, uint timeout)
        {
            return readMessages(channelId, ref message, ref messageCount, timeout);
        }

        internal int WriteMessages(uint channelId, ref PassThruMsg message, ref uint messageCount, uint timeout)
        {
            return writeMessages(channelId, ref message, ref messageCount, timeout);
        }

        internal int StartMessageFilter(
            uint channelId,
            uint filterType,
            ref PassThruMsg mask,
            ref PassThruMsg pattern,
            ref PassThruMsg flowControl,
            out uint filterId)
        {
            return startMessageFilter(channelId, filterType, ref mask, ref pattern, ref flowControl, out filterId);
        }

        internal int StopMessageFilter(uint channelId, uint filterId)
        {
            return stopMessageFilter(channelId, filterId);
        }

        internal int StartPassFilter(
            uint channelId,
            uint filterType,
            ref PassThruMsg mask,
            ref PassThruMsg pattern,
            out uint filterId)
        {
            return startMessageFilterNoFlow(channelId, filterType, ref mask, ref pattern, IntPtr.Zero, out filterId);
        }

        internal int Ioctl(uint channelId, uint ioctlId)
        {
            return ioctl(channelId, ioctlId, IntPtr.Zero, IntPtr.Zero);
        }

        internal int ReadVersion(uint deviceId, out string firmware, out string dll, out string api)
        {
            StringBuilder firmwareBuffer = new StringBuilder(80);
            StringBuilder dllBuffer = new StringBuilder(80);
            StringBuilder apiBuffer = new StringBuilder(80);
            int status = readVersion(deviceId, firmwareBuffer, dllBuffer, apiBuffer);
            firmware = firmwareBuffer.ToString();
            dll = dllBuffer.ToString();
            api = apiBuffer.ToString();
            return status;
        }

        internal string GetLastError()
        {
            try
            {
                StringBuilder buffer = new StringBuilder(256);
                int status = getLastError(buffer);
                return status == 0 ? buffer.ToString().Trim() : String.Empty;
            }
            catch
            {
                return String.Empty;
            }
        }

        public void Dispose()
        {
            if (module != IntPtr.Zero)
            {
                NativeMethods.FreeLibrary(module);
                module = IntPtr.Zero;
            }
        }

        private T Load<T>(string name, int decoratedArgumentBytes) where T : class
        {
            IntPtr address = NativeMethods.GetProcAddress(module, name);
            if (address == IntPtr.Zero)
            {
                address = NativeMethods.GetProcAddress(module, "_" + name + "@" + decoratedArgumentBytes);
            }
            if (address == IntPtr.Zero)
            {
                throw new MissingMethodException("J2534 DLL does not export " + name + ".");
            }
            return (T)(object)Marshal.GetDelegateForFunctionPointer(address, typeof(T));
        }
    }

    internal static class NativeMethods
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr LoadLibrary(string path);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr module, string name);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr module);
    }
}
