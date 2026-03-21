// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public struct LifetimeExpirationSource : IReactiveLifetimeSource, IDisposable
{
	public static LifetimeExpirationSource Create() => new(default);

	public void SetExpired()
	{
		_backend?.SetExpired(_token);
	}
	
	public LifetimeExpiredRegistration RegisterOnExpired(Action callback)
	{
		return ReactiveLifetime.RegisterOnExpired(callback);
	}

	public LifetimeExpiredRegistration RegisterOnExpired(Action<object?> callback, object? state)
	{
		return ReactiveLifetime.RegisterOnExpired(callback, state);
	}
	
	public bool IsExpired => ReactiveLifetime.IsExpired;

	private struct GetFromPool;

	private LifetimeExpirationSource(GetFromPool getFromPool)
	{
		_backend = PooledReactiveLifetimeBackend.GetFromPool();
		_token = _backend.Token;

		// Capture token into lifetime object.
		ReactiveLifetime = ReactiveLifetime.FromBackend(_backend);
	}

	private readonly PooledReactiveLifetimeBackend? _backend;
	private readonly LifetimeToken _token;
	
	#region IReactiveLifetime Implementations

	public void BindResourceLifetime(IDisposable resource)
	{
		_backend?.BindResourceLifetime(resource);
	}

	public void BindResourceLifetime(LifetimeExpirationSource resource)
	{
		_backend?.BindResourceLifetime(resource);
	}
	
	public ReactiveLifetime ReactiveLifetime { get; }

	#endregion
	
	#region IDisposable Implementations

	public void Dispose() => SetExpired();

	#endregion
}


