using Core.Mvi.Api.Patch;

namespace Features.GuessWord.Core.Patch
{
public interface Patch : MviPatch
{
    public record Start(string word) : Patch;

    public record LetterGuessed(char letter) : Patch;

    public record LetterMistake(char letter) : Patch;

    public record Lose : Patch;

    public record Win : Patch;
}
}