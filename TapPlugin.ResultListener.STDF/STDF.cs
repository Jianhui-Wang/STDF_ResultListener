// Responsible: TEAM (johansen)
// Copyright:   Copyright 2015 Keysight Technologies.  All rights reserved. No 
//              part of this program may be photocopied, reproduced or translated 
//              to another program language without the prior written consent of 
//              Keysight Technologies.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Keysight.Tap;
using Keysight.Tap.ResultsViewer;
using LinqToStdf;
using System.IO;

namespace TapPlugin.ResultListeners
{
    [DisplayName("STDF Database")]
    [ShortName("STDF")]
    public class STDF : ResultListener, IFileResultStore
    {
        [MacroPath]
        [DisplayName("File name")]
        [Description("Path to file to save. May contain tags: <DATE> expands to date, <VERDICT> expands to testplan verdict")]
        public string FilePath { get; set; }

        public STDF()
        {
            FilePath = "results/<Date>-<Verdict>.stdf";
        }

        #region ResultListener
        private List<LinqToStdf.StdfRecord> Records = null;

        public override void OnResultPublished(Guid stepRunId, ResultTable result)
        {
            base.OnResultPublished(stepRunId, result);

            if (Utility.IsSTDFType(result))
                Records.AddRange(Utility.GetRecord(result));
            else
                throw new Exception("Wrong result type. Expected STDF record result type");
        }

        public override void OnTestPlanRunStart(TestPlanRun planRun)
        {
            base.OnTestPlanRunStart(planRun);
            Records = new List<StdfRecord>();
        }

        public override void OnTestPlanRunCompleted(TestPlanRun planRun, Stream logStream)
        {
            base.OnTestPlanRunCompleted(planRun, logStream);

            try
            {
                using (StdfFileWriter fw = new StdfFileWriter(MacroPathAttribute.Expand(FilePath, planRun)))
                {
                    fw.WriteRecords(Records);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw ex;
            }
        }
        #endregion

        public string DefaultExtension
        {
            get { return "stdf"; }
        }

        public TimeSpan GetAverageDuration(TestStepRun testStep, int averageCount)
        {
            return TimeSpan.Zero;
        }
    }
}
