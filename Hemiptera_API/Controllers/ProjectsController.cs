using Hemiptera_API.Extensions;
using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_API.Utilitys;
using Hemiptera_API.Validators.Projects;
using Hemiptera_Contracts.Projects.Requests;
using Hemiptera_Contracts.Projects.Responses;
using Hemiptera_Contracts.Users.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotFoundResult = Hemiptera_API.Results.NotFoundResult;

namespace Hemiptera_API.Controllers;

[Route("api/[controller]/")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            return Ok(MapProjectResponse(getProjectResult.Payload));
        }
        if (getProjectResult is NotFoundResult<Project> notFoundResult)
        {
            return NotFound(notFoundResult.Message);
        }

        return BadRequest();
    }

    [Authorize]
    [HttpGet("GetAll")]
    public IActionResult GetProjects()
    {
        var getProjectResult = _unitOfWork.Project.GetAll();

        if (getProjectResult.IsSuccessful)
        {
            return Ok(MapProjectResponse(getProjectResult.Payload));
        }
        if (getProjectResult is NotFoundResult<List<Project>> notFoundResult)
        {
            return NotFound(notFoundResult.Message);
        }

        return BadRequest();
    }

    [HttpPost("Create")]
    public IActionResult CreateProject(CreateProjectRequest request)
    {
        var validatorResult = ValidatorResultUtility.Validate(request, new CreateProjectValidator());
        if (validatorResult.IsUnsuccessful) return BadRequest(validatorResult.Errors);
        
        var createProjectResult = _unitOfWork.Project.Create(Project.From(request));

        if (createProjectResult.IsSuccessful)
        {
            _unitOfWork.Save();
            return GetProjectCreatedAt(createProjectResult.Payload);
        }
        if (createProjectResult is AlreadyExistsResult<Project> alreadyExistsResult)
        {
            return Conflict(alreadyExistsResult.Message);
        }

        return BadRequest();
    }

    [HttpPut("Update/{id:guid}")]
    public IActionResult UpdateProject(Guid id, UpdateProjectRequest request)
    {
        var validatorResult = ValidatorResultUtility.Validate(request, new UpdateProjectValidator());
        if (validatorResult.IsUnsuccessful) return BadRequest(validatorResult.Errors);
        
        var updateProjectResult = _unitOfWork.Project.Update(Project.From(id, request));

        if (updateProjectResult.IsSuccessful)
        {
            return Ok(updateProjectResult);
        }
        if (updateProjectResult is NotFoundResult<Project> notFoundResult)
        {
            return NotFound(notFoundResult.Message);
        }

        return BadRequest();
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
        if (deleteProjectResult is NotFoundResult notFoundResult)
        {
            return NotFound(notFoundResult.Message);
        }

        return BadRequest();
    }

    [HttpPost("AssignUser/{userId:guid}/{projectId:guid}")]
    public IActionResult AssignUserToProject(UsersProjectsRequest request)
    {
        var assignUserResult = _unitOfWork.Project.AssignUserToProject(request);
        
        if (assignUserResult is NotFoundResult notFoundResult)
        {
            return BadRequest(notFoundResult.Message);
        }
        
        if (!assignUserResult.IsSuccessful) return BadRequest();

        _unitOfWork.Save();
        return Ok();
    }
    
    [HttpPost("RemoveUser/{userId:guid}/{projectId:guid}")]
    public IActionResult RemoveUserToProject(UsersProjectsRequest request)
    {
        var removeUserResult = _unitOfWork.Project.RemoveUserFromProject(request);
        
        if (removeUserResult is NotFoundResult notFoundResult)
        {
            return BadRequest(notFoundResult.Message);
        }
        
        if (!removeUserResult.IsSuccessful) return BadRequest();

        _unitOfWork.Save();
        return Ok();
    }

    [HttpPost("GetUsers/{projectId:guid}")]
    public IActionResult GetUsersInProject(Guid projectId)
    {
        var getResult = _unitOfWork.Project.GetUsersInProject(projectId);
        if (getResult.IsSuccessful)
        {
            return Ok(MapUserResponse(getResult.Payload));
        }

        if (getResult is NotFoundResult<List<User>> notFoundResult)
        {
            return BadRequest(notFoundResult.Message);
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

    private static UserResponse MapUserResponse(User user)
    {
        return new UserResponse(user.Email, user.UserName);
    }
    
    private static List<UserResponse> MapUserResponse(List<User> users)
    {
        return users.ConvertAll(MapUserResponse);
    }
    
    private CreatedAtActionResult GetProjectCreatedAt(Project project)
    {
        return CreatedAtAction(
            actionName: nameof(GetProject),
            routeValues: new { id = project.Id },
            value: MapProjectResponse(project));
    }
}