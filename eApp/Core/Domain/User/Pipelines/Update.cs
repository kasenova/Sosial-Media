using eApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.Core.Domain.User.Pipelines;

public class Update 
{
    public record Request(string Username, string Name, string Apartment, int Zipcode) : IRequest<Response>;

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
            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            // Detach the existingUser from the DbContext
            _db.Entry(existingUser).State = EntityState.Detached;

            var updatedUser = new Users
            (request.Username,
            existingUser.Password, 
            request.Name ?? existingUser.Name, 
            request.Apartment ?? existingUser.Apartment,
            request.Zipcode);

            _db.Users.Update(updatedUser);
            await _db.SaveChangesAsync(cancellationToken);
            
            return new Response(true, "Updated information");
        }
    }
}