
using LinqToStdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keysight.Tap;

namespace TapPlugin.ResultListeners
{
    public static class STDFHelper
    {
        private static IConvertible ToConvertible(BitArray val)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(val.Count.ToString());

            for (int i = 0; i < val.Count; i++)
                if (val[i])
                    sb.Append(string.Format(",{0}", i));

            return sb.ToString();
        }

        private static IConvertible JoinArray<T>(T[] val)
        {
            if (val == null)
                return "";
            else
                return string.Join(",", val.Select(x => Convert.ToString(x, System.Globalization.CultureInfo.InvariantCulture)));
        }

        private static string DeNull(string t)
        {
            if (t == null)
                return "";
            else
                return t;
        }

        //public static TestStepResultType FarType = new TestStepResultType { Name = "STDF Far", DimensionTitles = new List<string> { "CpuType", "StdfVersion" } };
        //public static TestStepResultType AtrType = new TestStepResultType { Name = "STDF Atr", DimensionTitles = new List<string> { "ModifiedTime", "CommandLine" } };
        //public static TestStepResultType MirType = new TestStepResultType { Name = "STDF Mir", DimensionTitles = new List<string> { "SetupTime", "StartTime", "StationNumber", "ModeCode", "RetestCode", "ProtectionCode", "BurnInTime", "CommandModeCode", "LotId", "PartType", "NodeName", "TesterType", "JobName", "JobRevision", "SublotId", "OperatorName", "ExecType", "ExecVersion", "TestCode", "TestTemperature", "UserText", "AuxiliaryFile", "PackageType", "FamilyId", "DateCode", "FacilityId", "FloorId", "ProcessId", "OperationFrequency", "SpecificationName", "SpecificationVersion", "FlowId", "SetupId", "DesignRevision", "EngineeringId", "RomCode", "SerialNumber", "SupervisorName" } };
        //public static TestStepResultType MrrType = new TestStepResultType { Name = "STDF Mrr", DimensionTitles = new List<string> { "FinishTime", "DispositionCode", "UserDescription", "ExecDescription" } };
        //public static TestStepResultType PcrType = new TestStepResultType { Name = "STDF Pcr", DimensionTitles = new List<string> { "HeadNumber", "SiteNumber", "PartCount", "RetestCount", "AbortCount", "GoodCount", "FunctionalCount" } };
        //public static TestStepResultType HbrType = new TestStepResultType { Name = "STDF Hbr", DimensionTitles = new List<string> { "HeadNumber", "SiteNumber", "BinNumber", "BinCount", "BinPassFail", "BinName" } };
        //public static TestStepResultType SbrType = new TestStepResultType { Name = "STDF Sbr", DimensionTitles = new List<string> { "HeadNumber", "SiteNumber", "BinNumber", "BinCount", "BinPassFail", "BinName" } };
        //public static TestStepResultType PmrType = new TestStepResultType { Name = "STDF Pmr", DimensionTitles = new List<string> { "Index", "ChannelType", "ChannelName", "PhysicalName", "LogicalName", "HeadNumber", "SiteNumber" } };
        //public static TestStepResultType PgrType = new TestStepResultType { Name = "STDF Pgr", DimensionTitles = new List<string> { "GroupIndex", "GroupName", "PinIndexes" } };
        //public static TestStepResultType RdrType = new TestStepResultType { Name = "STDF Rdr", DimensionTitles = new List<string> { "RetestBins" } };
        //public static TestStepResultType SdrType = new TestStepResultType { Name = "STDF Sdr", DimensionTitles = new List<string> { "HeadNumber", "SiteGroup", "SiteNumbers", "HandlerType", "HandlerId", "CardType", "CardId", "LoadboardType", "LoadboardId", "DibType", "DibId", "CableType", "CableId", "ContactorType", "ContactorId", "LaserType", "LaserId", "ExtraType", "ExtraId" } };
        //public static TestStepResultType WirType = new TestStepResultType { Name = "STDF Wir", DimensionTitles = new List<string> { "HeadNumber", "SiteGroup", "StartTime", "WaferId" } };
        //public static TestStepResultType WrrType = new TestStepResultType { Name = "STDF Wrr", DimensionTitles = new List<string> { "HeadNumber", "SiteGroup", "FinishTime", "PartCount", "RetestCount", "AbortCount", "GoodCount", "FunctionalCount", "WaferId", "FabWaferId", "FrameId", "MaskId", "UserDescription", "ExecDescription" } };
        //public static TestStepResultType WcrType = new TestStepResultType { Name = "STDF Wcr", DimensionTitles = new List<string> { "WaferSize", "DieHeight", "DieWidth", "Units", "Flat", "CenterX", "CenterY", "PositiveX", "PositiveY" } };
        //public static TestStepResultType PirType = new TestStepResultType { Name = "STDF Pir", DimensionTitles = new List<string> { "HeadNumber", "SiteNumber" } };
        //public static TestStepResultType PrrType = new TestStepResultType { Name = "STDF Prr", DimensionTitles = new List<string> { "HeadNumber", "SiteNumber", "PartFlag", "TestCount", "HardBin", "SoftBin", "XCoordinate", "YCoordinate", "TestTime", "PartId", "PartText", "PartFix", "SupersedesPartId", "SupersedesCoords", "AbnormalTest", "Failed" } };
        //public static TestStepResultType TsrType = new TestStepResultType { Name = "STDF Tsr", DimensionTitles = new List<string> { "HeadNumber", "SiteNumber", "TestType", "TestNumber", "ExecutedCount", "FailedCount", "AlarmCount", "TestName", "SequencerName", "TestLabel", "TestTime", "TestMin", "TestMax", "TestSum", "TestSumOfSquares" } };
        //public static TestStepResultType PtrType = new TestStepResultType { Name = "STDF Ptr", DimensionTitles = new List<string> { "TestNumber", "HeadNumber", "SiteNumber", "TestFlags", "ParametricFlags", "Result", "TestText", "AlarmId", "OptionalFlags", "ResultScalingExponent", "LowLimitScalingExponent", "HighLimitScalingExponent", "LowLimit", "HighLimit", "Units", "ResultFormatString", "LowLimitFormatString", "HighLimitFormatString", "LowSpecLimit", "HighSpecLimit" } };
        //public static TestStepResultType MprType = new TestStepResultType { Name = "STDF Mpr", DimensionTitles = new List<string> { "TestNumber", "HeadNumber", "SiteNumber", "TestFlags", "ParametricFlags", "PinStates", "Results", "TestText", "AlarmId", "OptionalFlags", "ResultScalingExponent", "LowLimitScalingExponent", "HighLimitScalingExponent", "LowLimit", "HighLimit", "StartingCondition", "ConditionIncrement", "PinIndexes", "Units", "IncrementUnits", "ResultFormatString", "LowLimitFormatString", "HighLimitFormatString", "LowSpecLimit", "HighSpecLimit" } };
        //public static TestStepResultType FtrType = new TestStepResultType { Name = "STDF Ftr", DimensionTitles = new List<string> { "TestNumber", "HeadNumber", "SiteNumber", "TestFlags", "CycleCount", "RelativeVectorAddress", "RepeatCount", "FailingPinCount", "XFailureAddress", "YFailureAddress", "VectorOffset", "ReturnIndexes", "ReturnStates", "ProgrammedIndexes", "ProgrammedStates", "FailingPinBitfield", "VectorName", "TimeSet", "OpCode", "TestText", "AlarmId", "ProgrammedText", "ResultText", "PatternGeneratorNumber", "SpinMap" } };
        //public static TestStepResultType EpsType = new TestStepResultType { Name = "STDF Eps", DimensionTitles = new List<string> { } };
        //public static TestStepResultType DtrType = new TestStepResultType { Name = "STDF Dtr", DimensionTitles = new List<string> { "Text" } };

