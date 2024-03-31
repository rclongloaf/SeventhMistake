namespace Features.GuessWord.Api
{
public interface LetterState
{
    public record NotGuessed : LetterState;

    public record Guessed(char letter) : LetterState;
}
}