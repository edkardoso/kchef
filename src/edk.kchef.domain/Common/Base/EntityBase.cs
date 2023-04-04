using System;
using System.Collections.Generic;
using System.Linq;
using edk.Kchef.Domain.Common.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace edk.Kchef.Domain.Common.Base;

public abstract class EntityBase : IEntity
{
    private readonly List<Notification> _notifications = new();

    public Guid Id { get; private set; }
    public virtual bool Deleted { get; private set; }
    public virtual Guid? CompanyId { get; private set; }
    public virtual Guid? AuditUserId { get; private set; }
    public virtual DateTime? AuditDate { get; private set; }
    public List<Notification> Notifications => _notifications;

     protected EntityBase()
    {
        if (Id == Guid.Empty)
        {
            Id = Guid.NewGuid();
        }

    }
    /// <summary>
    /// Valida a entidade e lança uma exceção caso a mesma esteja inválida
    /// </summary>
    /// <remarks>Todas as falhas identificadas serão enviadas juntos na exceção</remarks>
    /// <exception cref="InvalidEntityException"></exception>
    protected void Validate()
    {
        CheckBasicData();

        RunValidatorCustom();

        if (Notifications.HasError())
        {
            throw new InvalidEntityException(Notifications);
        }

    }

    /// <summary>
    /// Permite adicionar outras validações 
    /// </summary>
    public virtual List<Notification> ValidateCustom() => null;

    private void RunValidatorCustom()
    {
        var _notificationCustom = ValidateCustom();
        if (_notificationCustom != null && _notifications.Any())
        {
            _notifications.AddRange(_notificationCustom);
        }
    }

    private void CheckBasicData()
    {
        if (Id == Guid.Empty)
            _notifications.Add(Notification.Error($"A entidade não possui um Id."));

        if (CompanyId == null)
            Warning($"A entidade não possui a identificação da Empresa.");

        if (AuditUserId == null)
            Warning($"A entidade não possui a identificação do Usuário que realizou a ação.");

        if (AuditDate == DateTime.MinValue)
            Warning($"A entidade não possui o momento que o Usuário realizou a ação.");

        void Warning(string message)
            => _notifications.Add(Notification.Warning(message));
    }
}
