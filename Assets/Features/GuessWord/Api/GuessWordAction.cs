using Core.Mvi.Api.Action;

namespace Features.GuessWord.Api
{
public interface GuessWordAction : InternalMviAction, MviAction
{
    public record Start(string word) : GuessWordAction;

    public record GuessLetter(char letter) : GuessWordAction;
}
}