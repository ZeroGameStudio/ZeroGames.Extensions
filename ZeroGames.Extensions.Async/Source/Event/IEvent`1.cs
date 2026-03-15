// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public interface IEventEntry<out T> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T> : IEventEntry<T>
{
	void Invoke(T args);
    
	Func<Exception, bool>? ExceptionHandler { set; }
}


