// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public struct LifetimeExpirationSource : IReactiveLifetimeSource
{
	public static LifetimeExpirationSource Create() => new(default);

	public void SetExpired()
	{
		_backend?.SetExpired(_token);
	}
	
	public LifetimeExpiredRegistration RegisterOnExpired(Action callback)
	{
		return Lifetime.RegisterOnExpired(callback);
	}

	public LifetimeExpiredRegistration RegisterOnExpired(Action<object?> callback, object? state)
	{
		return Lifetime.RegisterOnExpired(callback, state);
	}

	public ReactiveLifetime Lifetime => ReactiveLifetime.FromBackend(_backend);
	public bool IsExpired => Lifetime.IsExpired;

	private struct GetFromPool;

	private LifetimeExpirationSource(GetFromPool getFromPool)
	{
		_backend = PooledReactiveLifetimeBackend.GetFromPool();
		_token = _backend.Token;
	}

	private readonly PooledReactiveLifetimeBackend? _backend;
	private readonly LifetimeToken _token;
	
	#region IReactiveLifetime Implementations
	
	Lifetime ILifetimeSource.Lifetime => Lifetime;
	ReactiveLifetime IReactiveLifetimeSource.ReactiveLifetime => Lifetime;

	#endregion
}


