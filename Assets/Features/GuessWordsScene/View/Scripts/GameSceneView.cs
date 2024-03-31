using System;
using Core.Translations;
using Core.View;
using Features.GuessWord.Api;
using Features.GuessWordsScene.Core;
using Features.GuessWordsScene.View.Scripts.GameArea;
using Features.GuessWordsScene.View.Scripts.Keyboard;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Features.GuessWordsScene.View.Scripts
{
public class GameSceneView : MonoBehaviour
{
    [SerializeField]
    private WordView wordView = null!;
    [SerializeField]
    private MistakesImageView mistakesImageView = null!;
    [SerializeField]
    private KeyboardView keyboardView = null!;
    [SerializeField]
    private GameObject guessResultLayout = null!;
    [SerializeField]
    private Button restartButton = null!;
    [SerializeField]
    private Text finishText = null!;
    [SerializeField]
    private Text resultsText = null!;

    [SerializeField]
    private TranslationString lossTitle = null!;
    [SerializeField]
    private TranslationString winTitle = null!;

    [SerializeField]
    private TranslationString wonCountStr = null!;
    [SerializeField]
    private TranslationString lostCountStr = null!;

    private Context context = null!;

    public event Action? OnRestartClicked;
    public event Action<char>? OnLetterClicked;

    [Inject]
    public void Init(Context context)
    {
        this.context = context;
    }

    private void Awake()
    {
        restartButton.onClick.AddListener(() => { OnRestartClicked?.Invoke(); });
        keyboardView.OnLetterClicked += (letter) => { OnLetterClicked?.Invoke(letter); };
    }

    public void Bind(GuessWordsSceneState.GameScene state)
    {
        switch (state.guessWordState.status)
        {
            case GuessWordActiveStatus.InProgress:
                guessResultLayout.gameObject.SetActive(false);
                keyboardView.gameObject.SetActive(true);
                break;
            case GuessWordActiveStatus.Lose:
                guessResultLayout.gameObject.SetActive(true);
                keyboardView.gameObject.SetActive(false);

                finishText.text = lossTitle.Resolve(context);
                break;
            case GuessWordActiveStatus.Win:
                guessResultLayout.gameObject.SetActive(true);
                keyboardView.gameObject.SetActive(false);

                finishText.text = winTitle.Resolve(context);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        wordView.SetLettersState(state.guessWordState.lettersStateList);
        mistakesImageView.SetMistakesCount(state.guessWordState.mistakesCount);
        resultsText.text = $"{wonCountStr.Resolve(context)}: {state.winsCount}. {lostCountStr.Resolve(context)}: {state.losesCount}";
    }
}
}