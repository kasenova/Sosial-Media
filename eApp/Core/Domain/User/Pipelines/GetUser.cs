
using eApp.Infrastructure.Data;
using MediatR;
using eApp.Core.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace eApp.Core.Domain.User.Pipelines;

public class GetUser
{
	public record Request(string Username) : IRequest<Users>;

	public class Handler : IRequestHandler<Request, Users>
	{
		private readonly DataBase _db;
		public Handler(DataBase db)
		{
			_db = db ?? throw new System.ArgumentNullException(nameof(db));
		}

		public async Task<Users> Handle(Request request, CancellationToken cancellationToken)
		{
			var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username);
			return user;
		}
	}
}