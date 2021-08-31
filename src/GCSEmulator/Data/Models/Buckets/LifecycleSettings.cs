using System;
using System.Collections.Generic;

namespace GCSEmulator.Data.Models.Buckets
{
    public class LifecycleSettings
    {
        public List<LifecycleRule> Rule { get; set; } = new();

        public class LifecycleRule
        {
            public LifecycleRuleAction Action { get; set; }

            public sealed class LifecycleRuleAction
            {
                public string Type { get; set; }
                public string StorageClass { get; set; }
            }

            public sealed class LifecycleRuleCondition
            {
                public int Age { get; set; }
                public DateTime CreatedBefore { get; set; }
                public DateTime CustomTimeBefore { get; set; }
                public int DaysSinceCustomTime { get; set; }
                public int DaysSinceNoncurrentTime { get; set; }
                public bool IsLive { get; set; }
                public List<string> MatchesStorageClass { get; set; }
                public DateTime NoncurrentTimeBefore { get; set; }
                public int NumNewerVersions { get; set; }
            }
        }
    }
}