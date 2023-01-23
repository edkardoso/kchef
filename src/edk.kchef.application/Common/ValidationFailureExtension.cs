using System.Collections.Generic;
using System.Linq;
using edk.Fusc.Contracts.Common;
using edk.Fusc.Core.Validators;
using FluentValidation.Results;

namespace edk.Kchef.Application.Common;

public static class ValidationFailureExtension
{
    public static IReadOnlyCollection<Notification> ToNotifications(this List<ValidationFailure> failures)
    {
        if (failures == null)
        {
            return new List<Notification>();
        }

        return failures.Select(f => new Notification()
        {
            Message = f.ErrorMessage,
            Severity = (SeverityType)f.Severity
        }
        ).ToList()
        .AsReadOnly();
    }
}