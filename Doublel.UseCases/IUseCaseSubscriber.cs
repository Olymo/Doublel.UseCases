namespace Doublel.UseCases
{
    public interface IUseCaseSubscriber<TUseCase, TData, TResult>
        where TUseCase : UseCase<TData, TResult>
    {
        void Notify(UseCaseExecutedEvent<TData> @event);
    }

    public class UseCaseExecutedEvent<TData>
    {
        public IApplicationActor Actor { get; set; }
        public TData UseCaseData { get; set; }
        public int UseCaseId { get; set; }
        public string UseCaseName { get; set; }
    }
}
