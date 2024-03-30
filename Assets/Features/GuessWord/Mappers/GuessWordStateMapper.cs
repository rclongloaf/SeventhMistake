using System;
using System.Collections.Generic;
using Core.Mvi.State;
using Features.GuessWord.State;

namespace Features.GuessWord.Mappers
{
public class GuessWordStateMapper : MviStateMapper<InternalState, GuessWordState>
{
    public GuessWordState MapState(InternalState internalState)
    {
        switch (internalState)
        {
            case InternalState.Active state:
                var charactersState = new List<CharacterState>();

                foreach (var character in state.word)
                {
                    if (state.guessedCharacters.Contains(character))
                    {
                        charactersState.Add(new CharacterState.Guessed(character));
                    }
                    else
                    {
                        charactersState.Add(new CharacterState.NotGuessed());
                    }
                }

                return new GuessWordState.Active(
                    charactersStateList: charactersState,
                    mistakesCount: state.mistakesCount
                );
            
            case InternalState.Lose state:
                return new GuessWordState.Lose(state.word);
            
            case InternalState.Win state:
                return new GuessWordState.Win(state.word);
            
            case InternalState.WaitingForStart:
            case InternalState.WaitInit:
                return new GuessWordState.NotStarted();
            
            default:
                throw new ArgumentOutOfRangeException(nameof(internalState));
        }
    }
}
}