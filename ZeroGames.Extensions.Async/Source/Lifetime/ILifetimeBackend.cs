// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public interface ILifetimeBackend : ILifetimeSource
{
	bool IsExpired(LifetimeToken token);
	
	LifetimeToken Token { get; }
}


