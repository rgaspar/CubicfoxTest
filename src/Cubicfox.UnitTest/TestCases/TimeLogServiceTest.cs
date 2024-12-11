using System.Linq.Expressions;
using Cubicfox.Application.Repository;
using Cubicfox.Application.Service.TimeLog.Service;
using Cubicfox.Domain.Common.Exceptions;
using Cubicfox.Domain.Common.Models;
using Cubicfox.Domain.Common.Repository.TimeLogsRepository;
using Cubicfox.Domain.Common.Request.TimeLog;
using Cubicfox.Domain.Common.Response.TimeLog;
using Cubicfox.Domain.Entities;
using Cubicfox.UnitTest.Helper;
using Moq;

namespace Cubicfox.Unittest.TestCases;

public class TimeLogServiceTest
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private TimeLogService _timeLogService;
    private Mock<ITimeLogRepository> _timeLogRepository = new();
    
    [Fact]
    public async Task TimeLogService_GetById_ShouldReturnATimeLog()
    {
        // Arrange
        var expect = new TimeLog()
        {
            Id = Guid.NewGuid(),
            Description = "Test",
            StartDate = DateTime.Now,
            EndDate = null,
            IsDeleted = false,
        };
        
        _timeLogRepository.Setup(u => u.GetById(It.IsAny<Guid>(), default))
            .ReturnsAsync(expect);
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act
        var actualResult = await _timeLogService.FindAsync(expect.Id, default);

        // Assert
        Assert.Equal(expect.Id, actualResult.Id);
        Assert.Equal(expect.Description, actualResult.Description);
        Assert.Equal(expect.StartDate, actualResult.StartDate);
        Assert.Equal(expect.EndDate, actualResult.EndDate);
        Assert.Equal(expect.IsDeleted, actualResult.IsDeleted);
    }
    
    [Fact]
    public async Task TimeLogService_StartTime_ShouldAddTimeLog()
    {
        // Arrange
        var expected = TimeLogHelper.TestTimeLog();
        var request = new StartTimeLogRequest(expected.Description);
        
        _timeLogRepository.Setup(u => u.Create(It.IsAny<TimeLog>()));
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act
        var result = await _timeLogService.StartAsync(request,  CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.Save(CancellationToken.None), Times.Once);
    }
    
    [Fact]
    public async Task TimeLogService_StartTimeOneTimerIsAlreadyRunnning_NotAddTimeLogReturnException()
    {
        // Arrange
        var expected = TimeLogHelper.TestTimeLog();
        var request = new StartTimeLogRequest(expected.Description);

        _timeLogRepository.Setup(u => u.Create(It.IsAny<TimeLog>()));
        _timeLogRepository.Setup(u => u.AllTimeLogIsStopped(It.IsAny<CancellationToken>())).ReturnsAsync(TimeLogHelper.TestTimeLog());
        
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act - Assert
        await Assert.ThrowsAsync<CubicfoxException>(() => _timeLogService.StartAsync(request,  CancellationToken.None));
    }
    
    [Fact]
    public async Task TimeLogService_StopTime_ShouldUpdateTimeLogAndFillEndDate()
    {
        // Arrange
        var expected = TimeLogHelper.TestTimeLog();
        var request = new StopTimeLogRequest(expected.Id);
        
        _timeLogRepository.Setup(u => u.Create(It.IsAny<TimeLog>()));
        _timeLogRepository.Setup(u => u.GetById(expected.Id, It.IsAny<CancellationToken>())).ReturnsAsync(expected);
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act
        var result = await _timeLogService.StopAsync(request,  CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.Save(CancellationToken.None), Times.Once);
        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Description, result.Description);
        Assert.Equal(expected.StartDate, result.StartDate);
        Assert.NotNull(result.EndDate);
        Assert.Equal(expected.IsDeleted, result.IsDeleted);
    }
    
    [Fact]
    public async Task TimeLogService_StopTime_RecordNotExistsThrowException()
    {
        // Arrange
        var expected = TimeLogHelper.TestTimeLog();
        var request = new StopTimeLogRequest(expected.Id);
        
        _timeLogRepository.Setup(u => u.Create(It.IsAny<TimeLog>()));
        _timeLogRepository.Setup(u => u.GetById(expected.Id, It.IsAny<CancellationToken>())).ReturnsAsync((TimeLog)null);
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act - Assert
        await Assert.ThrowsAsync<CubicfoxException>(() => _timeLogService.StopAsync(request,  CancellationToken.None));    
    }
    
    [Fact]
    public async Task TimeLogService_Update_ShouldUpdateTimeLog()
    {
        // Arrange
        var expected = TimeLogHelper.TestTimeLog();
        var request = new UpdateTimeLogRequest(expected.Id, expected.Description);
        
        _timeLogRepository.Setup(u => u.Create(It.IsAny<TimeLog>()));
        _timeLogRepository.Setup(u => u.GetById(expected.Id, It.IsAny<CancellationToken>())).ReturnsAsync(expected);
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act
        var result = await _timeLogService.UpdateAsync(request,  CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.Save(CancellationToken.None), Times.Once);
        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Description, result.Description);
        Assert.Equal(expected.StartDate, result.StartDate);
        Assert.Null(result.EndDate);
        Assert.Equal(expected.IsDeleted, result.IsDeleted);
    }
    
    [Fact]
    public async Task TimeLogService_Update_RecordNotExistsThrowException()
    {
        // Arrange
        var expected = TimeLogHelper.TestTimeLog();
        var request = new UpdateTimeLogRequest(expected.Id, expected.Description);
        
        _timeLogRepository.Setup(u => u.Create(It.IsAny<TimeLog>()));
        _timeLogRepository.Setup(u => u.GetById(expected.Id, It.IsAny<CancellationToken>())).ReturnsAsync((TimeLog)null);
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act - Assert
        await Assert.ThrowsAsync<CubicfoxException>(() => _timeLogService.UpdateAsync(request,  CancellationToken.None));    
    }
    
    [Fact]
    public async Task TimeLogService_Delete_ShouldUpdateTimeLogIsDeletedToTrue()
    {
        // Arrange
        var expected = TimeLogHelper.TestTimeLog();
        var expectedResult = TimeLogHelper.CreateFrom(expected);
        
        _timeLogRepository.Setup(u => u.Create(It.IsAny<TimeLog>()));
        _timeLogRepository.Setup(u => u.GetById(expected.Id, It.IsAny<CancellationToken>())).ReturnsAsync(expectedResult);
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act
        var result = await _timeLogService.DeleteAsync(expected.Id,  CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.Save(CancellationToken.None), Times.Once);
        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Description, result.Description);
        Assert.Equal(expected.StartDate, result.StartDate);
        Assert.Null(result.EndDate);
        Assert.NotEqual(expected.IsDeleted, result.IsDeleted);
    }
    
    [Fact]
    public async Task TimeLogService_Delete_RecordNotExistsThrowException()
    {
        // Arrange
        var expected = TimeLogHelper.TestTimeLog();
        
        _timeLogRepository.Setup(u => u.Create(It.IsAny<TimeLog>()));
        _timeLogRepository.Setup(u => u.GetById(expected.Id, It.IsAny<CancellationToken>())).ReturnsAsync((TimeLog)null);
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act - Assert
        await Assert.ThrowsAsync<CubicfoxException>(() => _timeLogService.DeleteAsync(expected.Id,  CancellationToken.None));    
    }
    
    [Fact]
    public async Task TimeLogService_GetAll_ReturnsAllTimeLogs()
    {
        // Arrange
        var expectedResult = new Pagination<TimeLogResponse>(TimeLogHelper.TestTimeLogsResponse(), 2, 0, 2);
        var request = new GetAllTimeLogRequest(DateTime.Now, DateTime.Now.AddDays(1), 0, 2);
        _timeLogRepository.Setup(u => u.ToPagination(It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<Expression<Func<TimeLog, bool>>>(),
            It.IsAny<Func<IQueryable<TimeLog>, IQueryable<TimeLog>>>(),
            It.IsAny<Expression<Func<TimeLog, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<Expression<Func<TimeLog, TimeLogResponse>>>())).ReturnsAsync(expectedResult);
        _timeLogService = new TimeLogService(_unitOfWorkMock.Object, _timeLogRepository.Object);
        
        // Act 
        var actualResult = await _timeLogService.GetAllAsync(request);
        
        // Assert
        Assert.NotNull(actualResult);
        Assert.Equal(expectedResult.CurrentPage, actualResult.CurrentPage);
        Assert.Equal(expectedResult.TotalPages, actualResult.TotalPages);
        Assert.Equal(expectedResult.PageSize, actualResult.PageSize);
        Assert.Equal(expectedResult.TotalCount, actualResult.TotalCount);
        Assert.Equal(expectedResult.HasPrevious, actualResult.HasPrevious);
        Assert.Equal(expectedResult.HasNext, actualResult.HasNext);
        Assert.Equal(expectedResult.Items?.Count, actualResult.Items?.Count);

        var expect = expectedResult.Items?.ToList();
        var actual = actualResult.Items?.ToList();

        for (int i = 0; i < expectedResult.Items?.Count; i++)
        {
            Assert.Equal(expect?[i].Id, actual?[i].Id);
            Assert.Equal(expect?[i].StartDate, actual?[i].StartDate);
            Assert.Equal(expect?[i].Description, actual?[i].Description);
            Assert.Equal(expect?[i].EndDate, actual?[i].EndDate);
            Assert.Equal(expect?[i].IsDeleted, actual?[i].IsDeleted);
        }
    }
}
