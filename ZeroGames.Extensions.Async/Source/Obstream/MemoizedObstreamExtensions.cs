// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public static class MemoizedObstreamExtensions
{

    extension<T>(IMemoizedObstream<T> @this)
    {
        public IMemoizedObstream<T> WithLifetime(Lifetime lifetime) => new MemoizedObstreamWithLifetime<T>(@this, lifetime);
    }
    
    private class MemoizedObstreamWithLifetime<T>(IMemoizedObstream<T> source, Lifetime lifetime) : IMemoizedObstream<T>
    {

        private readonly IMemoizedObstream<T> _source = source;
        private readonly Lifetime _lifetime = lifetime;
        
        #region IObstream<T> Implementations
        
        public ObstreamRegistration Register(Action<T?, T, bool> callback, Lifetime lifetime = default)
        {
            if (_lifetime.IsExpired || lifetime.IsExpired)
            {
                return default;
            }

            return _source.Register((oldValue, newValue, initial) =>
            {
                if (!_lifetime.IsExpired)
                {
                    callback(oldValue, newValue, initial);
                }
            }, lifetime);
        }
        
        public bool Unregister(ObstreamRegistration registration)
            => _source.Unregister(registration);
        
        public bool IsInitialized => _source.IsInitialized;

        public T Value
        {
            get => _source.Value;
            set => _source.Value = value;
        }

        public T? LastValue => _source.LastValue;

        #endregion

    }
    
}


