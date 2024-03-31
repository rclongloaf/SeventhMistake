using System;
using Core.Mvi.Api.State;
using Core.Mvi.Core;
using Features.GuessWord.Api.Config;
using Features.GuessWordsScene.Core;
using Features.WordsProvider.Api;
using UnityEngine;
using VContainer;

namespace Features.GuessWordsScene.View.Scripts
{
public class GuessWordsSceneView : MonoBehaviour, MviStateConsumer<GuessWordsSceneState>
{
    [SerializeField]
    private StartSceneView startSceneView = null!;
    [SerializeField]
    private GameSceneView gameSceneView = null!;


    private GuessWordsSceneFeature feature = null!;

    [Inject]
    public void Init(
        IWordsProvider wordsProvider,
        MviExecutor mviExecutor,
        GuessWordConfig guessWordConfig
    )
    {
        var reducer = new GuessWordsSceneReducer(
            stateConsumer: this,
            mviExecutor: mviExecutor
        );

        feature = new GuessWordsSceneFeature(
            reducer: reducer,
            wordsProvider: wordsProvider,
            guessWordConfig: guessWordConfig
        );

        startSceneView.OnStartClicked += () => { feature.SendAction(new GuessWordsSceneAction.OnStartClicked()); };
        gameSceneView.OnRestartClicked += () => { feature.SendAction(new GuessWordsSceneAction.OnRestartClicked()); };
        gameSceneView.OnLetterClicked += letter => { feature.SendAction(new GuessWordsSceneAction.OnLetterClicked(letter)); };
    }

    public void ConsumeState(GuessWordsSceneState state)
    {
        switch (state)
        {
            case GuessWordsSceneState.GameScene gameScene:
                ApplyGameSceneState(gameScene);
                break;
            case GuessWordsSceneState.StartScene mainScene:
                ApplyMainSceneState(mainScene);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state));
        }
    }

    private void ApplyMainSceneState(GuessWordsSceneState.StartScene state)
    {
        startSceneView.gameObject.SetActive(true);
        gameSceneView.gameObject.SetActive(false);
    }

    private void ApplyGameSceneState(GuessWordsSceneState.GameScene state)
    {
        startSceneView.gameObject.SetActive(false);
        gameSceneView.gameObject.SetActive(true);

        gameSceneView.Bind(state);
    }
}
}