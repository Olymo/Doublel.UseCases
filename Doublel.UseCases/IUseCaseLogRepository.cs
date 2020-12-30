using System;
namespace Doublel.UseCases
{
    public interface IUseCaseLogRepository
    {
        PagedResult<UseCaseLog> GetLogs(IUseCaseLogSearch search);
        void Log(UseCaseLog log);
        void UpdateStatus(Guid id, UseCaseExecutionStatus status);
    }

    public interface IUseCaseLogSearch
    {
        DateTime? StartDate { get; set; }
        DateTime? EndDate { get; set; }
        int? ActorId { get; set; }
        int? Status { get; set; }
        string UseCaseName { get; set; }
    }
}
