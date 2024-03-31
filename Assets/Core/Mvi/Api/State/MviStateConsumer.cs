namespace Core.Mvi.Api.State
{
public interface MviStateConsumer<in ES>
where ES : ExternalMviState
{
    public void ConsumeState(ES state);
}
}