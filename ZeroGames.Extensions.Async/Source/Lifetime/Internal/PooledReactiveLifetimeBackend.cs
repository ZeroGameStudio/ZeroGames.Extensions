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

		_comp = new(this);
		_comp.Initialize();
	}

	public void SetExpired(LifetimeToken token)
	{
		_comp.SetExpired(token);
		_pool.Return(this);
	}
	
	private static readonly ObjectPool<PooledReactiveLifetimeBackend> _pool = new();

	private LifetimeBackendComp _comp;
	
	#region IReactiveLifetimeBackend Implementations
	
	public bool IsExpired(LifetimeToken token)
	{
		return _comp.IsExpired(token);
	}
	
	public LifetimeExpiredRegistration RegisterOnExpired(Action callback, LifetimeToken token)
	{
		return _comp.RegisterOnExpired(callback, token);
	}

	public LifetimeExpiredRegistration RegisterOnExpired(Action<object?> callback, object? state, LifetimeToken token)
	{
		return _comp.RegisterOnExpired(callback, state, token);
	}

	public void UnregisterOnExpired(LifetimeExpiredRegistration registration, LifetimeToken token)
	{
		_comp.UnregisterOnExpired(registration, token);
	}

	public Lifetime Lifetime => ReactiveLifetime;
	public ReactiveLifetime ReactiveLifetime { get; }
	public LifetimeToken Token => _comp.Token;
	
	#endregion

	#region IPooled Implementations
	
	public void PreGetFromPool()
	{
		_comp.Initialize();
	}

	public void PreReturnToPool()
	{
		_comp.Deinitialize();
	}
	
	#endregion

}


