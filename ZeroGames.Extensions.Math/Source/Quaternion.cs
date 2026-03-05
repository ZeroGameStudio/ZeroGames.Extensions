// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ZeroGames.Extensions.Math;

[StructLayout(LayoutKind.Explicit, Size = 32, Pack = 8)]
public partial struct Quaternion : IEquatable<Quaternion>
{
	
	[FieldOffset(0)]
	public double X;

	[FieldOffset(8)]
	public double Y;

	[FieldOffset(16)]
	public double Z;

	[FieldOffset(24)]
	public double W;

	public Quaternion() : this(0, 0, 0, 1){}
	public Quaternion(double x, double y, double z, double w) => (X, Y, Z, W) = (x, y, z, w);

	public static readonly Quaternion Identity = new();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Quaternion other, double tolerance)
	{
		return Abs(X - other.X) <= tolerance &&
		       Abs(Y - other.Y) <= tolerance &&
		       Abs(Z - other.Z) <= tolerance &&
		       Abs(W - other.W) <= tolerance;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Quaternion other) => Equals(other, SMALL_NUMBER);

	public override bool Equals(object? obj)
	{
		return obj is Quaternion other && Equals(other);
	}

	public override int32 GetHashCode()
	{
		return HashCode.Combine(X, Y, Z, W);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Quaternion left, Quaternion right) => left.Equals(right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Quaternion left, Quaternion right) => !left.Equals(right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
	{
		return new(
			rhs.W * lhs.X + rhs.X * lhs.W + rhs.Y * lhs.Z - rhs.Z * lhs.Y,
			rhs.W * lhs.Y - rhs.X * lhs.Z + rhs.Y * lhs.W + rhs.Z * lhs.X,
			rhs.W * lhs.Z + rhs.X * lhs.Y - rhs.Y * lhs.X + rhs.Z * lhs.W,
			rhs.W * lhs.W - rhs.X * lhs.X - rhs.Y * lhs.Y - rhs.Z * lhs.Z
		);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion operator *(Quaternion lhs, double rhs) => new(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs, lhs.W * rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion operator *(double lhs, Quaternion rhs) => new(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z, lhs * rhs.W);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Quaternion operator /(Quaternion lhs, double rhs) => new(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs, lhs.W / rhs);
	
}


