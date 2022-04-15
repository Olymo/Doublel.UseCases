using System;
using System.Linq;

namespace Doublel.UseCases
{
    internal class AuthorizationUseCaseDecorator<TUseCase, TData, TResult> : UseCaseHandlingDecorator<TUseCase, TData, TResult>
        where TUseCase : UseCase<TData, TResult>
    {
        private readonly IApplicationActor _actor;

        public AuthorizationUseCaseDecorator(IUseCaseHandler<TUseCase, TData, TResult> decoratee, IApplicationActor actor) 
            : base(decoratee) => _actor = actor;

        public override TResult Handle(TUseCase useCase)
        {
            if (_actor.AllowedUseCases.Contains(useCase.Id) || _actor.IsAdmin)
            {
                return Decoratee.Handle(useCase);
            }

            throw new UnauthorizedAccessException($"Actor {_actor.Identity} with an Id of {_actor.Identifier} has tried to execute {useCase.Id} use case without beeing authorized to do so.");
        }
    }
}
