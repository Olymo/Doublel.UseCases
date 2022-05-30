using Doublel.UseCases.Exceptions;
using FluentValidation;
using System.Linq;

namespace Doublel.UseCases
{
    public class ValidationUseCaseDecorator<TUseCase, TData, TResult> : UseCaseHandlingDecorator<TUseCase, TData, TResult>
        where TUseCase : UseCase<TData, TResult>
    {
        private readonly IValidator<TUseCase> _validator;
        private readonly IUseCaseLogRepository _useCaseRepository;
        public ValidationUseCaseDecorator(IUseCaseHandler<TUseCase, TData, TResult> decoratee, IValidator<TUseCase> validator, IUseCaseLogRepository useCaseRepository) : base(decoratee)
        {
            _validator = validator;
            _useCaseRepository = useCaseRepository;
        }

        public override TResult Handle(TUseCase useCase)
        {
            var result = _validator.Validate(useCase);

            if (!result.IsValid)
            {
                _useCaseRepository.UpdateStatus(useCase.UseCaseInstanceId, UseCaseExecutionStatus.FailedDueToValidation);
                throw new UseCaseValidationException(result.Errors.Select(x => new UseCaseValidationError
                {
                    Error = x.ErrorMessage,
                    PropertyName = x.PropertyName.Split('.').Count() > 1 ? x.PropertyName.Split('.')[1] : x.PropertyName.Split('.')[0]
                }));
            }

            return Decoratee.Handle(useCase);
        }
    }
}
