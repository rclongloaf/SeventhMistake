using System;
using Core.Threads;

namespace Core.Mvi.Core
{
public class MviExecutor : IDisposable
{
    public Dispatcher InternalDispatcher { get; }
    public Dispatcher ExternalDispatcher { get; }

    public MviExecutor(
        Dispatcher internalDispatcher,
        Dispatcher externalDispatcher
    )
    {
        InternalDispatcher = internalDispatcher;
        ExternalDispatcher = externalDispatcher;
    }

    public void DispatchInternal(Action action)
    {
        InternalDispatcher.Dispatch(action);
    }

    public void DispatchExternal(Action action)
    {
        ExternalDispatcher.Dispatch(action);
    }

    public void Dispose()
    {
        if (InternalDispatcher is IDisposable internalDisposable)
        {
            internalDisposable.Dispose();
        }

        if (ExternalDispatcher is IDisposable externalDisposable)
        {
            externalDisposable.Dispose();
        }
    }
}
}