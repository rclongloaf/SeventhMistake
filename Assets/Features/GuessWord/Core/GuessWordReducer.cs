using System;
using System.Collections.Generic;
using Core.Mvi.Api.State;
using Core.Mvi.Core;
using Features.GuessWord.Api;
using Features.GuessWord.Core.Mappers;
using Features.GuessWord.Core.State;

namespace Features.GuessWord.Core
{
public class GuessWordReducer : MviReducer<InternalState, GuessWordState, Patch.Patch>
{
    public GuessWordReducer(
        MviStateConsumer<GuessWordState> stateConsumer
    ) : base(stateConsumer, new InternalState.WaitingForStart()) { }

    protected override MviStateMapper<InternalState, GuessWordState> ProvideStateMapper()
    {
        return new GuessWordStateMapper();
    }

    protected override InternalState ApplyPatch(Patch.Patch patch)
    {
        return patch switch
        {
            Patch.Patch.LetterGuessed letterGuessed => ApplyLetterGuessedPatch(letterGuessed),
            Patch.Patch.LetterMistake letterMistake => ApplyLetterMistakePatch(letterMistake),
            Patch.Patch.Lose lose => ApplyLosePatch(lose),
            Patch.Patch.Start start => ApplyStartPatch(start),
            Patch.Patch.Win win => ApplyWinPatch(win),
            _ => throw new ArgumentOutOfRangeException(nameof(patch))
        };
    }

    private InternalState ApplyLetterGuessedPatch(Patch.Patch.LetterGuessed patch)
    {
        if (State is not InternalState.Active { status: GuessWordActiveStatus.InProgress} activeState) return State;

        var checkedLetters = new HashSet<char>(activeState.checkedLetters);
        var guessedLetters = new HashSet<char>(activeState.guessedLetters);

        checkedLetters.Add(patch.letter);
        guessedLetters.Add(patch.letter);

        return activeState with
        {
            guessedLetters = guessedLetters,
            checkedLetters = checkedLetters
        };
    }

    private InternalState ApplyLetterMistakePatch(Patch.Patch.LetterMistake patch)
    {
        if (State is not InternalState.Active { status: GuessWordActiveStatus.InProgress} activeState) return State;

        var checkedLetters = new HashSet<char>(activeState.checkedLetters);

        checkedLetters.Add(patch.letter);

        return activeState with
        {
            checkedLetters = checkedLetters,
            mistakesCount = activeState.mistakesCount + 1
        };
    }

    private InternalState ApplyLosePatch(Patch.Patch.Lose patch)
    {
        if (State is not InternalState.Active { status: GuessWordActiveStatus.InProgress} activeState) return State;

        return activeState with
        {
            mistakesCount = activeState.mistakesCount + 1,
            status = GuessWordActiveStatus.Lose
        };
    }

    private InternalState ApplyWinPatch(Patch.Patch.Win patch)
    {
        if (State is not InternalState.Active { status: GuessWordActiveStatus.InProgress} activeState) return State;

        return activeState with
        {
            mistakesCount = activeState.mistakesCount,
            status = GuessWordActiveStatus.Win
        };
    }

    private InternalState ApplyStartPatch(Patch.Patch.Start patch)
    {
        return new InternalState.Active(
            word: patch.word,
            guessedLetters: new HashSet<char>(),
            checkedLetters: new HashSet<char>(),
            mistakesCount: 0,
            status: GuessWordActiveStatus.InProgress
        );
    }
}
}