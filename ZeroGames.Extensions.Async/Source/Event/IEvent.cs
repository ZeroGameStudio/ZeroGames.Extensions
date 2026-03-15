// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public interface IEventEntry : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent : IEventEntry
{
	void Invoke();

	Func<Exception, bool>? ExceptionHandler { set; }
}

public interface IEventEntry<out T> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T> : IEventEntry<T>
{
	void Invoke(T arg);

	Func<Exception, bool>? ExceptionHandler { set; }
}

public interface IEventEntry<out T1, out T2> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2> : IEventEntry<T1, T2>
{
	void Invoke(T1 arg1, T2 arg2);

	Func<Exception, bool>? ExceptionHandler { set; }
}

public interface IEventEntry<out T1, out T2, out T3> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3> : IEventEntry<T1, T2, T3>
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3);

	Func<Exception, bool>? ExceptionHandler { set; }
}

public interface IEventEntry<out T1, out T2, out T3, out T4> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3, T4> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3, T4> : IEventEntry<T1, T2, T3, T4>
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

	Func<Exception, bool>? ExceptionHandler { set; }
}

public interface IEventEntry<out T1, out T2, out T3, out T4, out T5> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3, T4, T5> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3, T4, T5> : IEventEntry<T1, T2, T3, T4, T5>
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

	Func<Exception, bool>? ExceptionHandler { set; }
}

public interface IEventEntry<out T1, out T2, out T3, out T4, out T5, out T6> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3, T4, T5, T6> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3, T4, T5, T6> : IEventEntry<T1, T2, T3, T4, T5, T6>
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);

	Func<Exception, bool>? ExceptionHandler { set; }
}

public interface IEventEntry<out T1, out T2, out T3, out T4, out T5, out T6, out T7> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3, T4, T5, T6, T7> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3, T4, T5, T6, T7> : IEventEntry<T1, T2, T3, T4, T5, T6, T7>
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);

	Func<Exception, bool>? ExceptionHandler { set; }
}



