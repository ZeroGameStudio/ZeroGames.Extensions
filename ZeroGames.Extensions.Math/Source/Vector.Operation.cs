// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

partial struct Vector
{
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Normalize()
	{
		double squareSum = X * X + Y * Y + Z * Z;
		if (squareSum > SMALL_NUMBER)
		{
			double scale = 1 / Sqrt(squareSum);
			X *= scale;
			Y *= scale;
			Z *= scale;
			return true;
		}
		X = 0;
		Y = 0;
		Z = 0;
		return false;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Distance(Vector other) => Sqrt(DistanceSquared(other));
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double DistanceSquared(Vector other) => (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y) + (Z - other.Z) * (Z - other.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Distance2D(Vector other) => Sqrt(DistanceSquared2D(other));
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double DistanceSquared2D(Vector other) => (X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y);
	
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector GetClampedToSize(double min, double max)
	{
		double vecSize = Size;
		Vector vecDir = vecSize > SMALL_NUMBER ? this / vecSize : Zero;
		double clampedSize = System.Math.Clamp(vecSize, min, max);
		return clampedSize * vecDir;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector GetClampedToMaxSize(double maxSize)
	{
		if (maxSize < KINDA_SMALL_NUMBER)
		{
			return Zero;
		}

		double vSq = SizeSquared;
		if (vSq > maxSize * maxSize)
		{
			double scale = maxSize / Sqrt(vSq);
			return new(X * scale, Y * scale, Z * scale);
		}
		return this;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector ComponentMin(Vector other) => new(System.Math.Min(X, other.X), System.Math.Min(Y, other.Y), System.Math.Min(Z, other.Z));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector ComponentMax(Vector other) => new(System.Math.Max(X, other.X), System.Math.Max(Y, other.Y), System.Math.Max(Z, other.Z));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector GridSnap(Vector gridSize)
	{
		return new(
			System.Math.GridSnap(X, gridSize.X),
			System.Math.GridSnap(Y, gridSize.Y),
			System.Math.GridSnap(Z, gridSize.Z)
		);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector Project(Vector direction)
	{
		double dotProduct = Dot(direction);
		double directionSizeSquared = direction.SizeSquared;
		if (directionSizeSquared > SMALL_NUMBER)
		{
			return direction * (dotProduct / directionSizeSquared);
		}
		return Zero;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector ProjectOnTo(Vector other)
	{
		double dotProduct = Dot(other);
		double otherSizeSquared = other.SizeSquared;
		if (otherSizeSquared > SMALL_NUMBER)
		{
			return other * (dotProduct / otherSizeSquared);
		}
		return Zero;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector ProjectOnToNormal(Vector normal)
	{
		return normal * Dot(normal);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector NearestPointOnSegment(Vector start, Vector end)
	{
		double segSizeSquared = start.DistanceSquared(end);
		if (segSizeSquared <= SMALL_NUMBER)
		{
			return start;
		}

		// Find the projection of the point onto the line containing the segment
		double t = ((end.X - start.X) * (X - start.X) + (end.Y - start.Y) * (Y - start.Y) + (end.Z - start.Z) * (Z - start.Z)) / segSizeSquared;

		// Clamp t to the segment [0, 1]
		double clampedT = System.Math.Clamp(t, 0.0, 1.0);

		return new(
			start.X + clampedT * (end.X - start.X),
			start.Y + clampedT * (end.Y - start.Y),
			start.Z + clampedT * (end.Z - start.Z)
		);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector MirrorBy(Vector normal)
	{
		Vector normalizedNormal = normal.Normalized;
		return this - normalizedNormal * (2.0 * Dot(normalizedNormal));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double PointPlaneDist(Vector planeBase, Vector planeNormal)
	{
		return (this - planeBase).Dot(planeNormal.Normalized);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double CosineAngle2D(Vector other)
	{
		Vector a = Normalized;
		Vector b = other.Normalized;
		return a | b;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector Clamp(Vector v, Vector min, Vector max)
	{
		return new(System.Math.Clamp(v.X, min.X, max.X), System.Math.Clamp(v.Y, min.Y, max.Y), System.Math.Clamp(v.Z, min.Z, max.Z));
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector Max(Vector a, Vector b) => new(System.Math.Max(a.X, b.X), System.Math.Max(a.Y, b.Y), System.Math.Max(a.Z, b.Z));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector Min(Vector a, Vector b) => new(System.Math.Min(a.X, b.X), System.Math.Min(a.Y, b.Y), System.Math.Min(a.Z, b.Z));

}


