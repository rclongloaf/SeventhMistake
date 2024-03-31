using Core.Mvi.Api.Action;
using Core.Mvi.Api.Patch;
using Core.Mvi.Api.State;

namespace Core.Mvi.Core
{
public abstract class MviFeature<IA, EA, IS, ES, P>
    where IA : InternalMviAction
    where EA : MviAction
    where IS : InternalMviState
    where ES : MviState
    where P : MviPatch
{
    private readonly MviActionMapper<IA, EA> actionMapper;
    protected readonly MviReducer<IS, ES, P> reducer;

    protected IS State => reducer.State;

    protected MviFeature(
        MviReducer<IS, ES, P> reducer,
        IA? initialAction
    )
    {
        this.reducer = reducer;
        actionMapper = ProvideActionMapper();

        if (initialAction != null)
        {
            ApplyAction(initialAction);
        }
    }

    protected abstract MviActionMapper<IA, EA> ProvideActionMapper();


    public void SendAction(EA action)
    {
        var internalAction = actionMapper.MapAction(action);
        ApplyAction(internalAction);
    }

    protected abstract void ApplyAction(IA action);
}
}