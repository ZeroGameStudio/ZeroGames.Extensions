// Copyright Zero Games. All Rights Reserved.

using ZeroGames.Extensions.Pooling;

namespace ZeroGames.Extensions.Async;

internal class PooledReactiveLifetimeBackend : IReactiveLifetimeBackend, IPooled
{

	public static PooledReactiveLifetimeBackend GetFromPool()
	{
		return _pool.Get();
	}

	public PooledReactiveLifetimeBackend()
	{
		ReactiveLifetime = ReactiveLifetime.FromBackend(this);

		_core = new(this);
		_core.Initialize();
	}

	public void SetExpired(LifetimeToken token)
	{
		_core.SetExpired(token);
		_pool.Return(this);
	}
	
	public void BindResourceLifetime(IDisposable resource, LifetimeToken token)
	{
		_core.BindResourceLifetime(resource, token);
	}

	public void BindResourceLifetime(LifetimeExpirationSource resource, LifetimeToken token)
	{
		_core.BindResourceLifetime(resource, token);
	}
	
	private static readonly ObjectPool<PooledReactiveLifetimeBackend> _pool = new();

	private LifetimeBackendCore _core;
	
	#region IReactiveLifetimeBackend Implementations
	
	public bool IsExpired(LifetimeToken token)
	{
		return _core.IsExpired(token);
	}
	
	public LifetimeExpiredRegistration RegisterOnExpired(Action callback, LifetimeToken token)
	{
		return _core.RegisterOnExpired(callback, token);
	}

	public LifetimeExpiredRegistration RegisterOnExpired(Action<object?> callback, object? state, LifetimeToken token)
	{
		return _core.RegisterOnExpired(callback, state, token);
	}

	public void UnregisterOnExpired(LifetimeExpiredRegistration registration, LifetimeToken token)
	{
		_core.UnregisterOnExpired(registration, token);
	}

	public void BindResourceLifetime(IDisposable resource)
	{
		BindResourceLifetime(resource, Token);
	}

	public void BindResourceLifetime(LifetimeExpirationSource resource)
	{
		BindResourceLifetime(resource, Token);
	}

	public Lifetime Lifetime => ReactiveLifetime;
	public ReactiveLifetime ReactiveLifetime { get; }
	public LifetimeToken Token => _core.Token;
	
	#endregion

	#region IPooled Implementations
	
	public void PreGetFromPool()
	{
		_core.Initialize();
	}

	public void PreReturnToPool()
	{
		_core.Deinitialize();
	}
	
	#endregion

}


