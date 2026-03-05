// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

partial struct Vector
{
	
	public double Size
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Sqrt(SizeSquared);
	}
	
	public double SizeSquared
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => X * X + Y * Y + Z * Z;
	}

	public double Size2D
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Sqrt(SizeSquared2D);
	}

	public double SizeSquared2D
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => X * X + Y * Y;
	}

	public bool IsNearlyZero
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Abs(X) <= KINDA_SMALL_NUMBER && Abs(Y) <= KINDA_SMALL_NUMBER && Abs(Z) <= KINDA_SMALL_NUMBER;
	}

	public bool IsZero
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => X == 0 && Y == 0 && Z == 0;
	}

	public bool IsNormalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Abs(1 - SizeSquared) < THRESH_VECTOR_NORMALIZED;
	}

	public bool IsNormalized2D
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Abs(1 - SizeSquared2D) < THRESH_VECTOR_NORMALIZED;
	}

	public Vector Normalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => SizeSquared >= THRESH_VECTOR_NORMALIZED ? this / Size : Zero;
	}

	public Vector Normalized2D
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => SizeSquared2D >= THRESH_VECTOR_NORMALIZED ? this with { Z = 0 } / Size2D : Zero;
	}
	
	public Rotator Rotator
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			double pitch = RADIAN_TO_DEGREE * Atan2(Z, Size2D);
			double yaw = RADIAN_TO_DEGREE * Atan2(Y, X);
			return new(pitch, yaw, 0);
		}
	}

	public Quaternion Quaternion
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Rotator.Quaternion;
	}
	
	public Vector Sign
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => new(Sign(X), Sign(Y), Sign(Z));
	}

	public bool ContainsNaN
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !IsFinite(X) || !IsFinite(Y) || !IsFinite(Z);
	}
	
}


