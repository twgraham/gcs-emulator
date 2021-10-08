using GCSEmulator.Dtos.Shared;

namespace GCSEmulator.Dtos.Objects
{
    public class ObjectAccessControlDto : IResourceResponse
    {
        public string Kind => "storage#objectAccessControl";
        public string Id { get; set; }
        public string SelfLink { get; set; }
        public string Bucket { get; set; }
        public string Object { get; set; }
        public string Generation { get; set; }
        public string Entity { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string EntityId { get; set; }
        public string Domain { get; set; }
        public ProjectTeamDto ProjectTeam { get; set; }
        public string Etag { get; set; }
    }
}