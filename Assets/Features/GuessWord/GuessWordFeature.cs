using System;
using Core.Mvi;
using Core.Mvi.Action;
using Features.GuessWord.Action;
using Features.GuessWord.Config;
using Features.GuessWord.Mappers;
using Features.GuessWord.State;

namespace Features.GuessWord
{
public class GuessWordFeature : MviFeature<
    InternalAction,
    GuessWordAction,
    InternalState,
    GuessWordState,
    Patch
>
{
    private readonly GuessWordConfig config;
    
    public GuessWordFeature(
        MviReducer<InternalState, GuessWordState, Patch> reducer,
        GuessWordConfig config
    ) : base(reducer, new InternalAction.Init())
    {
        this.config = config;
    }
    
    protected override MviActionMapper<InternalAction, GuessWordAction> ProvideActionMapper()
    {
        return new GuessWordActionMapper();
    }

    protected override void ApplyAction(InternalAction action)
    {
        switch (action)
        {
            case InternalAction.GuessCharacter guessCharacterAction:
                ApplyGuessCharacterAction(guessCharacterAction);
                break;
            case InternalAction.Init:
                reducer.SendPatch(new Patch.Init());
                break;
            case InternalAction.Start startAction:
                reducer.SendPatch(new Patch.Start(startAction.word));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action));
        }
    }

    private void ApplyGuessCharacterAction(InternalAction.GuessCharacter action)
    {
        if (State is not InternalState.Active activeState) return;

        var character = action.character;

        if (activeState.checkedCharacters.Contains(character)) return;

        if (activeState.word.Contains(character))
        {
            var isGuessedWord = true;

            foreach (var ch in activeState.word)
            {
                isGuessedWord |= ch == character || activeState.guessedCharacters.Contains(ch);
                if (!isGuessedWord) break;
            }

            if (isGuessedWord)
            {
                reducer.SendPatch(new Patch.Win());
            }
            else
            {
                reducer.SendPatch(new Patch.CharacterGuessed(character));
            }
        }
        else if (activeState.mistakesCount + 1 < config.mistakesCountForLose)
        {
            reducer.SendPatch(new Patch.CharacterMistake(character));
        }
        else
        {
            reducer.SendPatch(new Patch.Lose());
        }
    }
}
}