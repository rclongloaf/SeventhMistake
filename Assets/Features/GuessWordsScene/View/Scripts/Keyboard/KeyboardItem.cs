using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.GuessWordsScene.View.Scripts.Keyboard
{
public class KeyboardItem : MonoBehaviour
{
    [SerializeField]
    private Text letterText = null!;
    [SerializeField]
    private Button button = null!;

    private char letter;
    
    public event Action<char>? OnClicked;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            OnClicked?.Invoke(letter);
        });
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        letterText.text = letter.ToString().ToUpper();
    }
}
}