        public static void AddFar(this ResultSource results, Byte CpuType, Byte StdfVersion)
        {
            results.Publish("STDF Far", new List<string> { "CpuType", "StdfVersion" },
                new IConvertible[] { CpuType, StdfVersion});
        }

        public static void AddAtr(this ResultSource results, String CommandLine, DateTime? ModifiedTime = null)
        {
            results.Publish("STDF Atr", new List<string> { "ModifiedTime", "CommandLine" },
                new IConvertible[] { ModifiedTime, DeNull(CommandLine) });
        }

        public static void AddMir(this ResultSource results, Byte StationNumber, String ModeCode, String RetestCode, String ProtectionCode, String CommandModeCode, DateTime? SetupTime = null, DateTime? StartTime = null, UInt16? BurnInTime = null, String LotId = null, String PartType = null, String NodeName = null, String TesterType = null, String JobName = null, String JobRevision = null, String SublotId = null, String OperatorName = null, String ExecType = null, String ExecVersion = null, String TestCode = null, String TestTemperature = null, String UserText = null, String AuxiliaryFile = null, String PackageType = null, String FamilyId = null, String DateCode = null, String FacilityId = null, String FloorId = null, String ProcessId = null, String OperationFrequency = null, String SpecificationName = null, String SpecificationVersion = null, String FlowId = null, String SetupId = null, String DesignRevision = null, String EngineeringId = null, String RomCode = null, String SerialNumber = null, String SupervisorName = null)
        {
            results.Publish("STDF Mir", new List<string> { "SetupTime", "StartTime", "StationNumber", "ModeCode", "RetestCode", "ProtectionCode", "BurnInTime", 
                "CommandModeCode", "LotId", "PartType", "NodeName", "TesterType", "JobName", "JobRevision", "SublotId", "OperatorName", "ExecType", "ExecVersion", 
                "TestCode", "TestTemperature", "UserText", "AuxiliaryFile", "PackageType", "FamilyId", "DateCode", "FacilityId", "FloorId", "ProcessId", "OperationFrequency", 
                "SpecificationName", "SpecificationVersion", "FlowId", "SetupId", "DesignRevision", "EngineeringId", "RomCode", "SerialNumber", "SupervisorName" },
                new IConvertible[] { SetupTime, StartTime, StationNumber, DeNull(ModeCode), DeNull(RetestCode), DeNull(ProtectionCode), BurnInTime, 
                    DeNull(CommandModeCode), DeNull(LotId), DeNull(PartType), DeNull(NodeName), DeNull(TesterType), DeNull(JobName), DeNull(JobRevision), DeNull(SublotId), DeNull(OperatorName), DeNull(ExecType), DeNull(ExecVersion), 
                    DeNull(TestCode), DeNull(TestTemperature), DeNull(UserText), DeNull(AuxiliaryFile), DeNull(PackageType), DeNull(FamilyId), DeNull(DateCode), DeNull(FacilityId), DeNull(FloorId), DeNull(ProcessId), DeNull(OperationFrequency), 
                    DeNull(SpecificationName), DeNull(SpecificationVersion), DeNull(FlowId), DeNull(SetupId), DeNull(DesignRevision), DeNull(EngineeringId), DeNull(RomCode), DeNull(SerialNumber), DeNull(SupervisorName)});
        }

