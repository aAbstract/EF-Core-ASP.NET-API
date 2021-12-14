using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using DB_API_TEST.Database;
using DB_API_TEST.Models.Data;
using DB_API_TEST.Models.Web;

namespace DB_API_TEST.Serives.MedClients
{
    public static class AddUser
    {
        public record Request(SignupInfo SignupInfo) : IRequest<Response>;

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly TestDbContext _context;

            public Handler(TestDbContext context, IConfiguration config)
            {
                this._context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                this._context.Add(new User()
                {
                    Username = $"{request.SignupInfo.FirstName.ToLower()}-{request.SignupInfo.LastName.ToLower()}",
                    Userrole = "user"
                });
                int cmdCode = await this._context.SaveChangesAsync();

                return new Response(cmdCode);
            }
        }

        public record Response(int CmdCode);
    }
}
