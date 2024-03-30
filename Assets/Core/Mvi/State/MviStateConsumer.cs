namespace Core.Mvi.State
{
public interface MviStateConsumer<in ES>
where ES : MviState
{
    public void BindState(ES state);
}
}