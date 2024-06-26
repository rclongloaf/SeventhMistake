﻿using Core.Mvi.Api.Action;
using Core.Mvi.Api.Patch;
using Core.Mvi.Api.State;

namespace Core.Mvi.Core
{
public abstract class MviFeature<IA, EA, IS, ES, P>
    where IA : InternalMviAction
    where EA : ExternalMviAction
    where IS : InternalMviState
    where ES : ExternalMviState
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
            reducer.MviExecutor.DispatchInternal(() => { ApplyAction(initialAction); });
        }
    }

    protected abstract MviActionMapper<IA, EA> ProvideActionMapper();


    public void SendAction(EA action)
    {
        reducer.MviExecutor.DispatchInternal(() =>
        {
            var internalAction = actionMapper.MapAction(action);
            ApplyAction(internalAction);
        });
    }

    protected abstract void ApplyAction(IA action);
}
}