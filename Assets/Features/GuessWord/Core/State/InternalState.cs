using System.Collections.Generic;
using Core.Mvi.Api.State;
using Features.GuessWord.Api;

namespace Features.GuessWord.Core.State
{
public interface InternalState : InternalMviState
{
    public record WaitingForStart : InternalState;

    public record Active(
        string word,
        HashSet<char> guessedLetters,
        HashSet<char> checkedLetters,
        int mistakesCount,
        GuessWordActiveStatus status
    ) : InternalState;
}
}