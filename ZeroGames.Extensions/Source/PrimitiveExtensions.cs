// Copyright Zero Games. All Rights Reserved.

using System.Runtime.CompilerServices;

namespace ZeroGames.Extensions;

public static class PrimitiveExtensions
{
    extension(float @this)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearlyZero(float tolerance = 1e-7f) => Math.Abs(@this) < tolerance;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearlyEqual(float other, float tolerance = 1e-7f) => (@this - other).IsNearlyZero(tolerance);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearlyEqual(double other, double tolerance = 1e-14f) => (@this - other).IsNearlyZero(tolerance);
    }

    extension(double @this)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearlyZero(double tolerance = 1e-14) => Math.Abs(@this) < tolerance;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearlyEqual(float other, double tolerance = 1e-14) => (@this - other).IsNearlyZero(tolerance);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearlyEqual(double other, double tolerance = 1e-14) => (@this - other).IsNearlyZero(tolerance);
    }
}


