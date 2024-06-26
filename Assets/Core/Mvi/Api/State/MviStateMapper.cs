﻿namespace Core.Mvi.Api.State
{
public interface MviStateMapper<in IS, out ES>
    where IS : InternalMviState
    where ES : ExternalMviState
{
    public ES MapState(IS internalState);
}
}