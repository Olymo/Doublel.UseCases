using FluentValidation;
using System.Collections.Generic;

namespace Doublel.UseCases
{
    public class DefaultUseCaseExecutor<TUseCase, TData, TResult> : UseCaseExecutor<TUseCase, TData, TResult>
        where TUseCase : UseCase<TData, TResult>
    {
        private readonly IUseCaseLogRepository _logsRepository;
        private readonly IEnumerable<IUseCaseSubscriber<TUseCase, TData, TResult>> _subscribers;
        private readonly IValidator<TUseCase> _validator;
        private readonly IApplicationActor _actor;

        public DefaultUseCaseExecutor(
            IUseCaseLogRepository repo, 
            IEnumerable<IUseCaseSubscriber<TUseCase, TData, TResult>> subscribers, 
            IValidator<TUseCase> validator, 
            IApplicationActor actor)
        {
            _logsRepository = repo;
            _subscribers = subscribers;
            _validator = validator;
            _actor = actor;
        }

        protected override IUseCaseHandler<TUseCase, TData, TResult> MakeUseCaseHandlingDecoration(IUseCaseHandler<TUseCase, TData, TResult> handler)
        {
            var validationDecorator = new ValidationUseCaseDecorator<TUseCase, TData, TResult>(handler, _validator, _logsRepository);
            var loggingDecorator = new LoggingUseCaseDecorator<TUseCase, TData, TResult>(validationDecorator, _actor, _logsRepository);
            var authorizationDecorator = new AuthorizationUseCaseDecorator<TUseCase, TData, TResult>(loggingDecorator, _actor);
            var postProcessingDecorator = new PostProcessingUseCaseDecorator<TUseCase, TData, TResult>(authorizationDecorator, _actor, _subscribers, _logsRepository);
            return postProcessingDecorator;
        }
    }
}
