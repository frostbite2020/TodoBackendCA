using Application.Common.Models;
using Infrastructure.Identity.Commands.CreateRegister;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Controllers
{

    public class ApplicationUserController : ApiControllerBase
    {
        public ApplicationUserController()
        {
        }
        [HttpPost]
        [Route("Register")]
        public async Task<(Result, string)> CreateRegister(CreateRegisterCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
