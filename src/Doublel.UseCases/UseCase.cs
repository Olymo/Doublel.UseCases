using System;

namespace Doublel.UseCases
{
    public abstract class UseCase<TData, TResult>
    {
        protected UseCase(TData data) => Data = data;

        public virtual string Id
        {
            get
            {
                var className = GetType().Name;

                return className.IndexOf("UseCase") > 0
                    ? className.Substring(0, className.IndexOf("UseCase"))
                    : className;
            }
        }
        public virtual string Description { get; }
        public virtual TData Data { get; private set; }
        public virtual Guid UseCaseInstanceId { get; set; } = Guid.NewGuid();
        public virtual bool ShouldBeLogged => true;
    }
}
