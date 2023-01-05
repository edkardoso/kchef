using System;
using FluentValidation;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common
{
    public abstract class UseCase<TInput, TOutput>
    {
        private readonly AbstractValidator<TInput> _validator;
        private readonly IPresenter<TInput, TOutput> _presenter;
        private bool _isValid = true;
        private bool _complete;
        private ValidationResult _validationResult;

    
        public UseCase(AbstractValidator<TInput> validator = default, IPresenter<TInput, TOutput> presenter = null)
        {
            _validator = validator;
            _presenter = presenter;
        }

        public IPresenter<TInput, TOutput> Execute(TInput input)
        {
            try
            {
                if (_validator != null)
                {
                    _validationResult = _validator.Validate(input);
                    _isValid = _validationResult.IsValid;
                }

                if (_isValid)
                {
                    var result = OnExecute(input, _validationResult);
                    _complete = true;
                    _presenter.OnSuccess(result);
                }
                else
                {
                    _presenter.OnError(input, _validationResult);
                }

            }
            catch (Exception ex)
            {
                // Pode ser necessário adicionar um novo evento virtual de Exceção
                // para cenários onde há a necessidade de se desfazer alguma ação anterior
                // em caso de exceção. Embora o OnComplete(false) talvez já permita isso.

                _presenter.OnException(input, ex);

            }
            finally
            {
                OnComplete(_complete);
            }

            return _presenter;

        }

        public abstract TOutput OnExecute(TInput input, ValidationResult validationResult);


        public virtual void OnComplete(bool completed)
        {
            return;
        }
    }
}
