// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions;

public static class EnumExtensions
{
	extension<T>(T @this) where T : Enum
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasAllFlags(T flags)
		{
			return @this.HasFlag(flags);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool HasAnyFlags(T flags)
		{
			Type underlyingType = typeof(T).GetEnumUnderlyingType();
			if (underlyingType == typeof(uint64))
			{
				return (Unsafe.As<T, uint64>(ref @this) & Unsafe.As<T, uint64>(ref flags)) != 0;
			}
			else if (underlyingType == typeof(int64))
			{
				return (Unsafe.As<T, int64>(ref @this) & Unsafe.As<T, int64>(ref flags)) != 0;
			}
			if (underlyingType == typeof(uint32))
			{
				return (Unsafe.As<T, uint32>(ref @this) & Unsafe.As<T, uint32>(ref flags)) != 0;
			}
			else if (underlyingType == typeof(int32))
			{
				return (Unsafe.As<T, int32>(ref @this) & Unsafe.As<T, int32>(ref flags)) != 0;
			}
			if (underlyingType == typeof(uint16))
			{
				return (Unsafe.As<T, uint16>(ref @this) & Unsafe.As<T, uint16>(ref flags)) != 0;
			}
			else if (underlyingType == typeof(int16))
			{
				return (Unsafe.As<T, int16>(ref @this) & Unsafe.As<T, int16>(ref flags)) != 0;
			}
			if (underlyingType == typeof(uint8))
			{
				return (Unsafe.As<T, uint8>(ref @this) & Unsafe.As<T, uint8>(ref flags)) != 0;
			}
			else if (underlyingType == typeof(int8))
			{
				return (Unsafe.As<T, int8>(ref @this) & Unsafe.As<T, int8>(ref flags)) != 0;
			}
			else
			{
				throw new InvalidOperationException();
			}
		}
	}
}


