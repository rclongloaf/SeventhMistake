using System.Collections.Generic;
using Core.Mvi.Api.State;

namespace Features.GuessWord.Api
{
public interface GuessWordState : MviState
{
    public record NotStarted : GuessWordState;

    public record Active(
        IReadOnlyList<LetterState> lettersStateList,
        int mistakesCount,
        GuessWordActiveStatus status
    ) : GuessWordState;
}
}