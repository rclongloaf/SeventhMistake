using Core.Mvi.Action;

namespace Features.GuessWord.Action
{
public interface InternalAction : InternalMviAction, MviAction
{
    public record Init : InternalAction;

    public record Start(string word) : InternalAction;

    public record GuessCharacter(char character) : InternalAction;
}
}