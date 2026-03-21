// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions;

public static class ObjectExtensions
{
	extension(object @this)
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T? As<T>() => @this is T t ? t : default;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T To<T>() => (T)@this;
	}
}


