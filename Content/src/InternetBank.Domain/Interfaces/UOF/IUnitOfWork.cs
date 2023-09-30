namespace InternetBank.Domain.Interfaces.UOF;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
    Task DisposeAsync();
}