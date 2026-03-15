// Copyright Zero Games. All Rights Reserved.

using System.Diagnostics.CodeAnalysis;

namespace ZeroGames.Extensions.Async;

public class Obstream<T>(T? value = default, IEqualityComparer<T>? comparer = null) : IObstream<T>
{

    private readonly record struct Rec(ObstreamRegistration Reg, Action<T, bool> Callback, Lifetime Lifetime);

    private void GuardedInvoke(Action<T, bool> cb, T value, bool initial)
    {
        try
        {
            cb(value, initial);
        }
        catch (Exception ex)
        {
            ExceptionGuard.PublishUnhandledException(ex);
        }
    }
    
    private T? _value = value;
    private readonly IEqualityComparer<T> _comparer = comparer ?? EqualityComparer<T>.Default;
    private readonly List<Rec?> _registry = [];
    private int32 _registryLock;
    private uint64 _handle;

    #region IObstream<T> Implementations
    
    public ObstreamRegistration Register(Action<T, bool> callback, Lifetime lifetime = default)
    {
        if (lifetime.IsExpired)
        {
            return default;
        }
        
        ObstreamRegistration registration = new(this, ++_handle);
        _registry.Add(new(registration, callback, lifetime));
        if (IsInitialized)
        {
            GuardedInvoke(callback, _value, true);
        }
        
        return registration;
    }

    public bool Unregister(ObstreamRegistration registration)
    {
        if (registration == default)
        {
            return false;
        }
        
        if (_registry.FindIndex(rec => rec?.Reg == registration) is var i && _registry.IsValidIndex(i))
        {
            if (_registryLock > 0)
            {
                _registry[i] = null;
            }
            else
            {
                _registry.RemoveAt(i);
            }

            return true;
        }

        return false;
    }

    [MemberNotNullWhen(true, nameof(_value))]
    public bool IsInitialized => default(T) is null || _value is not null;

    public T Value
    {
        get
        {
            if (!IsInitialized)
            {
                throw new InvalidOperationException();
            }

            return _value;
        }
        set
        {
            if (IsInitialized && _comparer.Equals(Value, value))
            {
                return;
            }
            
            bool needsCompaction = false;

            try
            {
                ++_registryLock;
                
                _value = value;
                int32 count = _registry.Count; // 执行期间加入的回调不会在本次执行。
                for (int32 i = 0; i < count; ++i)
                {
                    if (_registry[i] is not { } rec)
                    {
                        needsCompaction = true;
                        continue;
                    }

                    if (rec.Lifetime.IsExpired)
                    {
                        _registry[i] = null;
                        needsCompaction = true;
                        continue;
                    }

                    GuardedInvoke(rec.Callback, value, false);
                }
            }
            finally
            {
                --_registryLock;
            }

            if (needsCompaction && _registryLock is 0)
            {
                _registry.RemoveAll(static rec => rec is null);
            }
        }
    }

    #endregion

}


