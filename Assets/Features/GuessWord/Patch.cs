using Core.Mvi.Patch;

namespace Features.GuessWord
{
public interface Patch : MviPatch
{
    public record Init() : Patch;
    
    public record Start(string word) : Patch;

    public record CharacterGuessed(char character) : Patch;

    public record CharacterMistake(char character) : Patch;

    public record Lose : Patch;

    public record Win : Patch;
}
}