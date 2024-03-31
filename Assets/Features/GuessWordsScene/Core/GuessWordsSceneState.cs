using Core.Mvi.Api.State;
using Features.GuessWord.Api;

namespace Features.GuessWordsScene.Core
{
public interface GuessWordsSceneState : MviState
{
    public record StartScene : GuessWordsSceneState;

    public record GameScene(
        GuessWordState.Active guessWordState,
        int winsCount,
        int losesCount
    ) : GuessWordsSceneState;
}
}