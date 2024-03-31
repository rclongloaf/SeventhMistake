using System;
using System.Collections.Generic;
using Core.Mvi.Api.State;
using Features.GuessWord.Api;
using Features.GuessWord.Core.State;

namespace Features.GuessWord.Core.Mappers
{
public class GuessWordStateMapper : MviStateMapper<InternalState, GuessWordState>
{
    public GuessWordState MapState(InternalState internalState)
    {
        switch (internalState)
        {
            case InternalState.Active state:
                var lettersState = new List<LetterState>();

                foreach (var letter in state.word)
                {
                    if (state.status != GuessWordActiveStatus.InProgress
                        || state.guessedLetters.Contains(letter))
                    {
                        lettersState.Add(new LetterState.Guessed(letter));
                    }
                    else
                    {
                        lettersState.Add(new LetterState.NotGuessed());
                    }
                }

                return new GuessWordState.Active(
                    lettersStateList: lettersState,
                    mistakesCount: state.mistakesCount,
                    status: state.status
                );
            
            case InternalState.WaitingForStart:
                return new GuessWordState.NotStarted();
            
            default:
                throw new ArgumentOutOfRangeException(nameof(internalState));
        }
    }
}
}