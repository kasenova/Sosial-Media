using eApp.Infrastructure.Data;
using MediatR;
using eApp.Core.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace eApp.Core.Domain.User.Pipelines;

public class GetAllUsers
{
	public record Request() : IRequest<List<Users>>;

	public class Handler : IRequestHandler<Request, List<Users>>
	{
		private readonly DataBase _db;
		public Handler(DataBase db)
		{
			_db = db ?? throw new System.ArgumentNullException(nameof(db));
		}

		public async Task<List<Users>> Handle(Request request, CancellationToken cancellationToken)
		{
			var user = await _db.Users.ToListAsync();
			return user;
		}
	}
}