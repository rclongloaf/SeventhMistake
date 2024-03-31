using System;
using Core.Mvi.Api.State;

namespace Features.GuessWordsScene.Core.Mappers
{
internal class StateMapper : MviStateMapper<InternalState, GuessWordsSceneState>
{
    public GuessWordsSceneState MapState(InternalState internalState)
    {
        return internalState switch
        {
            InternalState.Active active => new GuessWordsSceneState.GameScene(
                active.guessWordState,
                active.winsCount,
                active.losesCount
            ),
            InternalState.WaitForStart => new GuessWordsSceneState.StartScene(),
            _ => throw new ArgumentOutOfRangeException(nameof(internalState))
        };
    }
}
}