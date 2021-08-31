namespace GCSEmulator.Data.Models.Buckets
{
    public class DefaultObjectAccessControl
    {
        public string Entity { get; set; }
        public string Role { get; set; }
        public string? Email { get; set; }
        public string EntityId { get; set; }
        public string? Domain { get; set; }
        public ProjectTeam ProjectTeam { get; set; }
    }
}