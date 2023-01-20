using Hemiptera_API.Extensions;
using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Project.Requests;
using Hemiptera_Contracts.Project.Responses;
using Hemiptera_Contracts.Project.Validator;
using Microsoft.AspNetCore.Mvc;

namespace Hemiptera_API.Controllers
{
    public class ProjectController : ControllerBase
    {
        private IUnitOfWorkService _unitOfWork;

        public ProjectController(IUnitOfWorkService unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetProject(Guid id)
        {
            var getProjectResult = _unitOfWork.Project.GetById(id);

            if (getProjectResult.IsFailure)
            {
                return NotFound(getProjectResult.Errors);
            }
            var response = MapProjectResponse(getProjectResult.Payload!);
            return Ok(response);
        }

        [HttpGet("GetAll")]
        public IActionResult GetProjects()
        {
            var getProjectResult = _unitOfWork.Project.GetAll();

            if (getProjectResult.IsFailure)
            {
                return NotFound(getProjectResult.Errors);
            }

            var response = MapProjectResponse(getProjectResult.Payloads.ToList());
            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult CreateProject(CreateProjectRequest request)
        {
            // RETURN ALREADY EXISTING ERROR
            var validator = new CreateProjectValidator();

            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                Project requestToProjectResult = Project.From(request);

                var createProjectResult = _unitOfWork.Project.Insert(requestToProjectResult);
                if (createProjectResult.IsFailure)
                {
                    return UnprocessableEntity(createProjectResult.Errors);
                }

                _unitOfWork.Save();
                return GetProjectCreatedAt(requestToProjectResult);
            }
            else
            {
                return BadRequest(validationResult.Errors);
            }
        }

        [HttpPut("Update/{id:guid}")]
        public IActionResult UpdateProject(Guid id, UpdateProjectRequest request)
        {
            var validator = new UpdateProjectValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var updateProjectResult = _unitOfWork.Project.Update(Project.From(id, request));
            if (updateProjectResult.IsFailure)
            {
                return NotFound(updateProjectResult.Errors);
            }
            return Ok(updateProjectResult);
        }

        [HttpDelete("Delete/{id:guid}")]
        public IActionResult DeleteProject(Guid id)
        {
            var deleteProjectResult = _unitOfWork.Project.Delete(id);

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
            return projects.ConvertAll(MapProjectResponse);
        }

        private CreatedAtActionResult GetProjectCreatedAt(Project project)
        {
            return CreatedAtAction(
                actionName: nameof(GetProject),
                routeValues: new { id = project.Id },
                value: MapProjectResponse(project));
        }
    }
}