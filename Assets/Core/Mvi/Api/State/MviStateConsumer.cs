namespace Core.Mvi.Api.State
{
public interface MviStateConsumer<in ES>
where ES : MviState
{
    public void ConsumeState(ES state);
}
}