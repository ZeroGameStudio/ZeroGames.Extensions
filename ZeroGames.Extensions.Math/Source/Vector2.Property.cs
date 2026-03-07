// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

partial struct Vector2
{

	public double Size
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Sqrt(SizeSquared);
	}

	public double SizeSquared
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => X * X + Y * Y;
	}

	public bool IsNearlyZero
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Abs(X) <= KINDA_SMALL_NUMBER && Abs(Y) <= KINDA_SMALL_NUMBER;
	}

	public bool IsZero
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => X == 0 && Y == 0;
	}

	public bool IsNormalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => Abs(1 - SizeSquared) < THRESH_VECTOR_NORMALIZED;
	}

	public Vector2 Normalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => SizeSquared >= THRESH_VECTOR_NORMALIZED ? this / Size : Zero;
	}
	
	public Vector2 Sign
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => new(Sign(X), Sign(Y));
	}

	public bool ContainsNaN
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !IsFinite(X) || !IsFinite(Y);
	}

}
