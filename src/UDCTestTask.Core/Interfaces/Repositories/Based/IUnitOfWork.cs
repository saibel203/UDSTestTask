using UDCTestTask.Core.Interfaces.Repositories.Main;

namespace UDCTestTask.Core.Interfaces.Repositories.Based;

public interface IUnitOfWork : IDisposable
{
    public IEmployeeRepository EmployeeRepository { get; }

    void SaveChanges();
    Task SaveChangesAsync();
}