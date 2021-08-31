using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Buckets
{
    public class BillingDto
    {
        public bool RequesterPays { get; set; }

        public static BillingDto Create(BillingSettings billingSettings)
        {
            return new BillingDto { RequesterPays = billingSettings.RequesterPays };
        }
    }
}