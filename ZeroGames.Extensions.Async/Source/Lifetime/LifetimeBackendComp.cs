// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public struct LifetimeBackendComp(ILifetimeBackend backend)
{

	public void Initialize()
	{
		_handle = 0;
	}

	public void Deinitialize()
	{
		_registry?.Clear();
	}
	
	public LifetimeExpiredRegistration RegisterOnExpired(Action callback, LifetimeToken token)
	{
		ValidateReactive();
		
		if (IsExpired(token))
		{
			callback();
			return default;
		}
		
		_registry ??= new();
		LifetimeExpiredRegistration reg = new((IReactiveLifetimeBackend)_backend, ++_handle);
		_registry[reg] = new(callback, null, null);

		return reg;
	}
	
	public LifetimeExpiredRegistration RegisterOnExpired(Action<object?> callback, object? state, LifetimeToken token)
	{
		ValidateReactive();
		
		if (IsExpired(token))
		{
			callback(state);
			return default;
		}
		
		_registry ??= new();
		LifetimeExpiredRegistration reg = new((IReactiveLifetimeBackend)_backend, ++_handle);
		_registry[reg] = new(null, callback, state);

		return reg;
	}

	public void UnregisterOnExpired(LifetimeExpiredRegistration registration, LifetimeToken token)
	{
		ValidateReactive();

		if (IsExpired(token))
		{
			return;
		}
		
		_registry?.Remove(registration);
	}

	public bool IsExpired(LifetimeToken token)
	{
		return token != Token;
	}

	public void SetExpired(LifetimeToken token)
	{
		if (IsExpired(token))
		{
			return;
		}

		Token = Token.Next;
		if (_registry is not null)
		{
			foreach (var pair in _registry)
			{
				Rec rec = pair.Value;
				if (rec.StatelessCallback is not null)
				{
					rec.StatelessCallback();
				}
				else if (rec.StatefulCallback is not null)
				{
					rec.StatefulCallback(rec.State);
				}
			}
		}
	}

	public LifetimeToken Token { get; private set; }

	private void ValidateReactive()
	{
		if (_backend is not IReactiveLifetimeBackend)
		{
			throw new InvalidOperationException("Lifetime is not reactive.");
		}
	}
	
	private readonly record struct Rec(Action? StatelessCallback, Action<object?>? StatefulCallback, object? State);

	private readonly ILifetimeBackend _backend = backend;
	private uint64 _handle;
	
	private Dictionary<LifetimeExpiredRegistration, Rec>? _registry;

}


