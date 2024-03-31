using Core.Mvi.Api.Action;
using Features.GuessWord.Api;

namespace Features.GuessWord.Core.Mappers
{
public class GuessWordActionMapper : MviActionMapper<GuessWordAction, GuessWordAction>
{
    public GuessWordAction MapAction(GuessWordAction externalAction)
    {
        return externalAction;
    }
}
}