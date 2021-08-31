using System.Linq;
using Raven.Client.Documents.Indexes;

namespace GCSEmulator.Data.Models.Buckets.Indexes
{
    public class Buckets_ByProjectAndName : AbstractIndexCreationTask<Bucket>
    {
        public Buckets_ByProjectAndName()
        {
            Map = buckets =>
                from bucket in buckets
                select new
                {
                    bucket.ProjectId,
                    bucket.Name
                };
        }
    }
}