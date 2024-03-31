using System;
using Features.GuessWord.Api;
using UnityEngine;
using UnityEngine.UI;

namespace Features.GuessWordsScene.View.Scripts.GameArea
{
public class WordLetterView : MonoBehaviour
{
    [SerializeField]
    private Text letterText = null!;

    public void SeLetterState(LetterState letterState)
    {
        switch (letterState)
        {
            case LetterState.Guessed guessed:
                letterText.gameObject.SetActive(true);
                letterText.text = guessed.letter.ToString().ToUpper();
                break;
            case LetterState.NotGuessed:
                letterText.gameObject.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(letterState));
        }
    }
}
}