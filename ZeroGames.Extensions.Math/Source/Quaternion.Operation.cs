// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

public partial struct Quaternion
{
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double AngularDistance(Quaternion other)
	{
		// Calculate the dot product
		double dotProduct = X * other.X + Y * other.Y + Z * other.Z + W * other.W;

		// Clamp the dot product to [-1, 1] to handle floating point errors
		double clampedDot = Clamp(dotProduct, -1.0, 1.0);

		// Calculate the angle (in radians)
		double angleRadians = Acos(Abs(clampedDot)) * 2.0;

		// Convert to degrees
		return RadiansToDegrees(angleRadians);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Normalize()
	{
		double scale = 1.0 / Sqrt(SizeSquared);
		X *= scale;
		Y *= scale;
		Z *= scale;
		W *= scale;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion MakeFromEuler(Vector euler)
	{
		double halfPitch = DegreesToRadians(euler.X) * 0.5;
		double halfYaw = DegreesToRadians(euler.Y) * 0.5;
		double halfRoll = DegreesToRadians(euler.Z) * 0.5;

		var (sp, cp) = SinCos(halfPitch);
		var (sy, cy) = SinCos(halfYaw);
		var (sr, cr) = SinCos(halfRoll);

		return new(
			cr * sp * sy - sr * cp * cy,
			cr * sp * cy + sr * cp * sy,
			cr * cp * sy - sr * sp * cy,
			cr * cp * cy + sr * sp * sy
		);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion FindBetween(Vector a, Vector b)
	{
		double crossX = a.Y * b.Z - a.Z * b.Y;
		double crossY = a.Z * b.X - a.X * b.Z;
		double crossZ = a.X * b.Y - a.Y * b.X;
		double crossSize = Sqrt(crossX * crossX + crossY * crossY + crossZ * crossZ);

		double dot = a.Dot(b);

		if (crossSize < SMALL_NUMBER)
		{
			return Identity;
		}

		return new Quaternion(crossX, crossY, crossZ, dot + Sqrt(a.SizeSquared * b.SizeSquared)).Normalized;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion Slerp(Quaternion quatA, Quaternion quatB, double alpha)
	{
		// Calculate cosine of the angle between quaternions
		double cosOmega = quatA.X * quatB.X + quatA.Y * quatB.Y + quatA.Z * quatB.Z + quatA.W * quatB.W;

		// Use the shortest path
		double sign = cosOmega >= 0.0 ? 1.0 : -1.0;
		double scaledCos = cosOmega * sign;

		// Linear interpolation
		double t1, t2;

		if (1.0 - scaledCos > SMALL_NUMBER)
		{
			// Spherical interpolation
			double omega = Acos(scaledCos);
			double sinOmega = Sin(omega);
			t1 = Sin((1.0 - alpha) * omega) / sinOmega;
			t2 = Sin(alpha * omega) / sinOmega;
		}
		else
		{
			// Linear interpolation for very close quaternions
			t1 = 1.0 - alpha;
			t2 = alpha;
		}

		return new Quaternion(
			t1 * quatA.X + t2 * quatB.X * sign,
			t1 * quatA.Y + t2 * quatB.Y * sign,
			t1 * quatA.Z + t2 * quatB.Z * sign,
			t1 * quatA.W + t2 * quatB.W * sign
		).Normalized;
	}
}


