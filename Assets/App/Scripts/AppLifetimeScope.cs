using App.Scripts.Words;
using Core.View;
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
    private TranslationWords wordsPack = null!;

    protected override void Configure(IContainerBuilder builder)
    {
        var context = new Context(defaultLanguage);
        builder.RegisterInstance(context);
        builder.RegisterInstance<IWordsProvider>(new ShuffledWordsProvider(wordsPack.Resolve(context)));
    }
}
}