// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ZeroGames.Extensions.Math;

[StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
public partial struct Vector : IEquatable<Vector>
{
	[FieldOffset(0)]
	public double X;
	
	[FieldOffset(8)]
	public double Y;
	
	[FieldOffset(16)]
	public double Z;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector(double scalar) => (X, Y, Z) = (scalar, scalar, scalar);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector(double x, double y, double z) => (X, Y, Z) = (x, y, z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector MakeForward(double scalar = 1) => new(scalar, 0, 0);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector MakeBackward(double scalar = 1) => new(-scalar, 0, 0);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector MakeRight(double scalar = 1) => new(0, scalar, 0);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector MakeLeft(double scalar = 1) => new(0, -scalar, 0);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector MakeUp(double scalar = 1) => new(0, 0, scalar);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector MakeDown(double scalar = 1) => new(0, 0, -scalar);
	
	public static readonly Vector Zero = new();
	public static readonly Vector One = new(1);
	
	public static readonly Vector UnitX = MakeForward();
	public static readonly Vector UnitY = MakeRight();
	public static readonly Vector UnitZ = MakeUp();

	public static readonly Vector XAxis = MakeForward();
	public static readonly Vector YAxis = MakeRight();
	public static readonly Vector ZAxis = MakeUp();

	public static readonly Vector Forward = MakeForward();
	public static readonly Vector Backward = MakeBackward();
	public static readonly Vector Right = MakeRight();
	public static readonly Vector Left = MakeLeft();
	public static readonly Vector Up = MakeUp();
	public static readonly Vector Down = MakeDown();
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector left, Vector right) => left.Equals(right);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector left, Vector right) => !left.Equals(right);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator +(Vector @this) => @this;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator -(Vector @this) => new(-@this.X, -@this.Y, -@this.Z);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator +(Vector lhs, Vector rhs) => new(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator +(Vector lhs, float rhs) => new(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator +(Vector lhs, double rhs) => new(lhs.X + rhs, lhs.Y + rhs, lhs.Z + rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator +(float lhs, Vector rhs) => new(lhs + rhs.X, lhs + rhs.Y, lhs + rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator +(double lhs, Vector rhs) => new(lhs + rhs.X, lhs + rhs.Y, lhs + rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator -(Vector lhs, Vector rhs) => new(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator -(Vector lhs, float rhs) => new(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator -(Vector lhs, double rhs) => new(lhs.X - rhs, lhs.Y - rhs, lhs.Z - rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator -(float lhs, Vector rhs) => new(lhs - rhs.X, lhs - rhs.Y, lhs - rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator -(double lhs, Vector rhs) => new(lhs - rhs.X, lhs - rhs.Y, lhs - rhs.Z);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator *(Vector lhs, Vector rhs) => new(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator *(Vector lhs, float rhs) => new(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator *(Vector lhs, double rhs) => new(lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator *(float lhs, Vector rhs) => new(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator *(double lhs, Vector rhs) => new(lhs * rhs.X, lhs * rhs.Y, lhs * rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator /(Vector lhs, Vector rhs) => new(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator /(Vector lhs, float rhs) => new(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator /(Vector lhs, double rhs) => new(lhs.X / rhs, lhs.Y / rhs, lhs.Z / rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator /(float lhs, Vector rhs) => new(lhs / rhs.X, lhs / rhs.Y, lhs / rhs.Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator /(double lhs, Vector rhs) => new(lhs / rhs.X, lhs / rhs.Y, lhs / rhs.Z);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double operator |(Vector lhs, Vector rhs) => lhs.Dot(rhs);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector operator ^(Vector lhs, Vector rhs) => lhs.Cross(rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Deconstruct(out double x, out double y, out double z) => (x, y, z) = (X, Y, Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Vector other, double tolerance)
	{
		return Abs(X - other.X) <= tolerance &&
		       Abs(Y - other.Y) <= tolerance &&
		       Abs(Z - other.Z) <= tolerance;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Vector other) => Equals(other, SMALL_NUMBER);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals(object? obj) => obj is Vector other && Equals(other);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int32 GetHashCode() => HashCode.Combine(X, Y, Z);
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Dot(Vector other) => X * other.X + Y * other.Y + Z * other.Z;
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector Cross(Vector other) => new(Y * other.Z - Z * other.Y, Z * other.X - X * other.Z, X * other.Y - Y * other.X);
}


