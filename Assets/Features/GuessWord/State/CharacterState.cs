namespace Features.GuessWord.State
{
public interface CharacterState
{
    public record NotGuessed : CharacterState;

    public record Guessed(char character) : CharacterState;
}
}