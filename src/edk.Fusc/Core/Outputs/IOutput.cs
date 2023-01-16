using edk.Fusc.Core.Validators;

namespace edk.Fusc.Core.Outputs;

public interface IOutput
{
    List<Notification> Messages { get; set; }
}
