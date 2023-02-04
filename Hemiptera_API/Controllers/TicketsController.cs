using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_Contracts.Tickets.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Hemiptera_API.Controllers;

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
        var createTicketResult = _unitOfWork.Ticket.Create(Ticket.From(request));
        _unitOfWork.Save();
        return Ok();
    }
}