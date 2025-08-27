// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions;

public static class ObjectExtensions
{
	extension(object @this)
	{
		public T? As<T>() where T : class => @this as T;
		public T To<T>() where T : class => (T)@this;
	}
}


