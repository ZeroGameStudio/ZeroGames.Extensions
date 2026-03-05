// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

public static class MathExtensions
{
	extension(System.Math)
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double RadiansToDegrees(double x) => x * RADIAN_TO_DEGREE;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double DegreesToRadians(double x) => x * DEGREE_TO_RADIAN;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double FMod(double x, double y) => x - y * Truncate(x / y);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFinite(double x)
		{
			return !double.IsNaN(x) && !double.IsInfinity(x);
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double GridSnap(double location, double grid)
		{
			return grid == 0 ? location : Floor((location + grid / 2) / grid) * grid;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Square(double x) => x * x;
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Lerp(double a, double b, double t) => a + (b - a) * t;
	}
}


