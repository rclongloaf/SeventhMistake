namespace Core.View
{
public class Context
{
    public Language Language { get; private set; }

    public Context(Language language)
    {
        Language = language;
    }
}
}