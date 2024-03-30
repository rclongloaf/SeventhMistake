using System.Collections.Generic;
using Core.Mvi.State;

namespace Features.GuessWord.State
{
public interface InternalState : InternalMviState
{
    public record WaitInit : InternalState;
    
    public record WaitingForStart : InternalState;

    public record Active(
        string word,
        HashSet<char> guessedCharacters,
        HashSet<char> checkedCharacters,
        int mistakesCount
    ) : InternalState;

    public record Lose(
        string word
    ) : InternalState;

    public record Win(
        string word
    ) : InternalState;
}
}