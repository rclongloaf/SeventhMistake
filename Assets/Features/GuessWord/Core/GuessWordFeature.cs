using System;
using Core.Mvi.Api.Action;
using Core.Mvi.Core;
using Features.GuessWord.Api;
using Features.GuessWord.Api.Config;
using Features.GuessWord.Core.Mappers;
using Features.GuessWord.Core.State;

namespace Features.GuessWord.Core
{
public class GuessWordFeature : MviFeature<
    GuessWordAction,
    GuessWordAction,
    InternalState,
    GuessWordState,
    Patch.Patch
>
{
    private readonly GuessWordConfig config;
    
    public GuessWordFeature(
        MviReducer<InternalState, GuessWordState, Patch.Patch> reducer,
        GuessWordConfig config
    ) : base(reducer, null)
    {
        this.config = config;
    }
    
    protected override MviActionMapper<GuessWordAction, GuessWordAction> ProvideActionMapper()
    {
        return new GuessWordActionMapper();
    }

    protected override void ApplyAction(GuessWordAction action)
    {
        switch (action)
        {
            case GuessWordAction.GuessLetter guessLetterAction:
                ApplyGuessLetterAction(guessLetterAction);
                break;
            case GuessWordAction.Start startAction:
                reducer.SendPatch(new Patch.Patch.Start(startAction.word.ToUpper()));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action));
        }
    }

    private void ApplyGuessLetterAction(GuessWordAction.GuessLetter action)
    {
        if (State is not InternalState.Active { status: GuessWordActiveStatus.InProgress } activeState) return;

        var letter = action.letter.ToString().ToUpper()[0];

        if (activeState.checkedLetters.Contains(letter)) return;

        if (activeState.word.Contains(letter))
        {
            var isGuessedWord = true;

            foreach (var ch in activeState.word)
            {
                isGuessedWord &= ch == letter || activeState.guessedLetters.Contains(ch);
                if (!isGuessedWord) break;
            }

            if (isGuessedWord)
            {
                reducer.SendPatch(new Patch.Patch.Win());
            }
            else
            {
                reducer.SendPatch(new Patch.Patch.LetterGuessed(letter));
            }
        }
        else if (activeState.mistakesCount + 1 < config.mistakesCountForLose)
        {
            reducer.SendPatch(new Patch.Patch.LetterMistake(letter));
        }
        else
        {
            reducer.SendPatch(new Patch.Patch.Lose());
        }
    }
}
}