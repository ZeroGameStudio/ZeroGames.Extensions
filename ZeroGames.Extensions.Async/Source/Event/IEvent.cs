// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public interface IEventEntry : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent : IEventEntry, IExceptionHandlerReceiver
{
	void Invoke();
}

public interface IEventEntry<out T> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T> : IEventEntry<T>, IExceptionHandlerReceiver
{
	void Invoke(T arg);
}

public interface IEventEntry<out T1, out T2> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2> : IEventEntry<T1, T2>, IExceptionHandlerReceiver
{
	void Invoke(T1 arg1, T2 arg2);
}

public interface IEventEntry<out T1, out T2, out T3> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3> : IEventEntry<T1, T2, T3>, IExceptionHandlerReceiver
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3);
}

public interface IEventEntry<out T1, out T2, out T3, out T4> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3, T4> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3, T4> : IEventEntry<T1, T2, T3, T4>, IExceptionHandlerReceiver
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
}

public interface IEventEntry<out T1, out T2, out T3, out T4, out T5> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3, T4, T5> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3, T4, T5> : IEventEntry<T1, T2, T3, T4, T5>, IExceptionHandlerReceiver
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
}

public interface IEventEntry<out T1, out T2, out T3, out T4, out T5, out T6> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3, T4, T5, T6> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3, T4, T5, T6> : IEventEntry<T1, T2, T3, T4, T5, T6>, IExceptionHandlerReceiver
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6);
}

public interface IEventEntry<out T1, out T2, out T3, out T4, out T5, out T6, out T7> : IRemoveEventRegistrationSource
{
	EventRegistration Add(Action<T1, T2, T3, T4, T5, T6, T7> handler, Lifetime lifetime = default);
	int32 RemoveAll(object? target);
}

public interface IEvent<T1, T2, T3, T4, T5, T6, T7> : IEventEntry<T1, T2, T3, T4, T5, T6, T7>, IExceptionHandlerReceiver
{
	void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7);
}



