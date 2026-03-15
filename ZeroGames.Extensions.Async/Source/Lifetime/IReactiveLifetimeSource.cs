// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public interface IReactiveLifetimeSource : ILifetimeSource
{
	ReactiveLifetime ReactiveLifetime { get; }
}


