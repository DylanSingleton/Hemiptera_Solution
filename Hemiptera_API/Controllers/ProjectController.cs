using Hemiptera_API.Models;
using Hemiptera_API.Services;
using Hemiptera_Contracts.Project;
using Microsoft.AspNetCore.Mvc;

namespace Hemiptera_API.Controllers
{

    public class ProjectController : ControllerBase
    {
        private readonly IGenericService<Project> _genericService;
        public ProjectController(
            IGenericService<Project> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetProject(Guid id)
        {
            var getProjectResult = _genericService.GetById(id);

            var response = MapProjectResponse(getProjectResult);
            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateProject(CreateProjectRequest request)
        {
            Project requestToProjectResult = Project.From(request);

            _genericService.Insert(requestToProjectResult);

            return GetProjectCreatedAt(requestToProjectResult);
        }

        private static ProjectResponse MapProjectResponse(Project project)
        {
            return new ProjectResponse(
                project.Name,
                project.Description ?? string.Empty,
                project.RepositoryLink ?? string.Empty,
                project.StartDatetTime,
                project.EndDatetTime,
                project.Status.ToString(),
                project.Type.ToString());
        }

        private CreatedAtActionResult GetProjectCreatedAt(Project project)
        {
            return CreatedAtAction(
                actionName: nameof(GetProject),
                routeValues: new { id = project.Id},
                value: MapProjectResponse(project));
        }
    }
}
