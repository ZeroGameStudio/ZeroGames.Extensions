// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions.Math;

partial struct Rotator
{
	
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void Normalize()
	{
		Pitch = NormalizeAxis(Pitch);
		Yaw = NormalizeAxis(Yaw);
		Roll = NormalizeAxis(Roll);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double ClampAxis(double angle)
	{
		angle = FMod(angle, 360);
		if (angle < 0)
		{
			angle += 360;
		}

		return angle;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static double NormalizeAxis(double angle)
	{
		angle = ClampAxis(angle);
		if (angle > 180)
		{
			angle -= 360;
		}

		return angle;
	}
	
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool IsNearlyZero(double tolerance = KINDA_SMALL_NUMBER)
	{
		return Abs(NormalizeAxis(Pitch)) <= tolerance &&
		       Abs(NormalizeAxis(Yaw)) <= tolerance &&
		       Abs(NormalizeAxis(Roll)) <= tolerance;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool Equals(Rotator other, double tolerance = KINDA_SMALL_NUMBER)
	{
		return Abs(NormalizeAxis(Pitch - other.Pitch)) <= tolerance &&
		       Abs(NormalizeAxis(Yaw - other.Yaw)) <= tolerance &&
		       Abs(NormalizeAxis(Roll - other.Roll)) <= tolerance;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool EqualsOrientation(Rotator other, double tolerance = KINDA_SMALL_NUMBER)
	{
		return Quaternion.AngularDistance(other.Quaternion) <= tolerance;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Rotator Add(double deltaPitch, double deltaYaw, double deltaRoll)
	{
		return new(Pitch + deltaPitch, Yaw + deltaYaw, Roll + deltaRoll);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public Rotator GridSnap(Rotator rotGrid)
	{
		return new(
			System.Math.GridSnap(Pitch, rotGrid.Pitch),
			System.Math.GridSnap(Yaw, rotGrid.Yaw),
			System.Math.GridSnap(Roll, rotGrid.Roll)
		);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void GetWindingAndRemainder(out Rotator winding, out Rotator remainder)
	{
		winding = new(
			Truncate(Pitch / 360) * 360,
			Truncate(Yaw / 360) * 360,
			Truncate(Roll / 360) * 360
		);
		remainder = new(
			Pitch - winding.Pitch,
			Yaw - winding.Yaw,
			Roll - winding.Roll
		);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public double GetManhattanDistance(Rotator other)
	{
		return Abs(Yaw - other.Yaw) + Abs(Pitch - other.Pitch) + Abs(Roll - other.Roll);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void SetClosestToMe(ref Rotator makeClosest)
	{
		Rotator otherChoice = makeClosest.EquivalentRotator;
		double firstDiff = GetManhattanDistance(makeClosest);
		double secondDiff = GetManhattanDistance(otherChoice);
		if (secondDiff < firstDiff)
		{
			makeClosest = otherChoice;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Rotator MakeFromEuler(Vector euler) => new(euler.X, euler.Y, euler.Z);
	
}


