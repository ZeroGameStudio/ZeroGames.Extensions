// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public readonly record struct ObstreamRegistration(IUnregisterObstreamSource? Owner, uint64 Handle) : IDisposable, IMixin_IsValidByDefault<ObstreamRegistration>
{
    #region IDisposable Implementations

    public void Dispose() => Owner?.Unregister(this);

    #endregion
}

public interface IUnregisterObstreamSource
{
    bool Unregister(ObstreamRegistration registration);
    bool IsInitialized { get; }
}

public interface IReadOnlyObstream<out T> : IUnregisterObstreamSource
{
    T Value { get; }
}

public interface IObstreamEntry<out T> : IReadOnlyObstream<T>
{
    ObstreamRegistration Register(Action<T, bool> callback, Lifetime lifetime = default);
}

public interface IObstream<T> : IObstreamEntry<T>
{
    new T Value { get; set; }
}


