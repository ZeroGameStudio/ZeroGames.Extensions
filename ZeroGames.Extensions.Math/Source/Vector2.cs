// Copyright ZeroGames. All Rights Reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ZeroGames.Extensions.Math;

[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
public partial struct Vector2 : IEquatable<Vector2>
{
	
	[FieldOffset(0)]
	public double X;

	[FieldOffset(8)]
	public double Y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2() { }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2(double scalar) => (X, Y) = (scalar, scalar);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2(double x, double y) => (X, Y) = (x, y);

	public static readonly Vector2 Zero = new();
	public static readonly Vector2 One = new(1);

	public static readonly Vector2 UnitX = new(1, 0);
	public static readonly Vector2 UnitY = new(0, 1);

	public static readonly Vector2 Unit45Deg = new(1 / Sqrt(2), 1 / Sqrt(2));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector2 left, Vector2 right) => left.Equals(right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector2 left, Vector2 right) => !left.Equals(right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator +(Vector2 @this) => @this;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator -(Vector2 @this) => new(-@this.X, -@this.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator +(Vector2 lhs, Vector2 rhs) => new(lhs.X + rhs.X, lhs.Y + rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator +(Vector2 lhs, double rhs) => new(lhs.X + rhs, lhs.Y + rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator +(double lhs, Vector2 rhs) => new(lhs + rhs.X, lhs + rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator -(Vector2 lhs, Vector2 rhs) => new(lhs.X - rhs.X, lhs.Y - rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator -(Vector2 lhs, double rhs) => new(lhs.X - rhs, lhs.Y - rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator -(double lhs, Vector2 rhs) => new(lhs - rhs.X, lhs - rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator *(Vector2 lhs, Vector2 rhs) => new(lhs.X * rhs.X, lhs.Y * rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator *(Vector2 lhs, double rhs) => new(lhs.X * rhs, lhs.Y * rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator *(double lhs, Vector2 rhs) => new(lhs * rhs.X, lhs * rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator /(Vector2 lhs, Vector2 rhs) => new(lhs.X / rhs.X, lhs.Y / rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator /(Vector2 lhs, double rhs) => new(lhs.X / rhs, lhs.Y / rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 operator /(double lhs, Vector2 rhs) => new(lhs / rhs.X, lhs / rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double operator |(Vector2 lhs, Vector2 rhs) => lhs.Dot(rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double operator ^(Vector2 lhs, Vector2 rhs) => lhs.Cross(rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Vector2 other, double tolerance)
	{
		return Abs(X - other.X) <= tolerance && Abs(Y - other.Y) <= tolerance;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Vector2 other) => Equals(other, SMALL_NUMBER);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals(object? obj) => obj is Vector2 other && Equals(other);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int32 GetHashCode() => HashCode.Combine(X, Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Dot(Vector2 other) => X * other.X + Y * other.Y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Cross(Vector2 other) => X * other.Y - Y * other.X;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Max(Vector2 a, Vector2 b) => new(System.Math.Max(a.X, b.X), System.Math.Max(a.Y, b.Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Min(Vector2 a, Vector2 b) => new(System.Math.Min(a.X, b.X), System.Math.Min(a.Y, b.Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max) => new(System.Math.Clamp(v.X, min.X, max.X), System.Math.Clamp(v.Y, min.Y, max.Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double DotProduct(Vector2 a, Vector2 b) => a | b;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double CrossProduct(Vector2 a, Vector2 b) => a ^ b;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double Dist(Vector2 a, Vector2 b) => Sqrt(DistSquared(a, b));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double DistSquared(Vector2 a, Vector2 b) => Square(a.X - b.X) + Square(a.Y - b.Y);
	
}
