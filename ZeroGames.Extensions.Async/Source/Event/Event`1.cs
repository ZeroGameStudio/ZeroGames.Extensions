// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public class Event<T> : IEvent<T>
{
    
    private readonly record struct Rec(EventRegistration Reg, Action<T> Handler, Lifetime Lifetime);
    
    private readonly List<Rec?> _invocationList = [];
    private int32 _invocationListLock;
    private uint64 _handle;
    
    #region IEvent<T> Implementations
    
    public EventRegistration Add(Action<T> handler, Lifetime lifetime = default)
    {
        if (lifetime.IsExpired)
        {
            return default;
        }
        
        EventRegistration reg = new(this, ++_handle);
        _invocationList.Add(new(reg, handler, lifetime));
        return reg;
    }

    public bool Remove(EventRegistration registration)
    {
        if (registration == default)
        {
            return false;
        }
        
        if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
        {
            if (_invocationListLock > 0)
            {
                _invocationList[i] = null;
            }
            else
            {
                _invocationList.RemoveAt(i);
            }

            return true;
        }

        return false;
    }

    public int32 RemoveAll(object? target)
    {
        if (target is null)
        {
            return 0;
        }

        int32 count = 0;
        for (int32 i = _invocationList.Count - 1; i >= 0; --i)
        {
            var rec = _invocationList[i];
            if (rec is not null && rec.Value.Handler.Target == target)
            {
                if (_invocationListLock > 0)
                {
                    _invocationList[i] = null;
                }
                else
                {
                    _invocationList.RemoveAt(i);
                }

                ++count;
            }
        }

        return count;
    }

    public void Invoke(T args)
    {
        bool needsCompaction = false;
        
        try
        {
            ++_invocationListLock;
            
            int32 count = _invocationList.Count; // 执行期间加入的回调不会在本次执行。
            for (int32 i = 0; i < count; ++i)
            {
                if (_invocationList[i] is not { } rec)
                {
                    needsCompaction = true;
                    continue;
                }

                if (rec.Lifetime.IsExpired)
                {
                    _invocationList[i] = null;
                    needsCompaction = true;
                    continue;
                }

                try
                {
                    rec.Handler(args);
                }
                catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
                {
                    ExceptionGuard.PublishUnhandledException(ex);
                }
            }
        }
        finally
        {
            --_invocationListLock;
        }
        
        if (needsCompaction && _invocationListLock is 0)
        {
            _invocationList.RemoveAll(static rec => rec is null);
        }
    }

    public Func<Exception, bool>? ExceptionHandler { get; set; }

    #endregion
    
}


