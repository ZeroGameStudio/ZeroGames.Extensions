// Copyright Zero Games. All Rights Reserved.

using System.Runtime.InteropServices;
using ZeroGames.Extensions.Math;

unsafe
{
	Console.WriteLine(sizeof(Transform));
	Console.WriteLine(Marshal.OffsetOf<Transform>(nameof(Transform.Rotation)));
	Console.WriteLine(Marshal.OffsetOf<Transform>(nameof(Transform.Translation)));
	Console.WriteLine(Marshal.OffsetOf<Transform>(nameof(Transform.Scale)));
}



