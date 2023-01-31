using System;

namespace edk.Kchef.Domain.Common.Base;

public interface IEntity
{
    Guid Id { get; }
    bool Deleted { get; }
    Guid? CompanyId { get; }
    Guid? AuditUserId { get; }
    DateTime? AuditDate { get; }
}
