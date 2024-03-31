using Core.Mvi.Api.State;
using Features.GuessWord.Api;

namespace Features.GuessWordsScene.Core
{
public interface InternalState : InternalMviState
{
    public record WaitForStart : InternalState;

    public record Active(
        GuessWordState.Active guessWordState,
        int winsCount,
        int losesCount
    ) : InternalState;
}
}