// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

partial struct Vector2D
{

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D GetClampedToSize(double min, double max)
	{
		double vecSize = Size;
		Vector2D vecDir = vecSize > SMALL_NUMBER ? this / vecSize : Zero;
		double clampedSize = System.Math.Clamp(vecSize, min, max);
		return clampedSize * vecDir;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D GetClampedToMaxSize(double maxSize)
	{
		if (maxSize < KINDA_SMALL_NUMBER)
		{
			return Zero;
		}

		double vSq = SizeSquared;
		if (vSq > Square(maxSize))
		{
			double scale = maxSize / Sqrt(vSq);
			return new(X * scale, Y * scale);
		}
		return this;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D ComponentMin(Vector2D other) => new(System.Math.Min(X, other.X), System.Math.Min(Y, other.Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D ComponentMax(Vector2D other) => new(System.Math.Max(X, other.X), System.Math.Max(Y, other.Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D GetRotated(double angleDeg)
	{
		double angleRad = DegreesToRadians(angleDeg);
		var (sin, cos) = SinCos(angleRad);
		return new(cos * X - sin * Y, sin * X + cos * Y);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ToDirectionAndLength(out Vector2D direction, out double length)
	{
		length = Size;
		if (length > SMALL_NUMBER)
		{
			double oneOverLength = 1.0 / length;
			direction = new(X * oneOverLength, Y * oneOverLength);
		}
		else
		{
			direction = Zero;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D RoundToVector() => new(Round(X), Round(Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D ClampAxes(double min, double max) => new(System.Math.Clamp(X, min, max), System.Math.Clamp(Y, min, max));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Distance(Vector2D other) => Sqrt(DistanceSquared(other));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double DistanceSquared(Vector2D other) => Square(X - other.X) + Square(Y - other.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Normalize()
	{
		double squareSum = X * X + Y * Y;
		if (squareSum > SMALL_NUMBER)
		{
			double scale = 1 / Sqrt(squareSum);
			X *= scale;
			Y *= scale;
			return true;
		}
		X = 0;
		Y = 0;
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D MakeFromAngleAndLength(double angleDeg, double length)
	{
		double angleRad = DegreesToRadians(angleDeg);
		return new(Cos(angleRad) * length, Sin(angleRad) * length);
	}

}
