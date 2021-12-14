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
    public static class GenAuthToken
    {
        public record Request(LoginInfo loginInfo) : IRequest<Response>;

        private static string generateJwtToken(User user, string secret)
        {
            var claims = new List<Claim>
            {
                new Claim("user_id", user.UserId.ToString()),
                new Claim("user_name", user.Username),
                new Claim("user_role", user.Userrole)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                signingCredentials: creds,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly TestDbContext _context;
            private readonly IConfiguration _config;

            public Handler(TestDbContext context, IConfiguration config)
            {
                this._context = context;
                this._config = config;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var userLinq = from userDbObj in this._context.Users
                            where userDbObj.Username == request.loginInfo.Username
                            select userDbObj;

                var user = await userLinq.SingleOrDefaultAsync();

                if (user == null)
                    return null;
                
                return new Response(GenAuthToken.generateJwtToken(user, this._config["JWT:KEY"]));
            }
        }

        public record Response(string token);
    }
}
