// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Pooling;

public interface IPooled
{
	void PreGetFromPool();
	void PreReturnToPool();
}


