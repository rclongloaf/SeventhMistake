using App.Scripts.Words;
using Core.Mvi.Core;
using Core.Threads;
using Core.View;
using Features.GuessWord.Api.Config;
using Features.WordsProvider.Api;
using Features.WordsProvider.Impl;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.Scripts
{
public class AppLifetimeScope : LifetimeScope
{
    [SerializeField]
    private Language defaultLanguage;
    [SerializeField]
    private MainThreadDispatcher mainThreadDispatcher = null!;
    [SerializeField]
    private TranslationWords wordsPack = null!;
    [SerializeField]
    private int mistakesCountForLose = 7;

    protected override void Configure(IContainerBuilder builder)
    {
        var context = new Context(defaultLanguage);
        builder.RegisterInstance(context);
        builder.RegisterInstance<IWordsProvider>(new ShuffledWordsProvider(wordsPack.Resolve(context)));

        builder.RegisterInstance(new MviExecutor(
            internalDispatcher: new ThreadDispatcher(),
            externalDispatcher: mainThreadDispatcher
        ));
        builder.RegisterInstance(new GuessWordConfig(mistakesCountForLose));
    }
}
}