// Copyright Zero Games. All Rights Reserved.

using System.Runtime.InteropServices;
using ZeroGames.Extensions;
using ZeroGames.Extensions.Math;

A a = A.A | A.B | A.D;
A b = A.A | A.B;
Console.WriteLine(a.HasAllFlags(b));


[Flags]
public enum A : uint64
{
	A = 1,
	B = 2,
	C = 4,
	D = 8,
}