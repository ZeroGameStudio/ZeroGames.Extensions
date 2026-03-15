// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public readonly partial struct ReactiveLifetime
{

	public static implicit operator ReactiveLifetime(CancellationToken cancellationToken) => new(cancellationToken);
	public static implicit operator Lifetime(ReactiveLifetime @this)
	{
		if (@this._backend is null)
		{
			return @this._token.IsInlineExpired ? Lifetime.Expired : Lifetime.NeverExpired;
		}

		if (@this._backend is ILifetimeBackend backend)
		{
			return Lifetime.FromBackend(backend);
		}

		return (CancellationToken)@this._backend;
	}

	public Lifetime ForceNonReactive() => ((Lifetime)this).ForceNonReactive();
	
	public LifetimeExpiredRegistration RegisterOnExpired(Action callback)
	{
		if (_backend is null)
		{
			if (_token.IsInlineExpired)
			{
				callback();
			}
			return default;
		}

		if (_backend is IReactiveLifetimeBackend backend)
		{
			if (backend.IsExpired(_token))
			{
				callback();
				return default;
			}
			
			return backend.RegisterOnExpired(callback, _token);
		}
		
		return new(((CancellationToken)_backend).Register(callback, true));
	}
	
	public LifetimeExpiredRegistration RegisterOnExpired(Action<object?> callback, object? state)
	{
		if (_backend is null)
		{
			if (_token.IsInlineExpired)
			{
				callback(state);
			}
			return default;
		}

		if (_backend is IReactiveLifetimeBackend backend)
		{
			if (backend.IsExpired(_token))
			{
				callback(state);
				return default;
			}
			
			return backend.RegisterOnExpired(callback, state, _token);
		}
		
		return new(((CancellationToken)_backend).Register(callback, state, true));
	}

	public bool IsExpired
	{
		get
		{
			if (_backend is null)
			{
				return _token.IsInlineExpired;
			}

			if (_backend is IReactiveLifetimeBackend backend)
			{
				return backend.IsExpired(_token);
			}
			
			return ((CancellationToken)_backend).IsCancellationRequested;
		}
	}
	
	private ReactiveLifetime(IReactiveLifetimeBackend backend)
	{
		if (backend.IsExpired(backend.Token))
		{
			_token = LifetimeToken.InlineExpired;
		}
		else
		{
			_backend = backend;
			_token = backend.Token;
		}
	}
	
	private ReactiveLifetime(CancellationToken cancellationToken)
	{
		if (cancellationToken.IsCancellationRequested)
		{
			_token = LifetimeToken.InlineExpired;
		}
		else if (cancellationToken.CanBeCanceled)
		{
			_backend = cancellationToken;
		}
	}

	private ReactiveLifetime(bool inlineExpired)
	{
		if (inlineExpired)
		{
			_token = LifetimeToken.InlineExpired;
		}
	}

	// WeakReference<IReactiveLifetimeBackend>, CancellationToken or CancellationTokenSource
	private readonly object? _backend;
	private readonly LifetimeToken _token;
	
}


