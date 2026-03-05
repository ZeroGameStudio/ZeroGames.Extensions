// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions;

public static class DictionaryExtensions
{
	extension<TKey, TValue>(IDictionary<TKey, TValue> @this)
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TValue GetOrAdd(TKey key, TValue value)
		{
			if (!@this.TryGetValue(key, out var v))
			{
				v = value;
				@this[key] = v;
			}

			return v;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TValue GetOrAdd(TKey key, Func<TValue> valueFunc)
		{
			if (!@this.TryGetValue(key, out var v))
			{
				v = valueFunc();
				@this[key] = v;
			}

			return v;
		}
	}
}


