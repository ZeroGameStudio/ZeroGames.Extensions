// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions;

public static class ListExtensions
{
    extension<T>(IReadOnlyList<T> @this)
    {
        public bool IsValidIndex(int32 index) => index >= 0 && index < @this.Count;
    }
}


