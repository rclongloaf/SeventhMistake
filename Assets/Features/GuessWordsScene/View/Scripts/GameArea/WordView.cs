using System.Collections.Generic;
using Features.GuessWord.Api;
using UnityEngine;

namespace Features.GuessWordsScene.View.Scripts.GameArea
{
public class WordView : MonoBehaviour
{
    [SerializeField]
    private WordLetterView letterPrefab = null!;

    private List<WordLetterView> visibleItems = new();
    private Stack<WordLetterView> itemsPool = new();

    public void SetLettersState(IReadOnlyList<LetterState> lettersState)
    {
        while (visibleItems.Count > lettersState.Count)
        {
            var lastIndex = visibleItems.Count - 1;
            var item = visibleItems[lastIndex];
            visibleItems.RemoveAt(visibleItems.Count - 1);
            ReleaseItem(item);
        }

        while (visibleItems.Count < lettersState.Count)
        {
            var item = AddItem();
            visibleItems.Add(item);
        }

        for (var i = 0; i < lettersState.Count; i++)
        {
            visibleItems[i].SeLetterState(lettersState[i]);
        }
    }

    private void ReleaseItem(WordLetterView item)
    {
        item.gameObject.SetActive(false);
        itemsPool.Push(item);
    }

    private WordLetterView AddItem()
    {
        if (itemsPool.Count > 0)
        {
            var item = itemsPool.Pop();
            item.gameObject.SetActive(true);
            return item;
        }

        return Instantiate(letterPrefab, transform);
    }
}
}