        public static void AddMrr(this ResultSource results, String DispositionCode, DateTime? FinishTime = null, String UserDescription = null, String ExecDescription = null)
        {
            results.Publish("STDF Mrr", new List<string> { "FinishTime", "DispositionCode", "UserDescription", "ExecDescription" },
                new IConvertible[] { FinishTime, DeNull(DispositionCode), DeNull(UserDescription), DeNull(ExecDescription) });
        }

        public static void AddPcr(this ResultSource results, UInt32 PartCount, Byte? HeadNumber = null, Byte? SiteNumber = null, UInt32? RetestCount = null, UInt32? AbortCount = null, UInt32? GoodCount = null, UInt32? FunctionalCount = null)
        {
            results.Publish("STDF Pcr", new List<string> { "HeadNumber", "SiteNumber", "PartCount", "RetestCount", "AbortCount", "GoodCount", "FunctionalCount" },
                new IConvertible[] { HeadNumber, SiteNumber, PartCount, RetestCount, AbortCount, GoodCount, FunctionalCount });
        }

        public static void AddHbr(this ResultSource results, UInt16 BinNumber, UInt32 BinCount, String BinPassFail, Byte? HeadNumber = null, Byte? SiteNumber = null, String BinName = null)
        {
            results.Publish("STDF Hbr", new List<string> { "HeadNumber", "SiteNumber", "BinNumber", "BinCount", "BinPassFail", "BinName" },
                new IConvertible[] { (IConvertible)HeadNumber, (IConvertible)SiteNumber, (IConvertible)BinNumber, (IConvertible)BinCount, DeNull(BinPassFail), DeNull(BinName) });
        }

