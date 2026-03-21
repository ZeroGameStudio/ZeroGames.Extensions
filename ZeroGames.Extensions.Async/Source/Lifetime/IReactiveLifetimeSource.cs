// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public interface IReactiveLifetimeSource : ILifetimeSource
{
	void BindResourceLifetime(IDisposable resource)
		=> ReactiveLifetime.RegisterOnExpired(static resource => resource?.To<IDisposable>().Dispose(), resource);
	
	void BindResourceLifetime(LifetimeExpirationSource resource) => BindResourceLifetime((IDisposable)resource);
	
	ReactiveLifetime ReactiveLifetime { get; }
	
	#region ILifetimeSource Implementations

	Lifetime ILifetimeSource.Lifetime => ReactiveLifetime;

	#endregion
}

public static class ReactiveLifetimeSourceMixin
{
	extension(IReactiveLifetimeSource @this)
	{
		public void BindResourceLifetime(IDisposable resource) => @this.BindResourceLifetime(resource);
		public void BindResourceLifetime(LifetimeExpirationSource resource) => @this.BindResourceLifetime(resource);

		public Lifetime Lifetime => @this.Lifetime;
	}
}

public static class ReactiveLifetimeSourceExtensions
{
	extension(IReactiveLifetimeSource @this)
	{
		public LifetimeExpirationSource CreateLifetimeExpirationSource()
		{
			LifetimeExpirationSource result = LifetimeExpirationSource.Create();
			@this.BindResourceLifetime(result);
			return result;
		}
		
		public CancellationTokenSource CreateCancellationTokenSource()
		{
			CancellationTokenSource result = new();
			@this.BindResourceLifetime(result);
			return result;
		}
	}
}


