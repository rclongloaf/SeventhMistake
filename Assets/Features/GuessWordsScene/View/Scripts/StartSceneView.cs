using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.GuessWordsScene.View.Scripts
{
public class StartSceneView : MonoBehaviour
{
    [SerializeField]
    private Button startButton = null!;

    public event Action? OnStartClicked;

    private void Awake()
    {
        startButton.onClick.AddListener(() =>
        {
            OnStartClicked?.Invoke();
        });
    }
}
}