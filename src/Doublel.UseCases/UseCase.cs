using System;

namespace Doublel.UseCases
{
    public abstract class UseCase<TData, TResult>
    {
        protected UseCase(TData data) => Data = data;

        public abstract string Id { get; }
        public abstract string Description { get; }
        public virtual TData Data { get; private set; }
        public virtual Guid UseCaseInstanceId { get; set; } = Guid.NewGuid();
        public virtual bool ShouldBeLogged => true;
    }
}
