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
    public class Utility
    {
        private static BitArray ToBitarray(IConvertible val)
        {
            if (val == null) return new BitArray(0);
            string str = val.ToString();
            if (string.IsNullOrEmpty(str)) return new BitArray(0);

            var Ints = str.Split(',').Select(sc => int.Parse(sc)).ToList();

            if (Ints.Count < 1) return new BitArray(0);

            BitArray b = new BitArray(Ints[0]);
            for (int i = 1; i < Ints.Count; i++)
                b[Ints[i]] = true;
            return b;
        }

        private static T[] ToArray<T>(IConvertible val)
        {
            string str = null;
            if (val != null) str = val.ToString();

            if (string.IsNullOrEmpty(str))
                return new T[0];
            else
                return str.Split(',').Select(x => (T)Convert.ChangeType(x, typeof(T), System.Globalization.CultureInfo.InvariantCulture)).ToArray();
        }

        public static bool IsSTDFType(ResultTable Typ)
        {
            return Typ.Name.StartsWith("STDF");
        }

        public static IEnumerable<StdfRecord> GetRecord(ResultTable Result)
        {
            for (int Row = 0; Row < Result.Rows; Row++)
            {
                Func<int, IConvertible> GetDim = (dim) =>
                    {
                        var value = Result.Columns[dim].Data.GetValue(Row);
                        return value as IConvertible;
                    };

                switch (Result.Name.Substring(5).Substring(0, 3))
                {
                    case "Far":
                        yield return new LinqToStdf.Records.V4.Far
                        {
                            CpuType = (Byte)GetDim(0),
                            StdfVersion = (Byte)GetDim(1)
                        };
                        break;

                    case "Atr":
                        yield return new LinqToStdf.Records.V4.Atr
                        {
                            ModifiedTime = (DateTime?)GetDim(0),
                            CommandLine = (String)GetDim(1)
                        };
                        break;

                    case "Mir":
                        yield return new LinqToStdf.Records.V4.Mir
                        {
                            SetupTime = (DateTime?)GetDim(0),
                            StartTime = (DateTime?)GetDim(1),
                            StationNumber = (Byte)GetDim(2),
                            ModeCode = (String)GetDim(3),
                            RetestCode = (String)GetDim(4),
                            ProtectionCode = (String)GetDim(5),
                            BurnInTime = (UInt16?)GetDim(6),
                            CommandModeCode = (String)GetDim(7),
                            LotId = (String)GetDim(8),
                            PartType = (String)GetDim(9),
                            NodeName = (String)GetDim(10),
                            TesterType = (String)GetDim(11),
                            JobName = (String)GetDim(12),
                            JobRevision = (String)GetDim(13),
                            SublotId = (String)GetDim(14),
                            OperatorName = (String)GetDim(15),
                            ExecType = (String)GetDim(16),
                            ExecVersion = (String)GetDim(17),
                            TestCode = (String)GetDim(18),
                            TestTemperature = (String)GetDim(19),
                            UserText = (String)GetDim(20),
                            AuxiliaryFile = (String)GetDim(21),
                            PackageType = (String)GetDim(22),
                            FamilyId = (String)GetDim(23),
                            DateCode = (String)GetDim(24),
                            FacilityId = (String)GetDim(25),
                            FloorId = (String)GetDim(26),
                            ProcessId = (String)GetDim(27),
                            OperationFrequency = (String)GetDim(28),
                            SpecificationName = (String)GetDim(29),
                            SpecificationVersion = (String)GetDim(30),
                            FlowId = (String)GetDim(31),
                            SetupId = (String)GetDim(32),
                            DesignRevision = (String)GetDim(33),
                            EngineeringId = (String)GetDim(34),
                            RomCode = (String)GetDim(35),
                            SerialNumber = (String)GetDim(36),
                            SupervisorName = (String)GetDim(37)
                        };
                        break;

                    case "Mrr":
                        yield return new LinqToStdf.Records.V4.Mrr
                        {
                            FinishTime = (DateTime?)GetDim(0),
                            DispositionCode = (String)GetDim(1),
                            UserDescription = (String)GetDim(2),
                            ExecDescription = (String)GetDim(3)
                        };
                        break;

                    case "Pcr":
                        yield return new LinqToStdf.Records.V4.Pcr
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteNumber = (Byte?)GetDim(1),
                            PartCount = (UInt32)GetDim(2),
                            RetestCount = (UInt32?)GetDim(3),
                            AbortCount = (UInt32?)GetDim(4),
                            GoodCount = (UInt32?)GetDim(5),
                            FunctionalCount = (UInt32?)GetDim(6)
                        };
                        break;

                    case "Hbr":
                        yield return new LinqToStdf.Records.V4.Hbr
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteNumber = (Byte?)GetDim(1),
                            BinNumber = (UInt16)GetDim(2),
                            BinCount = (UInt32)GetDim(3),
                            BinPassFail = (String)GetDim(4),
                            BinName = (String)GetDim(5)
                        };
                        break;

                    case "Sbr":
                        yield return new LinqToStdf.Records.V4.Sbr
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteNumber = (Byte?)GetDim(1),
                            BinNumber = (UInt16)GetDim(2),
                            BinCount = (UInt32)GetDim(3),
                            BinPassFail = (String)GetDim(4),
                            BinName = (String)GetDim(5)
                        };
                        break;

                    case "Pmr":
                        yield return new LinqToStdf.Records.V4.Pmr
                        {
                            PinIndex = (UInt16)GetDim(0),
                            ChannelType = (UInt16?)GetDim(1),
                            ChannelName = (String)GetDim(2),
                            PhysicalName = (String)GetDim(3),
                            LogicalName = (String)GetDim(4),
                            HeadNumber = (Byte?)GetDim(5),
                            SiteNumber = (Byte?)GetDim(6)
                        }; break;

                    case "Pgr":
                        yield return new LinqToStdf.Records.V4.Pgr
                        {
                            GroupIndex = (UInt16)GetDim(0),
                            GroupName = (String)GetDim(1),
                            PinIndexes = ToArray<UInt16>(GetDim(2))
                        };
                        break;

                    case "Rdr":
                        yield return new LinqToStdf.Records.V4.Rdr
                        {
                            RetestBins = ToArray<UInt16>(GetDim(0))
                        };
                        break;

                    case "Sdr":
                        yield return new LinqToStdf.Records.V4.Sdr
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteGroup = (Byte?)GetDim(1),
                            SiteNumbers = ToArray<Byte>(GetDim(2)),
                            HandlerType = (String)GetDim(3),
                            HandlerId = (String)GetDim(4),
                            CardType = (String)GetDim(5),
                            CardId = (String)GetDim(6),
                            LoadboardType = (String)GetDim(7),
                            LoadboardId = (String)GetDim(8),
                            DibType = (String)GetDim(9),
                            DibId = (String)GetDim(10),
                            CableType = (String)GetDim(11),
                            CableId = (String)GetDim(12),
                            ContactorType = (String)GetDim(13),
                            ContactorId = (String)GetDim(14),
                            LaserType = (String)GetDim(15),
                            LaserId = (String)GetDim(16),
                            ExtraType = (String)GetDim(17),
                            ExtraId = (String)GetDim(18)
                        }; break;

                    case "Wir":
                        yield return new LinqToStdf.Records.V4.Wir
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteGroup = (Byte?)GetDim(1),
                            StartTime = (DateTime?)GetDim(2),
                            WaferId = (String)GetDim(3)
                        }; break;

                    case "Wrr":
                        yield return new LinqToStdf.Records.V4.Wrr
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteGroup = (Byte?)GetDim(1),
                            FinishTime = (DateTime?)GetDim(2),
                            PartCount = (UInt32)GetDim(3),
                            RetestCount = (UInt32?)GetDim(4),
                            AbortCount = (UInt32?)GetDim(5),
                            GoodCount = (UInt32?)GetDim(6),
                            FunctionalCount = (UInt32?)GetDim(7),
                            WaferId = (String)GetDim(8),
                            FabWaferId = (String)GetDim(9),
                            FrameId = (String)GetDim(10),
                            MaskId = (String)GetDim(11),
                            UserDescription = (String)GetDim(12),
                            ExecDescription = (String)GetDim(13)
                        }; break;

                    case "Wcr":
                        yield return new LinqToStdf.Records.V4.Wcr
                        {
                            WaferSize = (Single?)GetDim(0),
                            DieHeight = (Single?)GetDim(1),
                            DieWidth = (Single?)GetDim(2),
                            Units = (Byte?)GetDim(3),
                            Flat = (String)GetDim(4),
                            CenterX = (Int16?)GetDim(5),
                            CenterY = (Int16?)GetDim(6),
                            PositiveX = (String)GetDim(7),
                            PositiveY = (String)GetDim(8)
                        }; break;

                    case "Pir":
                        yield return new LinqToStdf.Records.V4.Pir
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteNumber = (Byte?)GetDim(1)
                        }; break;
                    case "Prr":
                        yield return new LinqToStdf.Records.V4.Prr
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteNumber = (Byte?)GetDim(1),
                            PartFlag = (Byte)GetDim(2),
                            TestCount = (UInt16)GetDim(3),
                            HardBin = (UInt16)GetDim(4),
                            SoftBin = (UInt16?)GetDim(5),
                            XCoordinate = (Int16?)GetDim(6),
                            YCoordinate = (Int16?)GetDim(7),
                            TestTime = (UInt32?)GetDim(8),
                            PartId = (String)GetDim(9),
                            PartText = (String)GetDim(10),
                            PartFix = ToArray<Byte>(GetDim(11)),
                            SupersedesPartId = (Boolean)GetDim(12),
                            SupersedesCoords = (Boolean)GetDim(13),
                            AbnormalTest = (Boolean)GetDim(14),
                            Failed = (Boolean?)GetDim(15)
                        }; break;

                    case "Tsr":
                        yield return new LinqToStdf.Records.V4.Tsr
                        {
                            HeadNumber = (Byte?)GetDim(0),
                            SiteNumber = (Byte?)GetDim(1),
                            TestType = (String)GetDim(2),
                            TestNumber = (UInt32)GetDim(3),
                            ExecutedCount = (UInt32?)GetDim(4),
                            FailedCount = (UInt32?)GetDim(5),
                            AlarmCount = (UInt32?)GetDim(6),
                            TestName = (String)GetDim(7),
                            SequencerName = (String)GetDim(8),
                            TestLabel = (String)GetDim(9),
                            TestTime = (Single?)GetDim(10),
                            TestMin = (Single?)GetDim(11),
                            TestMax = (Single?)GetDim(12),
                            TestSum = (Single?)GetDim(13),
                            TestSumOfSquares = (Single?)GetDim(14)
                        }; break;

                    case "Ptr":
                        yield return new LinqToStdf.Records.V4.Ptr
                        {
                            TestNumber = (UInt32)GetDim(0),
                            HeadNumber = (Byte?)GetDim(1),
                            SiteNumber = (Byte?)GetDim(2),
                            TestFlags = (Byte)GetDim(3),
                            ParametricFlags = (Byte)GetDim(4),
                            Result = (Single?)GetDim(5),
                            TestText = (String)GetDim(6),
                            AlarmId = (String)GetDim(7),
                            OptionalFlags = (Byte?)GetDim(8),
                            ResultScalingExponent = (SByte?)GetDim(9),
                            LowLimitScalingExponent = (SByte?)GetDim(10),
                            HighLimitScalingExponent = (SByte?)GetDim(11),
                            LowLimit = (Single?)GetDim(12),
                            HighLimit = (Single?)GetDim(13),
                            Units = (String)GetDim(14),
                            ResultFormatString = (String)GetDim(15),
                            LowLimitFormatString = (String)GetDim(16),
                            HighLimitFormatString = (String)GetDim(17),
                            LowSpecLimit = (Single?)GetDim(18),
                            HighSpecLimit = (Single?)GetDim(19)
                        }; break;

                    case "Mpr":
                        yield return new LinqToStdf.Records.V4.Mpr
                        {
                            TestNumber = (UInt32)GetDim(0),
                            HeadNumber = (Byte?)GetDim(1),
                            SiteNumber = (Byte?)GetDim(2),
                            TestFlags = (Byte)GetDim(3),
                            ParametricFlags = (Byte)GetDim(4),
                            PinStates = ToArray<Byte>(GetDim(5)),
                            Results = ToArray<Single>(GetDim(6)),
                            TestText = (String)GetDim(7),
                            AlarmId = (String)GetDim(8),
                            OptionalFlags = (Byte?)GetDim(9),
                            ResultScalingExponent = (SByte?)GetDim(10),
                            LowLimitScalingExponent = (SByte?)GetDim(11),
                            HighLimitScalingExponent = (SByte?)GetDim(12),
                            LowLimit = (Single?)GetDim(13),
                            HighLimit = (Single?)GetDim(14),
                            StartingCondition = (Single?)GetDim(15),
                            ConditionIncrement = (Single?)GetDim(16),
                            PinIndexes = ToArray<UInt16>(GetDim(17)),
                            Units = (String)GetDim(18),
                            IncrementUnits = (String)GetDim(19),
                            ResultFormatString = (String)GetDim(20),
                            LowLimitFormatString = (String)GetDim(21),
                            HighLimitFormatString = (String)GetDim(22),
                            LowSpecLimit = (Single?)GetDim(23),
                            HighSpecLimit = (Single?)GetDim(24)
                        }; break;

                    case "Ftr":
                        yield return new LinqToStdf.Records.V4.Ftr
                        {
                            TestNumber = (UInt32)GetDim(0),
                            HeadNumber = (Byte?)GetDim(1),
                            SiteNumber = (Byte?)GetDim(2),
                            TestFlags = (Byte)GetDim(3),
                            CycleCount = (UInt32?)GetDim(4),
                            RelativeVectorAddress = (UInt32?)GetDim(5),
                            RepeatCount = (UInt32?)GetDim(6),
                            FailingPinCount = (UInt32?)GetDim(7),
                            XFailureAddress = (Int32?)GetDim(8),
                            YFailureAddress = (Int32?)GetDim(9),
                            VectorOffset = (Int16?)GetDim(10),
                            ReturnIndexes = ToArray<UInt16>(GetDim(11)),
                            ReturnStates = ToArray<Byte>(GetDim(12)),
                            ProgrammedIndexes = ToArray<UInt16>(GetDim(13)),
                            ProgrammedStates = ToArray<Byte>(GetDim(14)),
                            FailingPinBitfield = ToBitarray(GetDim(15)),
                            VectorName = (String)GetDim(16),
                            OpCode = (String)GetDim(17),
                            TestText = (String)GetDim(18),
                            AlarmId = (String)GetDim(19),
                            ProgrammedText = (String)GetDim(20),
                            ResultText = (String)GetDim(21),
                            PatternGeneratorNumber = (Byte?)GetDim(22),
                            SpinMap = ToBitarray(GetDim(23))
                        }; break;

                    case "Bps":
                        yield return new LinqToStdf.Records.V4.Bps { Name = (String)GetDim(0) }; break;

                    case "Eps":
                        yield return new LinqToStdf.Records.V4.Eps { }; break;

                    case "Dtr":
                        yield return new LinqToStdf.Records.V4.Dtr { Text = (String)GetDim(0) }; break;
                }
            }
        }
    }
}
