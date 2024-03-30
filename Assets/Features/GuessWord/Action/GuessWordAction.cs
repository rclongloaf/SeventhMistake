using Core.Mvi.Action;

namespace Features.GuessWord.Action
{
public interface GuessWordAction : InternalMviAction, MviAction
{
    public record Start(string word) : GuessWordAction;
    public record GuessCharacter(char character) : GuessWordAction;
}
}