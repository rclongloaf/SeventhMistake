using Core.Mvi.Api.Action;

namespace Features.GuessWord.Api
{
public interface GuessWordAction : InternalMviAction, ExternalMviAction
{
    public record Start(string word) : GuessWordAction;

    public record GuessLetter(char letter) : GuessWordAction;
}
}