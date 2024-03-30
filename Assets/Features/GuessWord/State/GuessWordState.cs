using System.Collections.Generic;
using Core.Mvi.State;

namespace Features.GuessWord.State
{
public interface GuessWordState : MviState
{
    public record NotStarted : GuessWordState;

    public record Active(
        IReadOnlyList<CharacterState> charactersStateList,
        int mistakesCount
    ) : GuessWordState;

    public record Lose(
        string word
    ) : GuessWordState;

    public record Win(
        string word
    ) : GuessWordState;
}
}