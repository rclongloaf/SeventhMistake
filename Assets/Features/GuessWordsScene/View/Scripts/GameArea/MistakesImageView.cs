using System.Collections.Generic;
using UnityEngine;

namespace Features.GuessWordsScene.View.Scripts.GameArea
{
public class MistakesImageView : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> mistakeImages = null!;

    public void SetMistakesCount(int mistakesCount)
    {
        for (var i = 0; i < mistakeImages.Count; i++)
        {
            mistakeImages[i].SetActive(i < mistakesCount);
        }
    }
}
}