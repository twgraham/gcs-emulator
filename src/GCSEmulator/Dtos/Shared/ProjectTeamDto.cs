using System;
using GCSEmulator.Data.Models.Buckets;

namespace GCSEmulator.Dtos.Shared
{
    public enum Team
    {
        Editors,
        Owners,
        Viewers
    }

    public class ProjectTeamDto
    {
        public string ProjectNumber { get; set; }
        public Team Team { get; set; }

        public static ProjectTeamDto Create(ProjectTeam projectTeam, string projectNumber)
        {
            return new ProjectTeamDto
            {
                ProjectNumber = projectNumber,
                Team =  projectTeam switch
                {
                    ProjectTeam.Owners => Team.Owners,
                    ProjectTeam.Viewers => Team.Viewers,
                    ProjectTeam.Editors => Team.Editors,
                    _ => throw new ArgumentOutOfRangeException(nameof(projectTeam), projectTeam, null)
                }
            };
        }
    }
}