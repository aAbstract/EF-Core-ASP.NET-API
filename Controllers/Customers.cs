using MediatR;
using Microsoft.AspNetCore.Mvc;

using DB_API_TEST.Event;
using DB_API_TEST.Serives.MedClients.DbQueries;
using DB_API_TEST.Models.Data;
using DB_API_TEST.Models.Web;

namespace DB_API_TEST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customers : Controller
    {
        private readonly IMediator _mediator;

        public Customers(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCustomers()
        {
            // check logged user
            User loggedUser = (User)(HttpContext.Items["LoggedUser"]);
            if (loggedUser == null)
                return Unauthorized();
            
            Console.WriteLine($"Logged in user: {loggedUser.Userrole}|{loggedUser.Username}");

            if (loggedUser.Userrole != "admin")
            {
                Response.StatusCode = StatusCodes.Status403Forbidden;
                return new JsonResult(new JSONMsg(StatusCode: 403, Msg: "Only Admin Users Can Access This API Endpoint"));
            }
            
            var data = await this._mediator.Send(new GetAllCustomers.Request()); // use mediator to commuincate with database service
            EventManager.PostEvent("GetAllCustomersAPIHit", null); // trigger event in the system
            return Ok(data);
        }
    }
}