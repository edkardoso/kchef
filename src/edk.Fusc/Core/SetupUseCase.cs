namespace edk.Fusc.Core;

public record SetupUseCase(bool PublishStartEvent = true
    , bool PublishSuccessEvent = true
    , bool PublishFailureEvent = true
    , bool WaitingCompleteStartEvent = false
    , bool WaitingCompleteSuccessEvent = false
    , bool WaitingCompleteFailureEvent = false)
{

}
