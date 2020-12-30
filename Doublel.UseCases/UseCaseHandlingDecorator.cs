
namespace Doublel.UseCases
{
    public abstract class UseCaseHandlingDecorator<TUseCase, TData, TResult> : IUseCaseHandler<TUseCase, TData, TResult>
        where TUseCase : UseCase<TData, TResult>
    {
        protected readonly IUseCaseHandler<TUseCase, TData, TResult> Decoratee;

        protected UseCaseHandlingDecorator(IUseCaseHandler<TUseCase, TData, TResult> decoratee) => Decoratee = decoratee;

        public abstract TResult Handle(TUseCase useCase);
    }
}
