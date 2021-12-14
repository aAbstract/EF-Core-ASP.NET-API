using MediatR;
using DB_API_TEST.Models.Data;
using DB_API_TEST.Database;
using Microsoft.EntityFrameworkCore;

namespace DB_API_TEST.Serives.MedClients.DbQueries
{
    public static class GetAllCustomers
    {
        public record Request() : IRequest<Response>;

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly TestDbContext _context;

            public Handler(TestDbContext context)
            {
                this._context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                return new Response(await this._context.Customers.ToListAsync());
            }
        }

        public record Response(IEnumerable<Customer> CustomersList);
    }
}
