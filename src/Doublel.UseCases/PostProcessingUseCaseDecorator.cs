using System;
using System.Collections.Generic;
using System.Linq;

namespace Doublel.UseCases
{
    public class PostProcessingUseCaseDecorator<TUseCase, TData, TResult> : UseCaseHandlingDecorator<TUseCase, TData, TResult>
     where TUseCase : UseCase<TData, TResult>
    {
        private readonly IApplicationActor _actor;
        private readonly IEnumerable<IUseCaseSubscriber<TUseCase, TData, TResult>> _subscribers;
        private readonly IUseCaseLogRepository _repository;

        public PostProcessingUseCaseDecorator(
            IUseCaseHandler<TUseCase, TData, TResult> decoratee,
            IApplicationActor actor,
            IEnumerable<IUseCaseSubscriber<TUseCase, TData, TResult>> subscribers,
            IUseCaseLogRepository repository) : base(decoratee)
        {
            _actor = actor;
            _subscribers = subscribers;
            _repository = repository;
        }

        public override TResult Handle(TUseCase useCase)
        {
            TResult result;

            try
            {
                result = Decoratee.Handle(useCase);

                var @event = new UseCaseExecutedEvent<TData>
                {
                    Actor = _actor,
                    UseCaseData = useCase.Data,
                    UseCaseId = useCase.Id,
                    UseCaseName = useCase.Name
                };

                if (_subscribers != null && _subscribers.Any())
                {
                    foreach (var subscriber in _subscribers)
                    {
                        subscriber.Notify(@event);
                    }
                }

                _repository.UpdateStatus(useCase.UseCaseInstanceId, UseCaseExecutionStatus.Successfull);

                return result;
            }
            catch (Exception ex)
            {
                _repository.UpdateStatus(useCase.UseCaseInstanceId, UseCaseExecutionStatus.Failed);
                throw ex;
            }
        }
    }
}
