using System;
using Core.View;
using UnityEngine;
using VContainer;

namespace Features.GuessWordsScene.View.Scripts.Keyboard
{
public class KeyboardView : MonoBehaviour
{
    [SerializeField]
    private KeyboardItem letterItemPrefab = null!;
    [SerializeField]
    private TranslationLetters lettersRes = null!;

    public event Action<char>? OnLetterClicked;

    [Inject]
    private void Init(Context context)
    {
        var letters = lettersRes.Resolve(context);
        foreach (var letter in letters)
        {
            var item = Instantiate(letterItemPrefab, transform);
            item.SetLetter(letter);
            item.OnClicked += OnItemClicked;
        }
    }

    private void OnItemClicked(char letter)
    {
        OnLetterClicked?.Invoke(letter);
    }
}
}