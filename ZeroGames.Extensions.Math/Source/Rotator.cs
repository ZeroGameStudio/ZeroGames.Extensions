// Copyright Zero Games. All Rights Reserved.

using System.Runtime.InteropServices;

namespace ZeroGames.Extensions.Math;

[StructLayout(LayoutKind.Explicit, Size = 24, Pack = 8)]
public partial struct Rotator
{
	[FieldOffset(0)]
	public double Pitch;
	
	[FieldOffset(8)]
	public double Yaw;
	
	[FieldOffset(16)]
	public double Roll;

	public Rotator(double scalar) => (Pitch, Yaw, Roll) = (scalar, scalar, scalar);
	public Rotator(double pitch, double yaw, double roll) => (Pitch, Yaw, Roll) = (pitch, yaw, roll);

	public static readonly Rotator ZeroRotator = new();
}


