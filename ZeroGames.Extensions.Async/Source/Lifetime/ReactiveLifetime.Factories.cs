// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Async;

public partial struct ReactiveLifetime
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static ReactiveLifetime FromBackend(IReactiveLifetimeBackend? backend) => backend is not null ? new(backend) : NeverExpired;
	
	public static ReactiveLifetime NeverExpired => default;
	public static ReactiveLifetime Expired => new(true);
}


