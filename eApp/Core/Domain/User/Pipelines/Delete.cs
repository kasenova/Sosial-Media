using eApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.Core.Domain.User.Pipelines;

public class Delete 
{
    public record Request(string Username) : IRequest<Response>;

    public record Response(bool Success, string Error);

    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly DataBase _db;

        public Handler(DataBase db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            _db.Users.Remove(user);
            await _db.SaveChangesAsync(cancellationToken);
            
            return new Response(true, "Updated information");
        }
    }
}