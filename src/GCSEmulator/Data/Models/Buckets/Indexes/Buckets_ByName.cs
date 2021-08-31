using System.Linq;
using Raven.Client.Documents.Indexes;

namespace GCSEmulator.Data.Models.Buckets.Indexes
{
    public class Buckets_ByName : AbstractIndexCreationTask<Bucket>
    {
        public Buckets_ByName()
        {
            Map = buckets => buckets.Select(b => new
            {
                b.Name
            });
        }
    }
}