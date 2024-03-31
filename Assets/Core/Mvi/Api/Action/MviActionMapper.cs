namespace Core.Mvi.Api.Action
{
public interface MviActionMapper<out IA, in EA>
    where EA : ExternalMviAction
    where IA : InternalMviAction
{
    public IA MapAction(EA externalAction);
}
}