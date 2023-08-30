using MediatR;
using Microsoft.EntityFrameworkCore;
using eApp.Core.Domain.User;
using eApp.Core.Domain.Posts;
using eApp.SharedKernel;

namespace eApp.Infrastructure.Data;

public class DataBase : DbContext
{
    private readonly IMediator _mediator;

    public DataBase (DbContextOptions options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

    public DbSet<Users> Users {get; set;} = null!;
    public DbSet<Posts> Posts {get; set;} = null!;
    public DbSet<Comments> Comments {get; set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_mediator == null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entitiesWithEvents)
        {

            var events = entity.Events.ToArray();
            entity.Events.Clear();
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }
        }
        return result;
    }

    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
}