using eApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eApp.Core.Domain.User.Pipelines;

public class Register 
{
    public record Request(string Username, string Password, string Name, string Apartment, int Zipcode) : IRequest<Response>;

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
            if (user != null)
            {
                return new Response(false, "Username is already taken.");
            }

            user = new Users(request.Username, request.Password, request.Name, request.Apartment, request.Zipcode);
            //if(user.Username =="")
            //if(user.Password =="")
            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);

            return new Response(true, "User Successfully created");
        }
    }
}