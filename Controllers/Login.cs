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
    public class Login : Controller
    {
        private readonly IMediator _mediator;

        public Login(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> GenerateSecureToken([FromBody] LoginInfo loginInfo)
        {
            EventManager.PostEvent("UserLoggedIn", new UserLoggedArgs(UserName: loginInfo.Username)); // trigger event in the system
            var data = await this._mediator.Send(new GenAuthToken.Request(loginInfo)); // use mediator to commuincate with token service
            return data == null ? Unauthorized() : Ok(new AuthToken(Token: data.token));
        }
    }
}