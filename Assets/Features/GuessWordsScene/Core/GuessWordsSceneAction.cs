﻿using Core.Mvi.Api.Action;

namespace Features.GuessWordsScene.Core
{
public interface GuessWordsSceneAction : ExternalMviAction
{
    public record OnStartClicked : GuessWordsSceneAction;

    public record OnRestartClicked : GuessWordsSceneAction;

    public record OnLetterClicked(char letter) : GuessWordsSceneAction;
}
}