using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using DB_API_TEST.Models.Data;

namespace DB_API_TEST.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public JwtMiddleware(RequestDelegate next, IConfiguration config)
        {
            this._next = next;
            this._config = config;
        }

        public Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["JWT:KEY"]);

                try
                {
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "user_id").Value);
                    var userName = jwtToken.Claims.First(x => x.Type == "user_name").Value;
                    var userRole = jwtToken.Claims.First(x => x.Type == "user_role").Value;

                    context.Items["LoggedUser"] = new User()
                    {
                        UserId = userId,
                        Username = userName,
                        Userrole = userRole
                    };
                }
                catch { }
            }
            return _next(context);
        }
    }
}
