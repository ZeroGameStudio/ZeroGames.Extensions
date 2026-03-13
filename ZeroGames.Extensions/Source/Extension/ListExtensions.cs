// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions;

public static class ListExtensions
{
    extension<T>(IReadOnlyList<T> @this)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValidIndex(int32 index) => index >= 0 && index < @this.Count;
    }
}


