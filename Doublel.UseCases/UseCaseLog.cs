using System;

namespace Doublel.UseCases
{
    public enum UseCaseExecutionStatus
    {
        Pending,
        Successfull,
        FailedDueToValidation,
        Failed
    }

    public class UseCaseLog
    {
        public object UseCaseData { get; set; }
        public DateTime ExecutedTime { get; set; } = DateTime.UtcNow;
        public string ActorIdentity { get; set; }
        public object ActorId { get; set; }
        public UseCaseExecutionStatus Status { get; set; }
        public Guid Id { get; set; }
        public string UseCaseName { get; set; }
    }
}
