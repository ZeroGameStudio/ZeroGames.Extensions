// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

partial struct Quaternion
{

	public Rotator Rotator
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			const double SINGULARITY_THRESHOLD = 0.4999995;

			double pitch, yaw, roll;
			double singularityTest = Z * X - W * Y;
			if (singularityTest < -SINGULARITY_THRESHOLD)
			{
				(pitch, yaw, roll) = (-90, Rotator.NormalizeAxis(-DEGREE_TO_RADIAN_DOUBLE * Atan2(X, W)), 0);
			}
			else if (singularityTest > SINGULARITY_THRESHOLD)
			{
				(pitch, yaw, roll) = (-90, Rotator.NormalizeAxis(RADIAN_TO_DEGREE_DOUBLE * Atan2(X, W)), 0);
			}
			else
			{
				pitch = RADIAN_TO_DEGREE * Asin(2 * singularityTest);
				yaw = RADIAN_TO_DEGREE * Atan2(2 * (W * Z + X * Y), 1 - 2 * (Y * Y + Z * Z));
				roll = RADIAN_TO_DEGREE * Atan2(-2 * (W * X + Y * Z), 1 - 2 * (X * X + Y * Y));
			}

			return new(pitch, yaw, roll);
		}
	}

	public Vector Vector
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return Rotator.Vector;
		}
	}
	
	public Quaternion Inverse
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			double scale = 1.0 / SizeSquared;
			return new(-X * scale, -Y * scale, -Z * scale, W * scale);
		}
	}

	public double Size
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Sqrt(SizeSquared);
	}

	public double SizeSquared
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => X * X + Y * Y + Z * Z + W * W;
	}

	public bool IsNormalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Abs(1.0 - SizeSquared) < THRESH_VECTOR_NORMALIZED;
	}

	public Quaternion Normalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			double scale = 1.0 / Sqrt(SizeSquared);
			return new(X * scale, Y * scale, Z * scale, W * scale);
		}
	}

	public bool ContainsNaN
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !IsFinite(X) || !IsFinite(Y) || !IsFinite(Z) || !IsFinite(W);
	}
	
}


