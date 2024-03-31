using System;
using Core.Mvi.Api.Action;

namespace Features.GuessWordsScene.Core.Mappers
{
internal class ActionMapper : MviActionMapper<InternalAction, GuessWordsSceneAction>
{
    public InternalAction MapAction(GuessWordsSceneAction externalAction)
    {
        switch (externalAction)
        {
            case GuessWordsSceneAction.OnLetterClicked onLetterClicked:
                return new InternalAction.GuessLetter(onLetterClicked.letter);

            case GuessWordsSceneAction.OnRestartClicked:
            case GuessWordsSceneAction.OnStartClicked:
                return new InternalAction.StartNewGame();

            default:
                throw new ArgumentOutOfRangeException(nameof(externalAction));
        }
    }
}
}