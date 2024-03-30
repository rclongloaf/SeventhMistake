using System;
using Core.Mvi.Action;
using Features.GuessWord.Action;

namespace Features.GuessWord.Mappers
{
public class GuessWordActionMapper : MviActionMapper<InternalAction, GuessWordAction>
{
    public InternalAction MapAction(GuessWordAction externalAction)
    {
        return externalAction switch
        {
            GuessWordAction.Start action => new InternalAction.Start(action.word),
            GuessWordAction.GuessCharacter action => new InternalAction.GuessCharacter(action.character),
            _ => throw new ArgumentOutOfRangeException(nameof(externalAction))
        };
    }
}
}