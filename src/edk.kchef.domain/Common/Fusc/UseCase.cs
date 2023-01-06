using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common.Fusc
{
    public abstract class UseCase<TInput, TOutput> : IUseCase<TInput, TOutput>
    {
        private readonly AbstractValidator<TInput> _validator;
        private readonly IPresenter<TInput, TOutput> _presenter;
        private bool _isValid = true;
        private bool _complete;
        private ValidationResult _validationResult;
        private readonly List<IUseCase> _observers;

        public TOutput Result { get; private set; }

        protected UseCase(IPresenter<TInput, TOutput> presenter = default, AbstractValidator<TInput> validator = default)
        {
            _presenter = presenter ?? new PresenterNull<TInput, TOutput>();
            _validator = validator ?? new ValidadorNull<TInput>();
            _observers = new List<IUseCase>();
        }

        public IPresenter<TInput, TOutput> Execute(TInput input)
        {
            try
            {
                _validationResult = _validator.Validate(input);
                _isValid = _validationResult.IsValid;

                if (_isValid)
                {
                    Result = OnExecute(input);
                    _complete = true;
                    _presenter.OnSuccess(Result);

                    if (!_presenter.Success)
                        throw new InvalidOperationException("The success property must equal TRUE.");

                    Notify();
                }
                else
                {
                    _presenter.OnError(input, _validationResult);

                    if (_presenter.Success)
                        throw new InvalidOperationException("The success property must equal FALSE.");

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

        public abstract TOutput OnExecute(TInput input);

        protected virtual void OnComplete(bool completed)
        {
            return;
        }

        protected void Notify()
        {
            _observers.ForEach(o => ((IUseCase<TInput, TOutput>)o).Handler(this));
        }

        public void Subscribe(IUseCase observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public virtual void Handler(IUseCase<TInput, TOutput> other)
        {

        }
    }
}
