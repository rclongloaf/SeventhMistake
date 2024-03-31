using Core.Mvi.Api.Action;

namespace Features.GuessWordsScene.Core
{
public interface InternalAction : InternalMviAction
{
    public record StartNewGame : InternalAction;

    public record GuessLetter(char character) : InternalAction;
}
}