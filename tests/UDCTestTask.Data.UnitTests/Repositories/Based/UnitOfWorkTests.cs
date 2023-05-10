using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using UDCTestTask.Core.Interfaces.Repositories.Based;
using UDCTestTask.Data.Repositories.Based;
using UDCTestTask.Infrastructure.DatabaseContext;
using UDCTestTask.Shared;
using UDCTestTask.WebAPI.Mapping;
using Xunit;

namespace UDCTestTask.Data.UnitTests.Repositories.Based;

public class UnitOfWorkTests : TestBase<UnitOfWork>
{
    [Fact]
    public void SaveChanges_SuccessSaveChanges_SaveChanges()
    {
        // Arrange
        Mock<TaskApplicationDbContext> mockApplicationDbContext =
            UnitOfWorkConfiguration.CreateMockApplicationDbContext();

        Mock<ILoggerFactory> loggerFactoryMock = new Mock<ILoggerFactory>();
        
        EmployeeMapperProfile employeeProfile = new EmployeeMapperProfile();
        var configuration = new MapperConfiguration(cfg => 
            cfg.AddProfile(employeeProfile));
        IMapper mapper = new Mapper(configuration);

        IUnitOfWork unitOfWork = new UnitOfWork(mockApplicationDbContext.Object, loggerFactoryMock.Object, mapper);

        // Act
        unitOfWork.SaveChanges();

        // Assert
        mockApplicationDbContext.Verify(options => options.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task SaveChangesAsync_SuccessSaveChangesAsync_SaveChangesAsync()
    {
        // Arrange
        Mock<TaskApplicationDbContext> mockApplicationDbContext =
            UnitOfWorkConfiguration.CreateMockApplicationDbContext();

        Mock<ILoggerFactory> loggerFactoryMock = new Mock<ILoggerFactory>();
        
        EmployeeMapperProfile employeeProfile = new EmployeeMapperProfile();
        var configuration = new MapperConfiguration(cfg => 
            cfg.AddProfile(employeeProfile));
        IMapper mapper = new Mapper(configuration);

        IUnitOfWork unitOfWork = new UnitOfWork(mockApplicationDbContext.Object, loggerFactoryMock.Object, mapper);
    
        // Act
        await unitOfWork.SaveChangesAsync();
    
        // Assert
        mockApplicationDbContext.Verify(options => 
            options.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
