using System;
using System.Collections.Generic;
using Core.View;
using UnityEngine;

namespace Features.GuessWordsScene.View.Scripts.Keyboard
{
[CreateAssetMenu(menuName = "Translations/TranslationLetters")]
public class TranslationLetters : ScriptableObject
{
    [SerializeField]
    private List<char> ru = null!;
    [SerializeField]
    private List<char> en = null!;

    public IEnumerable<char> Resolve(Context context)
    {
        var language = context.Language;
        return language switch
        {
            Language.RU => ru,
            Language.EN => en,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
}