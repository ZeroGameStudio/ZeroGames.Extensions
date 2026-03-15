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

    extension<T>(IList<T> @this)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddUnique(T item)
        {
            if (@this.Contains(item))
            {
                return;
            }
            
            @this.Add(item);
        }
	    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAtSwap(int32 index)
        {
            int32 lastIndex = @this.Count - 1;
            if (index != lastIndex)
            {
                (@this[index], @this[lastIndex]) = (@this[lastIndex], @this[index]);
            }

            @this.RemoveAt(lastIndex);
        }
    }
}


