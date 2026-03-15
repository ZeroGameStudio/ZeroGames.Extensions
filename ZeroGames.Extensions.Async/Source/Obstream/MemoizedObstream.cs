// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public class MemoizedObstream<T>(T? value = default, IEqualityComparer<T>? comparer = null) : IMemoizedObstream<T>
{

    private readonly Obstream<T> _inner = new(value, comparer);
    
    #region IMemoizedObstream<T> Implementations
    
    public ObstreamRegistration Register(Action<T?, T, bool> callback, Lifetime lifetime = default)
    {
        if (lifetime.IsExpired)
        {
            return default;
        }

        return _inner.Register((value, initial) => callback(LastValue, value, initial), lifetime);
    }
    
    public bool Unregister(ObstreamRegistration registration)
        => _inner.Unregister(registration);
    
    public bool IsInitialized => _inner.IsInitialized;

    public T Value
    {
        get => _inner.Value;
        set
        {
            if (IsInitialized)
            {
                LastValue = _inner.Value;
            }
            _inner.Value = value;
        }
    }

    public T? LastValue
    {
        get
        {
            if (!IsInitialized)
            {
                throw new InvalidOperationException();
            }

            return field;
        }
        private set;
    }

    #endregion

}


