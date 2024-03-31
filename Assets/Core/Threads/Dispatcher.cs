using System;

namespace Core.Threads
{
public interface Dispatcher
{
    public void Dispatch(Action action);
}
}