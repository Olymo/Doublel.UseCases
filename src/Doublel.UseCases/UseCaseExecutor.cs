namespace Doublel.UseCases
{
    public abstract class UseCaseExecutor<TUseCase, TData, TResult>
        where TUseCase : UseCase<TData, TResult>
    {
        public TResult ExecuteUseCase(TUseCase useCase, IUseCaseHandler<TUseCase, TData, TResult> handler)
        {
            var decoration = MakeUseCaseHandlingDecoration(handler);

            return decoration.Handle(useCase);
        }

        protected abstract IUseCaseHandler<TUseCase, TData, TResult> MakeUseCaseHandlingDecoration(IUseCaseHandler<TUseCase, TData, TResult> handler);
    }
}
