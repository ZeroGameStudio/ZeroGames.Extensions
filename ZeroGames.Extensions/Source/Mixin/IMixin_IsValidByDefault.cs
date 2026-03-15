// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions;

public interface IMixin_IsValidByDefault<TSelf> where TSelf: IMixin_IsValidByDefault<TSelf>, IEquatable<TSelf>;
public static class IsValidByDefaultMixin
{
	extension<T>(T @this) where T : struct, IMixin_IsValidByDefault<T>, IEquatable<T>
	{
		public bool IsValid => !@this.Equals(default);
	}
}


