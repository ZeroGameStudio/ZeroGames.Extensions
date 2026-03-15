// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public class NeverEvent : IEvent
{
	public static NeverEvent Instance { get; } = new();

	private NeverEvent(){}

	#region IEvent Implementations

	public EventRegistration Add(Action handler, Lifetime lifetime = default) => default;
	public bool Remove(EventRegistration registration) => false;
	public int32 RemoveAll(object? target) => 0;
	public void Invoke(){}
	public Func<Exception, bool>? ExceptionHandler { set{} }

	#endregion
}

public class NeverEvent<T> : IEvent<T>
{
	public static NeverEvent<T> Instance { get; } = new();

	private NeverEvent(){}

	#region IEvent<T> Implementations

	public EventRegistration Add(Action<T> handler, Lifetime lifetime = default) => default;
	public bool Remove(EventRegistration registration) => false;
	public int32 RemoveAll(object? target) => 0;
	public void Invoke(T arg){}
	public Func<Exception, bool>? ExceptionHandler { set{} }

	#endregion
}

public class NeverEvent<T1, T2> : IEvent<T1, T2>
{
	public static NeverEvent<T1, T2> Instance { get; } = new();

	private NeverEvent(){}

	#region IEvent<T1, T2> Implementations

	public EventRegistration Add(Action<T1, T2> handler, Lifetime lifetime = default) => default;
	public bool Remove(EventRegistration registration) => false;
	public int32 RemoveAll(object? target) => 0;
	public void Invoke(T1 arg1, T2 arg2){}
	public Func<Exception, bool>? ExceptionHandler { set{} }

	#endregion
}

public class NeverEvent<T1, T2, T3> : IEvent<T1, T2, T3>
{
	public static NeverEvent<T1, T2, T3> Instance { get; } = new();

	private NeverEvent(){}

	#region IEvent<T1, T2, T3> Implementations

	public EventRegistration Add(Action<T1, T2, T3> handler, Lifetime lifetime = default) => default;
	public bool Remove(EventRegistration registration) => false;
	public int32 RemoveAll(object? target) => 0;
	public void Invoke(T1 arg1, T2 arg2, T3 arg3){}
	public Func<Exception, bool>? ExceptionHandler { set{} }

	#endregion
}

public class NeverEvent<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>
{
	public static NeverEvent<T1, T2, T3, T4> Instance { get; } = new();

	private NeverEvent(){}

	#region IEvent<T1, T2, T3, T4> Implementations

	public EventRegistration Add(Action<T1, T2, T3, T4> handler, Lifetime lifetime = default) => default;
	public bool Remove(EventRegistration registration) => false;
	public int32 RemoveAll(object? target) => 0;
	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4){}
	public Func<Exception, bool>? ExceptionHandler { set{} }

	#endregion
}

public class NeverEvent<T1, T2, T3, T4, T5> : IEvent<T1, T2, T3, T4, T5>
{
	public static NeverEvent<T1, T2, T3, T4, T5> Instance { get; } = new();

	private NeverEvent(){}

	#region IEvent<T1, T2, T3, T4, T5> Implementations

	public EventRegistration Add(Action<T1, T2, T3, T4, T5> handler, Lifetime lifetime = default) => default;
	public bool Remove(EventRegistration registration) => false;
	public int32 RemoveAll(object? target) => 0;
	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5){}
	public Func<Exception, bool>? ExceptionHandler { set{} }

	#endregion
}

public class NeverEvent<T1, T2, T3, T4, T5, T6> : IEvent<T1, T2, T3, T4, T5, T6>
{
	public static NeverEvent<T1, T2, T3, T4, T5, T6> Instance { get; } = new();

	private NeverEvent(){}

	#region IEvent<T1, T2, T3, T4, T5, T6> Implementations

	public EventRegistration Add(Action<T1, T2, T3, T4, T5, T6> handler, Lifetime lifetime = default) => default;
	public bool Remove(EventRegistration registration) => false;
	public int32 RemoveAll(object? target) => 0;
	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6){}
	public Func<Exception, bool>? ExceptionHandler { set{} }

	#endregion
}

public class NeverEvent<T1, T2, T3, T4, T5, T6, T7> : IEvent<T1, T2, T3, T4, T5, T6, T7>
{
	public static NeverEvent<T1, T2, T3, T4, T5, T6, T7> Instance { get; } = new();

	private NeverEvent(){}

	#region IEvent<T1, T2, T3, T4, T5, T6, T7> Implementations

	public EventRegistration Add(Action<T1, T2, T3, T4, T5, T6, T7> handler, Lifetime lifetime = default) => default;
	public bool Remove(EventRegistration registration) => false;
	public int32 RemoveAll(object? target) => 0;
	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7){}
	public Func<Exception, bool>? ExceptionHandler { set{} }

	#endregion
}



