using System;
using System.Collections.Generic;
using Core.Mvi;
using Core.Mvi.State;
using Features.GuessWord.Mappers;
using Features.GuessWord.State;

namespace Features.GuessWord
{
internal class GuessWordReducer : MviReducer<InternalState, GuessWordState, Patch>
{
    public GuessWordReducer(
        MviStateConsumer<GuessWordState> stateConsumer
    ) : base(stateConsumer, new InternalState.WaitInit()) { }

    protected override MviStateMapper<InternalState, GuessWordState> ProvideStateMapper()
    {
        return new GuessWordStateMapper();
    }

    protected override InternalState ApplyPatch(Patch patch)
    {
        return patch switch
        {
            Patch.CharacterGuessed characterGuessed => ApplyCharacterGuessedPatch(characterGuessed),
            Patch.CharacterMistake characterMistake => ApplyCharacterMistakePatch(characterMistake),
            Patch.Init => new InternalState.WaitingForStart(),
            Patch.Lose lose => ApplyLosePatch(lose),
            Patch.Start start => ApplyStartPatch(start),
            Patch.Win win => ApplyWinPatch(win),
            _ => throw new ArgumentOutOfRangeException(nameof(patch))
        };
    }

    private InternalState ApplyCharacterGuessedPatch(Patch.CharacterGuessed patch)
    {
        if (State is not InternalState.Active activeState) return State;

        var checkedCharacters = new HashSet<char>(activeState.checkedCharacters);
        var guessedCharacters = new HashSet<char>(activeState.guessedCharacters);

        checkedCharacters.Add(patch.character);
        guessedCharacters.Add(patch.character);

        return activeState with
        {
            guessedCharacters = guessedCharacters,
            checkedCharacters = checkedCharacters
        };
    }

    private InternalState ApplyCharacterMistakePatch(Patch.CharacterMistake patch)
    {
        if (State is not InternalState.Active activeState) return State;

        var checkedCharacters = new HashSet<char>(activeState.checkedCharacters);

        checkedCharacters.Add(patch.character);

        return activeState with
        {
            checkedCharacters = checkedCharacters,
            mistakesCount = activeState.mistakesCount + 1
        };
    }

    private InternalState ApplyLosePatch(Patch.Lose patch)
    {
        if (State is not InternalState.Active activeState) return State;

        return new InternalState.Lose(activeState.word);
    }

    private InternalState ApplyWinPatch(Patch.Win patch)
    {
        if (State is not InternalState.Active activeState) return State;

        return new InternalState.Win(activeState.word);
    }

    private InternalState ApplyStartPatch(Patch.Start patch)
    {
        return new InternalState.Active(
            word: patch.word,
            guessedCharacters: new HashSet<char>(),
            checkedCharacters: new HashSet<char>(),
            mistakesCount: 0
        );
    }
}
}