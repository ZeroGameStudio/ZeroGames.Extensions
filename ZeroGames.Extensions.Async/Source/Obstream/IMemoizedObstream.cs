// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public interface IMemoizedObstreamEntry<out T> : IReadOnlyObstream<T>
{
    ObstreamRegistration Register(Action<T?, T, bool> callback, Lifetime lifetime = default);
}

public interface IMemoizedObstream<T> : IMemoizedObstreamEntry<T>
{
    new T Value { get; set; }
    T? LastValue { get; }
}


