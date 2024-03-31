using System;
using Core.View;
using UnityEngine;

namespace Core.Translations
{
[CreateAssetMenu(menuName = "Translations/TranslationString")]
public class TranslationString : ScriptableObject
{
    [field: SerializeField]
    public string Id { get; private set; }
    [field: SerializeField]
    public string Ru { get; private set; }
    [field: SerializeField]
    public string En { get; private set; }

    public string Resolve(Context context)
    {
        var language = context.Language;
        return language switch
        {
            Language.RU => Ru,
            Language.EN => En,
            _ => throw new ArgumentOutOfRangeException(nameof(language), language, null)
        };
    }
}
}