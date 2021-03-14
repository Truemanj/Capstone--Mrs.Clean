using MrsCleanCapstone.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Controllers.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Employee employee);
    }
}
