// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Async;

public partial struct Lifetime
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Lifetime FromBackend(ILifetimeBackend backend) => new(backend);
	
	public static Lifetime NeverExpired => default;
	public static Lifetime Expired => new(true);
}


