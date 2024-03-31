using System;
using System.Collections.Generic;
using Core.View;
using UnityEngine;

namespace App.Scripts.Words
{
[CreateAssetMenu(menuName = "Translations/TranslationWords")]
public class TranslationWords : ScriptableObject
{
    [SerializeField]
    private List<string> ru = null!;
    [SerializeField]
    private List<string> en = null!;

    public IEnumerable<string> Resolve(Context context)
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