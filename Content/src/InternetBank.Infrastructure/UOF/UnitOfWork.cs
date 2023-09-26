using InternetBank.Domain.Interfaces.UOF;

namespace InternetBank.Infrastructure.UOF;

public class UnitOfWork : IUnitOfWork
{
    public Task DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
