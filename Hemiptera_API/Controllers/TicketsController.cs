using Hemiptera_API.Extensions;
using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_API.Utilitys;
using Hemiptera_API.Validators.Tickets;
using Hemiptera_Contracts.Projects.Responses;
using Hemiptera_Contracts.Tickets.Requests;
using Hemiptera_Contracts.Tickets.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Hemiptera_API.Controllers;

[Route("api/[controller]/")]
public class TicketsController : ControllerBase
{
    private IUnitOfWorkRepository _unitOfWork;

    public TicketsController(IUnitOfWorkRepository unitOfWorkService)
    {
        _unitOfWork = unitOfWorkService;
    }

    [HttpPost("Create")]
    public IActionResult CreateTicket(CreateTicketRequest request)
    {
        var validatorResult = ValidatorResultUtility.Validate(request, new CreateTicketValidator());
        if (validatorResult.IsUnsuccessful) return BadRequest(validatorResult.Errors);

        var createProjectResult = _unitOfWork.Ticket.Create(Ticket.From(request));

        if (createProjectResult.IsSuccessful)
        {
            _unitOfWork.Save();
            return GetTicketCreatedAt(createProjectResult.Payload);
        }
        if (createProjectResult is AlreadyExistsResult<Ticket> alreadyExistsResult)
        {
            return Conflict(alreadyExistsResult.Message);
        }

        return BadRequest();
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetTicket(Guid id)
    {
        var getTicketResult = _unitOfWork.Ticket.GetById(id);

        if (getTicketResult.IsSuccessful)
        {
            return Ok(MapTicketResponse(getTicketResult.Payload));
        }
        if (getTicketResult is NotFoundResult<Ticket> notFoundResult)
        {
            return NotFound(notFoundResult.Message);
        }

        return BadRequest();
    }

    [HttpGet("GetAll")]
    public IActionResult GetTickets()
    {
        var getTicketResult = _unitOfWork.Ticket.GetAll();
        if (getTicketResult.IsSuccessful)
        {
            return Ok(MapTicketResponse(getTicketResult.Payload));
        }
        if (getTicketResult is NotFoundResult<List<Ticket>> notFoundResult)
        {
            return NotFound(notFoundResult.Message);
        }
        return BadRequest();
    }

    // TO:DO Could make request DTO contain Guid ID
    [HttpPut("Update/{id:guid}")]
    public IActionResult UpdateTicket(Guid id, UpdateTicketRequest request)
    {
        var validatorResult = ValidatorResultUtility.Validate(request, new UpdateTicketValidator());
        if (validatorResult.IsUnsuccessful) return BadRequest(validatorResult.Errors);

        var updateTicketResult = _unitOfWork.Ticket.Update(Ticket.From(id, request));

        if (updateTicketResult.IsSuccessful)
        {
            _unitOfWork.Save();
            return Ok(MapTicketResponse(updateTicketResult.Payload));
        }
        if (updateTicketResult is NotFoundResult<Ticket> notFoundResult)
        {
            return NotFound(notFoundResult.Message);
        }
        return BadRequest();
    }

    private static TicketResponse MapTicketResponse(Ticket ticket)
    {
        return new TicketResponse(
            ticket.Title,
            ticket.Summary,
            ticket.Description,
            ticket.ReporterId,
            ticket.AssignedToId,
            ticket.Priority.DisplayString(),
            ticket.Status.DisplayString(),
            ticket.ProjectId);
    }

    private static List<TicketResponse> MapTicketResponse(List<Ticket> tickets)
    {
        return tickets.ConvertAll(MapTicketResponse);
    }

    private CreatedAtActionResult GetTicketCreatedAt(Ticket ticket)
    {
        return CreatedAtAction(
            actionName: nameof(GetTicket),
            routeValues: new { id = ticket.Id },
            value: MapTicketResponse(ticket));
    }
}