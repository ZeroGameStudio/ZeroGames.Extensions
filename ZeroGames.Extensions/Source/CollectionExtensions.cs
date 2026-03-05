// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions;

public static class CollectionExtensions
{
	extension<T>(IReadOnlyCollection<T> @this)
	{
		public bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => @this.Count is 0;
		}
	}
}


