using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common
{
    public abstract class UseCase<TInput, TOutput>
    {
        private readonly AbstractValidator<TInput> _validator;
        private bool _isValid = true;
        private bool _complete;
        private ValidationResult _validationResult;
        public UseCase(AbstractValidator<TInput> validator = default)
        {
            _validator = validator;
        }

        public void Execute(TInput input)
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
                    OnExecute(input, _validationResult);
                    _complete = true;
                }
                else
                {
                    OnNotIsValid(input, _validationResult);
                }

            }
            catch (Exception ex)
            {

                OnException(ex);
            }
            finally
            {
                OnComplete(_complete);
            }
        }
        public abstract void OnExecute(TInput input, ValidationResult validationResult);
        public virtual void OnNotIsValid(TInput input, ValidationResult validationResult)
        {


        }

        public virtual void OnException(Exception exception)
        {
            throw exception;
        }

        public virtual void OnComplete(bool completed)
        {
            return;
        }
    }
}
