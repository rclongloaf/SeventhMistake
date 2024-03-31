using System;
using Core.Mvi.Api.Action;
using Core.Mvi.Api.State;
using Core.Mvi.Core;
using Features.GuessWord.Api;
using Features.GuessWord.Api.Config;
using Features.GuessWord.Core;
using Features.GuessWordsScene.Core.Mappers;
using Features.WordsProvider.Api;

namespace Features.GuessWordsScene.Core
{
public class GuessWordsSceneFeature : MviFeature<
    InternalAction,
    GuessWordsSceneAction,
    InternalState,
    GuessWordsSceneState,
    Patch
>, MviStateConsumer<GuessWordState>
{
    private IWordsProvider wordsProvider;
    private GuessWordFeature guessWordFeature;

    public GuessWordsSceneFeature(
        MviReducer<InternalState, GuessWordsSceneState, Patch> reducer,
        IWordsProvider wordsProvider,
        GuessWordConfig guessWordConfig
    ) : base(reducer, null)
    {
        this.wordsProvider = wordsProvider;

        var guessWordReducer = new GuessWordReducer(
            stateConsumer: this
        );
        guessWordFeature = new GuessWordFeature(
            reducer: guessWordReducer,
            config: guessWordConfig
        );
    }

    protected override MviActionMapper<InternalAction, GuessWordsSceneAction> ProvideActionMapper()
    {
        return new ActionMapper();
    }

    protected override void ApplyAction(InternalAction action)
    {
        switch (action)
        {
            case InternalAction.GuessLetter guessLetter:
                ApplyGuessLetterAction(guessLetter);
                break;
            case InternalAction.StartNewGame startNewGame:
                ApplyStartNewGameAction(startNewGame);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action));
        }
    }

    private void ApplyStartNewGameAction(InternalAction.StartNewGame action)
    {
        var word = wordsProvider.GetNextWord();

        guessWordFeature.SendAction(new GuessWordAction.Start(word));
    }

    private void ApplyGuessLetterAction(InternalAction.GuessLetter action)
    {
        if (State is not InternalState.Active activeState) return;

        guessWordFeature.SendAction(new GuessWordAction.GuessLetter(action.character));
    }

    public void ConsumeState(GuessWordState state)
    {
        switch (state)
        {
            case GuessWordState.Active activeState:
                reducer.SendPatch(new Patch.UpdateActiveGuessWordState(activeState));
                break;
            case GuessWordState.NotStarted:
                //Nothing
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state));
        }
    }
}
}