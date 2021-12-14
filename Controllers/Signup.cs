using MediatR;
using Microsoft.AspNetCore.Mvc;

using DB_API_TEST.Serives.MedClients;
using DB_API_TEST.Models.Web;
using DB_API_TEST.Event;
using DB_API_TEST.Event.ArgsModels;

namespace DB_API_TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Signup : Controller
    {
        private readonly IMediator _mediator;

        public Signup(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> GenerateSecureToken([FromBody] SignupInfo signupInfo)
        {
            EventManager.PostEvent("UserAdded", new UserAddedArgs(FirstName: signupInfo.FirstName, LastName: signupInfo.LastName)); // trigger event in the system
            var cmdCode = await this._mediator.Send(new AddUser.Request(signupInfo)); // use mediator to commuincate with database service            
            return Ok(new JsonResult(new JSONMsg(StatusCode: 200, Msg: "User Added to Database")));
        }
    }
}