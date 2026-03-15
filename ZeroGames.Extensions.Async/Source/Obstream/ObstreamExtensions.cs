// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public static class ObstreamExtensions
{

    extension<T>(IObstream<T> @this)
    {
        public IObstream<T> WithLifetime(Lifetime lifetime) => new ObstreamWithLifetime<T>(@this, lifetime);
    }
    
    private class ObstreamWithLifetime<T>(IObstream<T> source, Lifetime lifetime) : IObstream<T>
    {

        private readonly IObstream<T> _source = source;
        private readonly Lifetime _lifetime = lifetime;
        
        #region IObstream<T> Implementations
        
        public ObstreamRegistration Register(Action<T, bool> callback, Lifetime lifetime = default)
        {
            if (_lifetime.IsExpired || lifetime.IsExpired)
            {
                return default;
            }
            
            return _source.Register((value, initial) =>
            {
                if (!_lifetime.IsExpired)
                {
                    callback(value, initial);
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
        
        #endregion
        
    }
    
}