        public static void AddSbr(this ResultSource results, UInt16 BinNumber, UInt32 BinCount, String BinPassFail, Byte? HeadNumber = null, Byte? SiteNumber = null, String BinName = null)
        {
            results.Publish("STDF Sbr", new List<string> { "HeadNumber", "SiteNumber", "BinNumber", "BinCount", "BinPassFail", "BinName" },
                new IConvertible[] { (IConvertible)HeadNumber, (IConvertible)SiteNumber, (IConvertible)BinNumber, (IConvertible)BinCount, DeNull(BinPassFail), DeNull(BinName) });
        }

        public static void AddPmr(this ResultSource results, UInt16 Index, UInt16? ChannelType = null, String ChannelName = null, String PhysicalName = null, String LogicalName = null, Byte? HeadNumber = null, Byte? SiteNumber = null)
        {
            results.Publish("STDF Pmr", new List<string> { "Index", "ChannelType", "ChannelName", "PhysicalName", "LogicalName", "HeadNumber", "SiteNumber" },
                new IConvertible[] { (IConvertible)Index, (IConvertible)ChannelType, DeNull(ChannelName), DeNull(PhysicalName), DeNull(LogicalName), (IConvertible)HeadNumber, (IConvertible)SiteNumber} );
        }

        public static void AddPgr(this ResultSource results, UInt16 GroupIndex, String GroupName = null, UInt16[] PinIndexes = null)
        {
            results.Publish("STDF Pgr", new List<string> { "GroupIndex", "GroupName", "PinIndexes" },
                new IConvertible[] { (IConvertible)GroupIndex, DeNull(GroupName), JoinArray<UInt16>(PinIndexes) });
        }

        public static void AddRdr(this ResultSource results, UInt16[] RetestBins = null)
        {
            results.Publish("STDF Rdr", new List<string> { "RetestBins" },
                new IConvertible[] { JoinArray<UInt16>(RetestBins) });
        }

        public static void AddSdr(this ResultSource results, Byte? HeadNumber = null, Byte? SiteGroup = null, Byte[] SiteNumbers = null, String HandlerType = null, String HandlerId = null, String CardType = null, String CardId = null, String LoadboardType = null, String LoadboardId = null, String DibType = null, String DibId = null, String CableType = null, String CableId = null, String ContactorType = null, String ContactorId = null, String LaserType = null, String LaserId = null, String ExtraType = null, String ExtraId = null)
        {
            results.Publish("STDF Sdr", new List<string> { "HeadNumber", "SiteGroup", "SiteNumbers", "HandlerType", "HandlerId", "CardType", 
                "CardId", "LoadboardType", "LoadboardId", "DibType", "DibId", "CableType", "CableId", "ContactorType", "ContactorId", "LaserType", "LaserId", "ExtraType", "ExtraId" },
                new IConvertible[] { (IConvertible)HeadNumber, (IConvertible)SiteGroup, JoinArray<Byte>(SiteNumbers), DeNull(HandlerType), DeNull(HandlerId), DeNull(CardType), DeNull(CardId), 
                    DeNull(LoadboardType), DeNull(LoadboardId), DeNull(DibType), DeNull(DibId), DeNull(CableType), DeNull(CableId), DeNull(ContactorType), DeNull(ContactorId), DeNull(LaserType), DeNull(LaserId), DeNull(ExtraType), DeNull(ExtraId) });
        }

        public static void AddWir(this ResultSource results, Byte? HeadNumber = null, Byte? SiteGroup = null, DateTime? StartTime = null, String WaferId = null)
        {
            results.Publish("STDF Wir", new List<string> { "HeadNumber", "SiteGroup", "StartTime", "WaferId" },
                new IConvertible[] { (IConvertible)HeadNumber, (IConvertible)SiteGroup, (IConvertible)StartTime, DeNull(WaferId) });
        }

