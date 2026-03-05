// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions;

public static class ObjectExtensions
{
	extension(object @this)
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T? As<T>() where T : class => @this as T;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T To<T>() where T : class => (T)@this;
	}
}


