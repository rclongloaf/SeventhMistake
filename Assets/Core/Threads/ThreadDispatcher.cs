using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Core.Threads
{
public class ThreadDispatcher : Dispatcher, IDisposable
{
    private EventWaitHandle ewh;
    private Thread thread;

    private ConcurrentQueue<Action> actions = new();
    private volatile bool isDisposed;

    public ThreadDispatcher()
    {
        thread = new Thread(DoWork);
        ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
        thread.Start();
    }

    public void Dispatch(Action action)
    {
        if (Thread.CurrentThread == thread)
        {
            action.Invoke();
        }
        else
        {
            actions.Enqueue(action);
            ewh.Set();
        }
    }

    private void DoWork()
    {
        while (!isDisposed)
        {
            while (actions.TryDequeue(out var action))
            {
                action.Invoke();
            }

            ewh.Reset();
            ewh.WaitOne();
        }
    }

    public void Dispose()
    {
        isDisposed = true;
        ewh.Dispose();
        actions.Clear();
    }
}
}