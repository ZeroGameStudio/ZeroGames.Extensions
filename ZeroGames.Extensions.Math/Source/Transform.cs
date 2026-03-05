// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ZeroGames.Extensions.Math;

[StructLayout(LayoutKind.Explicit, Size = 96, Pack = 16)]
public partial struct Transform
{
	
	[FieldOffset(0)]
	public Quaternion Rotation;

	[FieldOffset(32)]
	public Vector Translation;

	[FieldOffset(64)]
	public Vector Scale;

	public Transform() : this(Quaternion.Identity, Vector.Zero, Vector.One){}
	public Transform(Quaternion rotation, Vector translation, Vector scale) => (Rotation, Translation, Scale) = (rotation, translation, scale);
	public Transform(Rotator rotation, Vector translation, Vector scale) : this(rotation.Quaternion, translation, scale){}

	public static readonly Transform Identity = new();
	
}


