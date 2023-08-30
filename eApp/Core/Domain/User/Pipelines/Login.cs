using eApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.Core.Domain.User.Pipelines;

public class Login
{
    public record Request (string Username, string Password) : IRequest<Response>;

    public record Response (bool Success, string Error);

    public class Handler : IRequestHandler<Request, Response>
    {
        public readonly DataBase _db;

        public Handler(DataBase db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
    
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);
            if (user != null)
            {
                return new Response(true, "Success");
            }
            return new Response(false, "Username or Password do not exist");
        }
    }
}