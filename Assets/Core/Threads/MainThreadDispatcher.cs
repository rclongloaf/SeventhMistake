using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;

namespace Core.Threads
{
public class MainThreadDispatcher : MonoBehaviour, Dispatcher
{
    private Thread mainThread = null!;
    private ConcurrentQueue<Action> actions = new();

    private void Awake()
    {
        mainThread = Thread.CurrentThread;
    }

    private void Update()
    {
        while (actions.TryDequeue(out var action))
        {
            action.Invoke();
        }
    }

    public void Dispatch(Action action)
    {
        if (Thread.CurrentThread == mainThread)
        {
            action.Invoke();
        }
        else
        {
            actions.Enqueue(action);
        }
    }

    public void OnDestroy()
    {
        actions.Clear();
    }
}
}