        public static void AddWrr(this ResultSource results, UInt32 PartCount, Byte? HeadNumber = null, Byte? SiteGroup = null, DateTime? FinishTime = null, UInt32? RetestCount = null, UInt32? AbortCount = null, UInt32? GoodCount = null, UInt32? FunctionalCount = null, String WaferId = null, String FabWaferId = null, String FrameId = null, String MaskId = null, String UserDescription = null, String ExecDescription = null)
        {
            results.Publish("STDF Wrr", new List<string> { "HeadNumber", "SiteGroup", "FinishTime", "PartCount", "RetestCount", "AbortCount", "GoodCount", "FunctionalCount", "WaferId", "FabWaferId", "FrameId", "MaskId", "UserDescription", "ExecDescription" },
                new IConvertible[] { (IConvertible)HeadNumber, (IConvertible)SiteGroup, (IConvertible)FinishTime, (IConvertible)PartCount, (IConvertible)RetestCount, (IConvertible)AbortCount, (IConvertible)GoodCount, (IConvertible)FunctionalCount, DeNull(WaferId), DeNull(FabWaferId), DeNull(FrameId), DeNull(MaskId), DeNull(UserDescription), DeNull(ExecDescription) });
        }

        public static void AddWcr(this ResultSource results, String Flat, String PositiveX, String PositiveY, Single? WaferSize = null, Single? DieHeight = null, Single? DieWidth = null, Byte? Units = null, Int16? CenterX = null, Int16? CenterY = null)
        {
            results.Publish("STDF Wcr", new List<string> { "WaferSize", "DieHeight", "DieWidth", "Units", "Flat", "CenterX", "CenterY", "PositiveX", "PositiveY" },
                new IConvertible[] { (IConvertible)WaferSize, (IConvertible)DieHeight, (IConvertible)DieWidth, (IConvertible)Units, DeNull(Flat), (IConvertible)CenterX, (IConvertible)CenterY, DeNull(PositiveX), DeNull(PositiveY) });
        }

        public static void AddPir(this ResultSource results, Byte? HeadNumber = null, Byte? SiteNumber = null)
        {
            results.Publish("STDF Pir", new List<string> { "HeadNumber", "SiteNumber" },
                new IConvertible[] { (IConvertible)HeadNumber, (IConvertible)SiteNumber });
        }

        public static void AddPrr(this ResultSource results, Byte PartFlag, UInt16 TestCount, UInt16 HardBin, Boolean SupersedesPartId, Boolean SupersedesCoords, Boolean AbnormalTest, Byte? HeadNumber = null, Byte? SiteNumber = null, UInt16? SoftBin = null, Int16? XCoordinate = null, Int16? YCoordinate = null, UInt32? TestTime = null, String PartId = null, String PartText = null, Byte[] PartFix = null, Boolean? Failed = null)
        {
            results.Publish("STDF Prr", new List<string> { "HeadNumber", "SiteNumber", "PartFlag", "TestCount", "HardBin", "SoftBin", "XCoordinate", "YCoordinate", 
                "TestTime", "PartId", "PartText", "PartFix", "SupersedesPartId", "SupersedesCoords", "AbnormalTest", "Failed" },
                new IConvertible[] { (IConvertible)HeadNumber, (IConvertible)SiteNumber, (IConvertible)PartFlag, (IConvertible)TestCount, (IConvertible)HardBin, (IConvertible)SoftBin, 
                    (IConvertible)XCoordinate, (IConvertible)YCoordinate, (IConvertible)TestTime, DeNull(PartId), DeNull(PartText), JoinArray<Byte>(PartFix), (IConvertible)SupersedesPartId, 
                    (IConvertible)SupersedesCoords, (IConvertible)AbnormalTest, (IConvertible)Failed });
        }

