using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hemiptera_Contracts.Authentication.Responses;

public record AuthenticatedResponse(
    string AccessToken,
    Guid UserId);