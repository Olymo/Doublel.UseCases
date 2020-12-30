
namespace Doublel.UseCases
{
    public class LoggingUseCaseDecorator<TUseCase, TData, TResult> : UseCaseHandlingDecorator<TUseCase, TData, TResult>
        where TUseCase : UseCase<TData, TResult>
    {
        private readonly IApplicationActor _actor;
        private readonly IUseCaseLogRepository _repository;
        public LoggingUseCaseDecorator(IUseCaseHandler<TUseCase, TData, TResult> decoratee, IApplicationActor actor, IUseCaseLogRepository repository) : base(decoratee)
        {
            _actor = actor;
            _repository = repository;
        }

        public override TResult Handle(TUseCase useCase)
        {
            if (useCase.ShouldBeLogged)
            {
                _repository.Log(new UseCaseLog
                {
                    ActorId = _actor.Identifier,
                    ActorIdentity = _actor.Identity,
                    Id = useCase.UseCaseInstanceId,
                    Status = UseCaseExecutionStatus.Pending,
                    UseCaseData = useCase.Data,
                    UseCaseName = useCase.Name
                });
            }

            return Decoratee.Handle(useCase);
        }
    }
}
