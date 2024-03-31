using Core.Mvi.Api.Patch;
using Features.GuessWord.Api;

namespace Features.GuessWordsScene.Core
{
public interface Patch : MviPatch
{
    public record UpdateActiveGuessWordState(GuessWordState.Active guessWordState) : Patch;
}
}