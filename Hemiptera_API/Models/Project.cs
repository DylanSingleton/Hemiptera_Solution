using Hemiptera_API.Models.Enums;
using Hemiptera_API.Services;
using Hemiptera_Contracts.Project.Requests;

namespace Hemiptera_API.Models
{
    public class Project
    {
        public Guid Id { get;  }
        public string Name { get; }
        public string? Description { get; }
        public string? RepositoryLink { get; }
        public DateTime StartDatetTime { get; }
        public DateTime? EndDatetTime { get; }
        public ProjectStatus Status { get; }
        public ProjectType Type { get; }

        private Project(
            Guid id,
            string name,
            string? description,
            string? repositoryLink,
            DateTime startDatetTime,
            DateTime? endDatetTime,
            ProjectStatus status,
            ProjectType type)
        {
            Id = id;
            Name = name;
            Description = description;
            RepositoryLink = repositoryLink;
            StartDatetTime = startDatetTime;
            EndDatetTime = endDatetTime;
            Status = status;
            Type = type;
        }

        private static Project Create(
            string name,
            string? description,
            string? repositoryLink,
            DateTime startDateTime,
            DateTime? endDateTime,
            ProjectStatus status,
            ProjectType type,
            Guid? id = null)
        {
            return new Project(
                id ?? Guid.NewGuid(),
                name,
                description,
                repositoryLink,
                startDateTime,
                endDateTime,
                status,
                type);
        }

        public static Project From(CreateProjectRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                request.RepositoryLink,
                request.StartDateTime,
                request.EndDateTime,
                (ProjectStatus)request.Status,
                (ProjectType)request.Type);
        }

        public static Project From(Guid id, UpdateProjectRequest request)
        {
            return Create(
                request.Name,
                request.Description,
                request.RepositoryLink,
                request.StartDateTime,
                request.EndDateTime,
                (ProjectStatus)request.Status,
                (ProjectType)request.Type,
                id);
        }
    }
}