        public static void AddTsr(this ResultSource results, String TestType, UInt32 TestNumber, Byte? HeadNumber = null, Byte? SiteNumber = null, UInt32? ExecutedCount = null, UInt32? FailedCount = null, UInt32? AlarmCount = null, String TestName = null, String SequencerName = null, String TestLabel = null, Single? TestTime = null, Single? TestMin = null, Single? TestMax = null, Single? TestSum = null, Single? TestSumOfSquares = null)
        {
            results.Publish("STDF Tsr", new List<string> { "HeadNumber", "SiteNumber", "TestType", "TestNumber", "ExecutedCount", "FailedCount", "AlarmCount", "TestName", "SequencerName", "TestLabel", "TestTime", "TestMin", "TestMax", "TestSum", "TestSumOfSquares" },
                new IConvertible[] { (IConvertible)HeadNumber, (IConvertible)SiteNumber, DeNull(TestType), (IConvertible)TestNumber, (IConvertible)ExecutedCount, (IConvertible)FailedCount, (IConvertible)AlarmCount, DeNull(TestName), DeNull(SequencerName), DeNull(TestLabel), (IConvertible)TestTime, (IConvertible)TestMin, (IConvertible)TestMax, (IConvertible)TestSum, (IConvertible)TestSumOfSquares });
        }

        public static void AddPtr(this ResultSource results, UInt32 TestNumber, Byte TestFlags, Byte ParametricFlags, Byte? HeadNumber = null, Byte? SiteNumber = null, Single? Result = null, String TestText = null, String AlarmId = null, Byte? OptionalFlags = null, SByte? ResultScalingExponent = null, SByte? LowLimitScalingExponent = null, SByte? HighLimitScalingExponent = null, Single? LowLimit = null, Single? HighLimit = null, String Units = null, String ResultFormatString = null, String LowLimitFormatString = null, String HighLimitFormatString = null, Single? LowSpecLimit = null, Single? HighSpecLimit = null)
        {
            results.Publish("STDF Ptr", new List<string> { "TestNumber", "HeadNumber", "SiteNumber", "TestFlags", "ParametricFlags", "Result", "TestText", "AlarmId", "OptionalFlags", "ResultScalingExponent", "LowLimitScalingExponent", "HighLimitScalingExponent", "LowLimit", "HighLimit", "Units", "ResultFormatString", "LowLimitFormatString", "HighLimitFormatString", "LowSpecLimit", "HighSpecLimit" },
                new IConvertible[] { (IConvertible)TestNumber, (IConvertible)HeadNumber, (IConvertible)SiteNumber, (IConvertible)TestFlags, (IConvertible)ParametricFlags, (IConvertible)Result, DeNull(TestText), DeNull(AlarmId), (IConvertible)OptionalFlags, (IConvertible)ResultScalingExponent, (IConvertible)LowLimitScalingExponent, (IConvertible)HighLimitScalingExponent, (IConvertible)LowLimit, (IConvertible)HighLimit, DeNull(Units), DeNull(ResultFormatString), DeNull(LowLimitFormatString), DeNull(HighLimitFormatString), (IConvertible)LowSpecLimit, (IConvertible)HighSpecLimit });
        }

