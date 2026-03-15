// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public readonly partial struct Lifetime
{
	
	public static implicit operator Lifetime(CancellationToken cancellationToken) => new(cancellationToken);

	public Lifetime ForceNonReactive() => new(this, true);

	public bool TryToReactive(out ReactiveLifetime reactiveLifetime)
	{
		// Never or Expired.
		if (_backend is null)
		{
			reactiveLifetime = _token.IsInlineExpired ? ReactiveLifetime.Expired : ReactiveLifetime.NeverExpired;
			return true;
		}
		
		// Backend is not reactive or force non-reactive.
		if (_nonReactive)
		{
			reactiveLifetime = default;
			return false;
		}
		
		// If _backend is ILifetimeBackend then it must be reactive.
		if (_backend is IReactiveLifetimeBackend backend)
		{
			reactiveLifetime = ReactiveLifetime.FromBackend(backend);
			return true;
		}
		
		reactiveLifetime = (CancellationToken)_backend;
		return true;
	}

	public bool IsExpired
	{
		get
		{
			if (_backend is null)
			{
				return _token.IsInlineExpired;
			}

			if (_backend is ILifetimeBackend backend)
			{
				return backend.IsExpired(_token);
			}
			
			return ((CancellationToken)_backend).IsCancellationRequested;
		}
	}

	private Lifetime(ILifetimeBackend backend)
	{
		if (backend.IsExpired(backend.Token))
		{
			_token = LifetimeToken.InlineExpired;
		}
		else
		{
			// Keep a weak reference to backend is enough because dead implies expired.
			_backend = backend;
			_token = backend.Token;
			_nonReactive = backend is not IReactiveLifetimeBackend;
		}
	}
	
	private Lifetime(CancellationToken cancellationToken)
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
	
	private Lifetime(bool inlineExpired)
	{
		if (inlineExpired)
		{
			_token = LifetimeToken.InlineExpired;
		}
	}
	
	private Lifetime(Lifetime other, bool nonReactive)
	{
		if (other.IsExpired)
		{
			_token = LifetimeToken.InlineExpired;
		}
		else
		{
			_backend = other._backend;
			_token = other._token;
			_nonReactive = nonReactive;
		}
	}

	// ILifetimeBackend or CancellationToken.
	private readonly object? _backend;
	private readonly LifetimeToken _token;
	private readonly bool _nonReactive;
	
}


