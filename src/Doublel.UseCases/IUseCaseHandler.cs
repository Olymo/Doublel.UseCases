namespace Doublel.UseCases
{
    public interface IUseCaseHandler<UseCase, TData, out TResult>
        where UseCase : UseCase<TData, TResult>
    {
        TResult Handle(UseCase useCase);
    }
}
