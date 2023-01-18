using Hemiptera_API.Extensions;
using Hemiptera_API.Models;
using Hemiptera_API.Models.Enums;
using Hemiptera_API.Services;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Project.Requests;
using Hemiptera_Contracts.Project.Responses;
using Hemiptera_Contracts.Project.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;

namespace Hemiptera_API.Controllers
{

    public class ProjectController : ControllerBase
    {
        private IUnitOfWorkService _unitOfWork;
        public ProjectController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWork= unitOfWorkService;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetProject(Guid id)
        {
            var getProjectResult = _unitOfWork.ProjectService.GetById(id);

            if(getProjectResult.IsFailure)
            {
                return NotFound(getProjectResult.Errors);
            }
            var response = MapProjectResponse(getProjectResult.Payload!);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public IActionResult GetProjects()
        {
            var getProjectResult = _unitOfWork.ProjectService.GetAll();

            if (getProjectResult.IsFailure)
            {
                return NotFound(getProjectResult.Errors);
            }

            var response = MapProjectResponse(getProjectResult.Payload!);
            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateProject(CreateProjectRequest request)
        {
            var validator = new CreateProjectValidator();

            var validationResult = validator.Validate(request);
            if(validationResult.IsValid)
            {
                Project requestToProjectResult = Project.From(request);

                _unitOfWork.ProjectService.Insert(requestToProjectResult);

                _unitOfWork.Save();
                return GetProjectCreatedAt(requestToProjectResult);
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }

        [HttpDelete("Delete/{id:guid}")]
        public IActionResult DeleteProject(Guid id)
        {
            var deleteProjectResult = _unitOfWork.ProjectService.Delete(id);

            if (deleteProjectResult.IsFailure)
            {
                return NotFound(deleteProjectResult.Errors);
            }

            _unitOfWork.Save();
            return Ok();
        }

        private static ProjectResponse MapProjectResponse(Project project)
        {
            return new ProjectResponse(
                project.Name,
                project.Description ?? string.Empty,
                project.RepositoryLink ?? string.Empty,
                project.StartDatetTime,
                project.EndDatetTime,
                EnumDisplayExtensions.GetProjectStatusDisplayString(project.Status),
                EnumDisplayExtensions.GetProjectTypeDisplayString(project.Type));
        }

        private static List<ProjectResponse> MapProjectResponse(List<Project> projects)
        {
            var list = new List<ProjectResponse>();
            projects.ForEach(x => list.Add(MapProjectResponse(x)));
            return list;
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
