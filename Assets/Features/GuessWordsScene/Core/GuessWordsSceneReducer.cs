using System;
using Core.Mvi.Api.State;
using Core.Mvi.Core;
using Features.GuessWord.Api;
using Features.GuessWordsScene.Core.Mappers;

namespace Features.GuessWordsScene.Core
{
public class GuessWordsSceneReducer : MviReducer<InternalState, GuessWordsSceneState, Patch>
{
    public GuessWordsSceneReducer(
        MviStateConsumer<GuessWordsSceneState> stateConsumer,
        MviExecutor mviExecutor
    ) : base(stateConsumer, mviExecutor, new InternalState.WaitForStart()) { }

    protected override MviStateMapper<InternalState, GuessWordsSceneState> ProvideStateMapper()
    {
        return new StateMapper();
    }

    protected override InternalState ApplyPatch(Patch patch)
    {
        return patch switch
        {
            Patch.UpdateActiveGuessWordState updatePatch => ApplyUpdateActiveGuessWordStatePatch(updatePatch),
            _ => throw new ArgumentOutOfRangeException(nameof(patch))
        };
    }

    private InternalState ApplyUpdateActiveGuessWordStatePatch(Patch.UpdateActiveGuessWordState patch)
    {
        switch (State)
        {
            case InternalState.Active active:
                var winsCount = active.winsCount;
                if (patch.guessWordState.status is GuessWordActiveStatus.Win)
                {
                    winsCount++;
                }

                var losesCount = active.losesCount;
                if (patch.guessWordState.status is GuessWordActiveStatus.Lose)
                {
                    losesCount++;
                }

                return new InternalState.Active(
                    guessWordState: patch.guessWordState,
                    winsCount: winsCount,
                    losesCount: losesCount
                );
            case InternalState.WaitForStart:
                return new InternalState.Active(
                    guessWordState: patch.guessWordState,
                    winsCount: 0,
                    losesCount: 0
                );
            default:
                throw new ArgumentOutOfRangeException(nameof(State));
        }
    }
}
}