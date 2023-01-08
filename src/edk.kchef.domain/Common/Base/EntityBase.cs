using System;
using System.Collections.Generic;
using System.Linq;
using edk.Kchef.Domain.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common.Base
{
    public abstract class EntityBase<TEntity> where TEntity : class
    {
        private readonly AbstractValidator<TEntity> _validator;
        private readonly TEntity _entity;
        private ValidationResult _validationResult;
        private List<Notification> _notifications = new List<Notification>();

        public Guid Id { get; private set; }
        public virtual bool Deleted { get; private set; }
        public virtual Guid? CompanyId { get; private set; }
        public virtual Guid? AuditUserId { get; private set; }
        public virtual DateTime? AuditDate { get; private set; }
        private bool HasErrors => _notifications.Any(n => n.Severity == SeverityType.Error);

        protected EntityBase(AbstractValidator<TEntity> validator, TEntity entity)
            : this()
        {
            _validator = validator;
            _entity = entity;
        }

        protected EntityBase()
        {
            if (Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }

        public virtual void Validate() => Validate();

        public virtual bool IsValid()
        {
            Validate(false);

            return HasErrors;
        }

        private void Validate(bool throwException = true)
        {
            if (_validator == null)
            {
                _validationResult = null;
                return;
            }

            _validationResult = _validator.Validate(_entity);
            _notifications = Notification.ConvertFrom(_validationResult.Errors);

            if (throwException)
            {
                throw new InvalidEntityException(Notification.ConvertFrom(_validationResult.Errors));
            }

        }
    }
}