        public static void AddMpr(this ResultSource results, UInt32 TestNumber, Byte TestFlags, Byte ParametricFlags, Byte? HeadNumber = null, Byte? SiteNumber = null, Byte[] PinStates = null, Single[] Results = null, String TestText = null, String AlarmId = null, Byte? OptionalFlags = null, SByte? ResultScalingExponent = null, SByte? LowLimitScalingExponent = null, SByte? HighLimitScalingExponent = null, Single? LowLimit = null, Single? HighLimit = null, Single? StartingCondition = null, Single? ConditionIncrement = null, UInt16[] PinIndexes = null, String Units = null, String IncrementUnits = null, String ResultFormatString = null, String LowLimitFormatString = null, String HighLimitFormatString = null, Single? LowSpecLimit = null, Single? HighSpecLimit = null)
        {
            results.Publish("STDF Mpr", new List<string> { "TestNumber", "HeadNumber", "SiteNumber", "TestFlags", "ParametricFlags", "PinStates", "Results", "TestText", "AlarmId", "OptionalFlags", "ResultScalingExponent", "LowLimitScalingExponent", "HighLimitScalingExponent", "LowLimit", "HighLimit", "StartingCondition", "ConditionIncrement", "PinIndexes", "Units", "IncrementUnits", "ResultFormatString", "LowLimitFormatString", "HighLimitFormatString", "LowSpecLimit", "HighSpecLimit" },
                new IConvertible[] { (IConvertible)TestNumber, (IConvertible)HeadNumber, (IConvertible)SiteNumber, (IConvertible)TestFlags, (IConvertible)ParametricFlags, JoinArray<Byte>(PinStates), JoinArray<Single>(Results), DeNull(TestText), DeNull(AlarmId), (IConvertible)OptionalFlags, (IConvertible)ResultScalingExponent, (IConvertible)LowLimitScalingExponent, (IConvertible)HighLimitScalingExponent, (IConvertible)LowLimit, (IConvertible)HighLimit, (IConvertible)StartingCondition, (IConvertible)ConditionIncrement, JoinArray<UInt16>(PinIndexes), DeNull(Units), DeNull(IncrementUnits), DeNull(ResultFormatString), DeNull(LowLimitFormatString), DeNull(HighLimitFormatString), (IConvertible)LowSpecLimit, (IConvertible)HighSpecLimit });
        }

        public static void AddFtr(this ResultSource results, UInt32 TestNumber, Byte TestFlags, Byte? HeadNumber = null, Byte? SiteNumber = null, UInt32? CycleCount = null, UInt32? RelativeVectorAddress = null, UInt32? RepeatCount = null, UInt32? FailingPinCount = null, Int32? XFailureAddress = null, Int32? YFailureAddress = null, Int16? VectorOffset = null, UInt16[] ReturnIndexes = null, Byte[] ReturnStates = null, UInt16[] ProgrammedIndexes = null, Byte[] ProgrammedStates = null, BitArray FailingPinBitfield = null, String VectorName = null, String OpCode = null, String TestText = null, String AlarmId = null, String ProgrammedText = null, String ResultText = null, Byte? PatternGeneratorNumber = null, BitArray SpinMap = null)
        {
            results.Publish("STDF Ftr", new List<string> { "TestNumber", "HeadNumber", "SiteNumber", "TestFlags", "CycleCount", "RelativeVectorAddress", "RepeatCount", "FailingPinCount", "XFailureAddress", "YFailureAddress", "VectorOffset", "ReturnIndexes", "ReturnStates", "ProgrammedIndexes", "ProgrammedStates", "FailingPinBitfield", "VectorName", "TimeSet", "OpCode", "TestText", "AlarmId", "ProgrammedText", "ResultText", "PatternGeneratorNumber", "SpinMap" },
                new IConvertible[] { (IConvertible)TestNumber, (IConvertible)HeadNumber, (IConvertible)SiteNumber, (IConvertible)TestFlags, (IConvertible)CycleCount, (IConvertible)RelativeVectorAddress, (IConvertible)RepeatCount, (IConvertible)FailingPinCount, (IConvertible)XFailureAddress, (IConvertible)YFailureAddress, (IConvertible)VectorOffset, JoinArray<UInt16>(ReturnIndexes), JoinArray<Byte>(ReturnStates), JoinArray<UInt16>(ProgrammedIndexes), JoinArray<Byte>(ProgrammedStates), ToConvertible(FailingPinBitfield), DeNull(VectorName), DeNull(OpCode), DeNull(TestText), DeNull(AlarmId), DeNull(ProgrammedText), DeNull(ResultText), (IConvertible)PatternGeneratorNumber, ToConvertible(SpinMap) });
        }

        public static void AddBps(this ResultSource results, String Name = null)
        {
            results.Publish("STDF Bps", new List<string> { "Name" },
                new IConvertible[] { DeNull(Name) });
        }

        public static void AddEps(this ResultSource results)
        {
            results.Publish("STDF Eps", new List<string> {},
                new IConvertible[] {});
        }

        public static void AddDtr(this ResultSource results, String Text)
        {
            results.Publish("STDF Dtr", new List<string> { "Text" },
                new IConvertible[] { DeNull(Text) });
        }
    }
}
