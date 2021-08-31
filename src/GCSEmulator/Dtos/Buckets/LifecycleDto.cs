using System;
using System.Collections.Generic;
using System.Linq;
using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    public class LifecycleDto
    {
        public List<LifecycleRule> Rule { get; set; } = new();

        public static LifecycleDto Create(LifecycleSettings lifecycleSettings)
        {
            return new LifecycleDto
            {
                Rule = lifecycleSettings.Rule.Select(LifecycleRule.Create).ToList()
            };
        }

        public class LifecycleRule
        {
            public LifecycleRuleAction Action { get; set; }

            public static LifecycleRule Create(LifecycleSettings.LifecycleRule rule)
            {
                return new LifecycleRule
                {
                    Action = new LifecycleRuleAction
                    {
                        Type = rule.Action.Type,
                        StorageClass = rule.Action.StorageClass
                    }
                };
            }

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