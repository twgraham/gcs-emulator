using GCSEmulator.Data.Models.Buckets;
using GCSEmulator.Dtos.Shared;

namespace GCSEmulator.Dtos.Buckets
{
    public class DefaultObjectAccessControlDto : IResourceResponse
    {
        public string Kind => "storage#objectAccessControl";
        public string Entity { get; set; }
        public string Role { get; set; }
        public string? Email { get; set; }
        public string EntityId { get; set; }
        public string? Domain { get; set; }
        public ProjectTeamDto ProjectTeam { get; set; }
        public string Etag { get; set; }

        public static DefaultObjectAccessControlDto Create(Bucket bucket, DefaultObjectAccessControl accessControl)
        {
            return new DefaultObjectAccessControlDto
            {
                Domain = accessControl.Domain,
                Email = accessControl.Email,
                Entity = accessControl.Entity,
                Etag = accessControl.GetHashCode().ToString(),
                Role = accessControl.Role,
                EntityId = accessControl.EntityId,
                ProjectTeam = ProjectTeamDto.Create(accessControl.ProjectTeam, bucket.ProjectNumber.ToString())
            };
        }
    }
}