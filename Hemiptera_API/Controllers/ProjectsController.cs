﻿using Hemiptera_API.Extensions;
using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Project.Requests;
using Hemiptera_Contracts.Project.Responses;
using Hemiptera_Contracts.Project.Validator;
using Microsoft.AspNetCore.Mvc;
using NotFoundResult = Hemiptera_API.Results.NotFoundResult;

namespace Hemiptera_API.Controllers
{
    [Route("api/[controller]/")]
    public class ProjectsController : ControllerBase
    {
        private IUnitOfWorkRepository _unitOfWork;

        public ProjectsController(IUnitOfWorkRepository unitOfWorkService)
        {
            _unitOfWork = unitOfWorkService;
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetProject(Guid id)
        {
            var getProjectResult = _unitOfWork.Project.GetById(id);

            if (getProjectResult.IsSuccessful)
            {
                return Ok(MapProjectResponse(getProjectResult.Payload!));
            }

            return new ObjectResult(getProjectResult.Error)
            { StatusCode = (int)getProjectResult.Error!.HttpStatusCode };
        }

        [HttpGet("GetAll")]
        public IActionResult GetProjects()
        {
                var getProjectResult = _unitOfWork.Project.GetAll();

                if (getProjectResult.IsSuccessful)
                {
                    return Ok(MapProjectResponse(getProjectResult.Payload));
                }
                else if (getProjectResult is NotFoundResult<List<Project>> notFoundResult)
                {
                    return NotFound(notFoundResult.Message);
                }

            return BadRequest();
        }

        [HttpPost("Create")]
        public IActionResult CreateProject(CreateProjectRequest request)
        {
            var validator = new CreateProjectValidator();

            var validationResult = validator.Validate(request);
            if (validationResult.IsValid)
            {
                var createProjectResult = _unitOfWork.Project.Insert(Project.From(request));

                if (createProjectResult.IsSuccessful)
                {
                    _unitOfWork.Save();
                    return GetProjectCreatedAt(createProjectResult.Payload!);
                }

                return new ObjectResult(createProjectResult.Error)
                { StatusCode = (int)createProjectResult.Error!.HttpStatusCode };
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

            if (updateProjectResult.IsSuccessful)
            {
                return Ok(updateProjectResult);
            }

            return new ObjectResult(updateProjectResult.Error)
            { StatusCode = (int)updateProjectResult.Error!.HttpStatusCode };
        }

        [HttpDelete("Delete/{id:guid}")]
        public IActionResult DeleteProject(Guid id)
        {
            var deleteProjectResult = _unitOfWork.Project.Delete(id);

            if (deleteProjectResult.IsSuccessful)
            {
                _unitOfWork.Save();
                return NoContent();
            }
            else if(deleteProjectResult is NotFoundResult notFoundResult)
            {
                return NotFound(notFoundResult.Message);
            }

            return BadRequest();
        }

        private static ProjectResponse MapProjectResponse(Project project)
        {
            return new ProjectResponse(
                project.Name,
                project.Description ?? string.Empty,
                project.RepositoryLink ?? string.Empty,
                project.StartDatetTime,
                project.EndDatetTime,
                project.Status.DisplayString(),
                project.Type.DisplayString());
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