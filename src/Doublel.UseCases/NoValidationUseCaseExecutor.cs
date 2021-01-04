using System.Collections.Generic;

namespace Doublel.UseCases
{
    public class NoValidationUseCaseExecutor<TUseCase, TData, TResult> :  UseCaseExecutor<TUseCase, TData, TResult>
        where TUseCase : UseCase<TData, TResult>
    {

        private readonly IUseCaseLogRepository _logsRepository;
        private readonly IEnumerable<IUseCaseSubscriber<TUseCase, TData, TResult>> _subscribers;
        private readonly IApplicationActor _actor;

        public NoValidationUseCaseExecutor(IUseCaseLogRepository repo, IEnumerable<IUseCaseSubscriber<TUseCase, TData, TResult>> subscribers, IApplicationActor actor)
        {
            _logsRepository = repo;
            _subscribers = subscribers;
            _actor = actor;
        }

        protected override IUseCaseHandler<TUseCase, TData, TResult> MakeUseCaseHandlingDecoration(IUseCaseHandler<TUseCase, TData, TResult> handler)
        {
            var loggingDecorator = new LoggingUseCaseDecorator<TUseCase, TData, TResult>(handler, _actor, _logsRepository);
            var authorizationDecorator = new AuthorizationUseCaseDecorator<TUseCase, TData, TResult>(loggingDecorator, _actor);
            var postProcessingDecorator = new PostProcessingUseCaseDecorator<TUseCase, TData, TResult>(authorizationDecorator, _actor, _subscribers, _logsRepository);
            return postProcessingDecorator;
        }
    }
}
