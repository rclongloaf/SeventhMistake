using Core.Mvi.Api.Patch;
using Core.Mvi.Api.State;

namespace Core.Mvi.Core
{
public abstract class MviReducer<IS, ES, P>
    where IS : InternalMviState
    where ES : MviState
    where P : MviPatch
{
    private readonly MviStateMapper<IS, ES> stateMapper;
    private readonly MviStateConsumer<ES> stateConsumer;
    
    public IS State { get; private set; }
    public MviExecutor MviExecutor { get; private set; }

    protected MviReducer(
        MviStateConsumer<ES> stateConsumer,
        MviExecutor mviExecutor,
        IS initialState
    )
    {
        this.stateConsumer = stateConsumer;
        MviExecutor = mviExecutor;
        State = initialState;
        stateMapper = ProvideStateMapper();
    }
    
    protected abstract MviStateMapper<IS, ES> ProvideStateMapper();
    
    public void SendPatch(P patch)
    {
        var newState = ApplyPatch(patch);
        if (!State.Equals(newState))
        {
            UpdateState(newState);
        }
    }

    protected abstract IS ApplyPatch(P patch);
    
    private void UpdateState(IS newState)
    {
        State = newState;

        var externalState = stateMapper.MapState(newState);
        MviExecutor.DispatchExternal(() =>
        {
            stateConsumer.ConsumeState(externalState);
        });
    }
}
}