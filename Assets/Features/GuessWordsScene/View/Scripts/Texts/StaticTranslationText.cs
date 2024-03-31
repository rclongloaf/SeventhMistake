using Core.Translations;
using Core.View;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Features.GuessWordsScene.View.Scripts.Texts
{
[RequireComponent(typeof(Text))]
public class StaticTranslationText : MonoBehaviour
{
    [SerializeField]
    private TranslationString translationString = null!;

    [Inject]
    public void Init(Context context)
    {
        GetComponent<Text>().text = translationString.Resolve(context);
    }
}
}