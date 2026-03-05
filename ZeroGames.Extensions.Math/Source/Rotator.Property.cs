// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

partial struct Rotator
{
	
	public Quaternion Quaternion
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			const double R = DEGREE_TO_RADIAN_HALF;
			
			var (halfPitchRad, halfYawRad, halfRollRad) = (R * FMod(Pitch, 360), R * FMod(Yaw, 360), R * FMod(Roll, 360));
			
			var (sp, cp) = (Sin(halfPitchRad), Cos(halfPitchRad));
			var (sy, cy) = (Sin(halfYawRad), Cos(halfYawRad));
			var (sr, cr) = (Sin(halfRollRad), Cos(halfRollRad));
			
			double x = + cr * sp * sy - sr * cp * cy;
			double y = - cr * sp * cy - sr * cp * sy;
			double z = + cr * cp * sy - sr * sp * cy;
			double w = + cr * cp * cy + sr * sp * sy;
			
			return new(x, y, z, w);
		}
	}
	
	public Vector Vector
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			const double R = DEGREE_TO_RADIAN;
			
			var (halfPitchRad, halfYawRad) = (R * FMod(Pitch, 360), R * FMod(Yaw, 360));
			var (cp, sp, cy, sy) = (Cos(halfPitchRad), Sin(halfPitchRad), Cos(halfYawRad), Sin(halfYawRad));

			return new(cp * cy, cp * sy, sp);
		}
	}
	
	public bool IsZero
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => ClampAxis(Pitch) == 0 && ClampAxis(Yaw) == 0 && ClampAxis(Roll) == 0;
	}

	public Rotator Inversed
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			double pitch = -Pitch;
			double yaw = -Yaw;
			double roll = -Roll;
			return new Rotator(pitch, yaw, roll).Normalized;
		}
	}

	public Rotator Normalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			Rotator rot = this;
			rot.Normalize();
			return rot;
		}
	}

	public Rotator Denormalized
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => new(ClampAxis(Pitch), ClampAxis(Yaw), ClampAxis(Roll));
	}

	public Rotator EquivalentRotator
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => new(180.0 - Pitch, Yaw + 180.0, Roll + 180.0);
	}

	public bool ContainsNaN
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get => !IsFinite(Pitch) || !IsFinite(Yaw) || !IsFinite(Roll);
	}
	
}


