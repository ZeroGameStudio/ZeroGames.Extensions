// Copyright ZeroGames. All Rights Reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ZeroGames.Extensions.Math;

[StructLayout(LayoutKind.Explicit, Size = 16, Pack = 8)]
public partial struct Vector2D : IEquatable<Vector2D>
{
	
	[FieldOffset(0)]
	public double X;

	[FieldOffset(8)]
	public double Y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D() { }

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D(double scalar) => (X, Y) = (scalar, scalar);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Vector2D(double x, double y) => (X, Y) = (x, y);

	public static readonly Vector2D Zero = new();
	public static readonly Vector2D One = new(1);

	public static readonly Vector2D UnitX = new(1, 0);
	public static readonly Vector2D UnitY = new(0, 1);

	public static readonly Vector2D Unit45Deg = new(1 / Sqrt(2), 1 / Sqrt(2));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator ==(Vector2D left, Vector2D right) => left.Equals(right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool operator !=(Vector2D left, Vector2D right) => !left.Equals(right);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator +(Vector2D @this) => @this;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator -(Vector2D @this) => new(-@this.X, -@this.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator +(Vector2D lhs, Vector2D rhs) => new(lhs.X + rhs.X, lhs.Y + rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator +(Vector2D lhs, double rhs) => new(lhs.X + rhs, lhs.Y + rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator +(double lhs, Vector2D rhs) => new(lhs + rhs.X, lhs + rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator -(Vector2D lhs, Vector2D rhs) => new(lhs.X - rhs.X, lhs.Y - rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator -(Vector2D lhs, double rhs) => new(lhs.X - rhs, lhs.Y - rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator -(double lhs, Vector2D rhs) => new(lhs - rhs.X, lhs - rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator *(Vector2D lhs, Vector2D rhs) => new(lhs.X * rhs.X, lhs.Y * rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator *(Vector2D lhs, double rhs) => new(lhs.X * rhs, lhs.Y * rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator *(double lhs, Vector2D rhs) => new(lhs * rhs.X, lhs * rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator /(Vector2D lhs, Vector2D rhs) => new(lhs.X / rhs.X, lhs.Y / rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator /(Vector2D lhs, double rhs) => new(lhs.X / rhs, lhs.Y / rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D operator /(double lhs, Vector2D rhs) => new(lhs / rhs.X, lhs / rhs.Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double operator |(Vector2D lhs, Vector2D rhs) => lhs.Dot(rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double operator ^(Vector2D lhs, Vector2D rhs) => lhs.Cross(rhs);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void Deconstruct(out double x, out double y) => (x, y) = (X, Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Vector2D other, double tolerance)
	{
		return Abs(X - other.X) <= tolerance && Abs(Y - other.Y) <= tolerance;
	}
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Vector2D other) => Equals(other, SMALL_NUMBER);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override bool Equals(object? obj) => obj is Vector2D other && Equals(other);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public override int32 GetHashCode() => HashCode.Combine(X, Y);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Dot(Vector2D other) => X * other.X + Y * other.Y;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double Cross(Vector2D other) => X * other.Y - Y * other.X;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D Max(Vector2D a, Vector2D b) => new(System.Math.Max(a.X, b.X), System.Math.Max(a.Y, b.Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D Min(Vector2D a, Vector2D b) => new(System.Math.Min(a.X, b.X), System.Math.Min(a.Y, b.Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Vector2D Clamp(Vector2D v, Vector2D min, Vector2D max) => new(System.Math.Clamp(v.X, min.X, max.X), System.Math.Clamp(v.Y, min.Y, max.Y));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double DotProduct(Vector2D a, Vector2D b) => a | b;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double CrossProduct(Vector2D a, Vector2D b) => a ^ b;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double Dist(Vector2D a, Vector2D b) => Sqrt(DistSquared(a, b));

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double DistSquared(Vector2D a, Vector2D b) => Square(a.X - b.X) + Square(a.Y - b.Y);
	
}
