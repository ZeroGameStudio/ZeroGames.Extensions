// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Pooling;

public interface IConfigProvider<T> where T : class, new()
{
	void GetConfig(out ObjectPoolConfig config);
}


