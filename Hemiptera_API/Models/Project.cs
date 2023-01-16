using Hemiptera_API.Models.Enums;

namespace Hemiptera_API.Models
{
    public class Project
    {
        public Guid Id { get;  }
        public string Name { get; }
        public string? Description { get; }
        public DateTime StartDatetTime { get; }
        public DateTime? EndDatetTime { get; }
        public ProjectStatus Status { get; }
        public ProjectType Type { get; }

        private Project(
            Guid id,
            string name,
            string? description,
            DateTime startDatetTime,
            DateTime? endDatetTime,
            ProjectStatus status,
            ProjectType type)
        {
            Id = id;
            Name = name;
            Description = description;
            StartDatetTime = startDatetTime;
            EndDatetTime = endDatetTime;
            Status = status;
            Type = type;
        }

        public static Project Create(
            Guid? id,
            string name,
            string? description,
            DateTime startDateTime,
            DateTime endDateTime,
            ProjectStatus status,
            ProjectType type)
        {
            return new Project(
                id ?? Guid.NewGuid(),
                name,
                description,
                startDateTime,
                endDateTime,
                status,
                type);
        }

        public static Project From(CreateBreakfastRequest request)
        {
            return Create(
                req)
        }
    }
}
