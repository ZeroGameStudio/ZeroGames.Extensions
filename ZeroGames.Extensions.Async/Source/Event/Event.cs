// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public class Event : IEvent
{

	private readonly record struct Rec(EventRegistration Reg, Action Handler, Lifetime Lifetime);

	private readonly List<Rec?> _invocationList = [];
	private int32 _invocationListLock;
	private uint64 _handle;

	#region IEvent Implementations

	public EventRegistration Add(Action handler, Lifetime lifetime = default)
	{
		if (lifetime.IsExpired)
		{
			return default;
		}

		EventRegistration reg = new(this, ++_handle);
		_invocationList.Add(new(reg, handler, lifetime));
		return reg;
	}

	public bool Remove(EventRegistration registration)
	{
		if (registration == default)
		{
			return false;
		}

		if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
		{
			if (_invocationListLock > 0)
			{
				_invocationList[i] = null;
			}
			else
			{
				_invocationList.RemoveAt(i);
			}

			return true;
		}

		return false;
	}

	public int32 RemoveAll(object? target)
	{
		if (target is null)
		{
			return 0;
		}

		int32 count = 0;
		for (int32 i = _invocationList.Count - 1; i >= 0; --i)
		{
			Rec? rec = _invocationList[i];
			if (rec is not null && rec.Value.Handler.Target == target)
			{
				if (_invocationListLock > 0)
				{
					_invocationList[i] = null;
				}
				else
				{
					_invocationList.RemoveAt(i);
				}

				++count;
			}
		}

		return count;
	}

	public void Invoke()
	{
		bool needsCompaction = false;

		try
		{
			++_invocationListLock;

			int32 count = _invocationList.Count;
			for (int32 i = 0; i < count; ++i)
			{
				if (_invocationList[i] is not { } rec)
				{
					needsCompaction = true;
					continue;
				}

				if (rec.Lifetime.IsExpired)
				{
					_invocationList[i] = null;
					needsCompaction = true;
					continue;
				}

				try
				{
					rec.Handler();
				}
				catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
				{
					ExceptionGuard.PublishUnhandledException(ex);
				}
			}
		}
		finally
		{
			--_invocationListLock;
		}

		if (needsCompaction && _invocationListLock is 0)
		{
			_invocationList.RemoveAll(static rec => rec is null);
		}
	}

	public Func<Exception, bool>? ExceptionHandler { get; set; }

	#endregion

}

public class Event<T> : IEvent<T>
{

	private readonly record struct Rec(EventRegistration Reg, Action<T> Handler, Lifetime Lifetime);

	private readonly List<Rec?> _invocationList = [];
	private int32 _invocationListLock;
	private uint64 _handle;

	#region IEvent<T> Implementations

	public EventRegistration Add(Action<T> handler, Lifetime lifetime = default)
	{
		if (lifetime.IsExpired)
		{
			return default;
		}

		EventRegistration reg = new(this, ++_handle);
		_invocationList.Add(new(reg, handler, lifetime));
		return reg;
	}

	public bool Remove(EventRegistration registration)
	{
		if (registration == default)
		{
			return false;
		}

		if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
		{
			if (_invocationListLock > 0)
			{
				_invocationList[i] = null;
			}
			else
			{
				_invocationList.RemoveAt(i);
			}

			return true;
		}

		return false;
	}

	public int32 RemoveAll(object? target)
	{
		if (target is null)
		{
			return 0;
		}

		int32 count = 0;
		for (int32 i = _invocationList.Count - 1; i >= 0; --i)
		{
			Rec? rec = _invocationList[i];
			if (rec is not null && rec.Value.Handler.Target == target)
			{
				if (_invocationListLock > 0)
				{
					_invocationList[i] = null;
				}
				else
				{
					_invocationList.RemoveAt(i);
				}

				++count;
			}
		}

		return count;
	}

	public void Invoke(T arg)
	{
		bool needsCompaction = false;

		try
		{
			++_invocationListLock;

			int32 count = _invocationList.Count;
			for (int32 i = 0; i < count; ++i)
			{
				if (_invocationList[i] is not { } rec)
				{
					needsCompaction = true;
					continue;
				}

				if (rec.Lifetime.IsExpired)
				{
					_invocationList[i] = null;
					needsCompaction = true;
					continue;
				}

				try
				{
					rec.Handler(arg);
				}
				catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
				{
					ExceptionGuard.PublishUnhandledException(ex);
				}
			}
		}
		finally
		{
			--_invocationListLock;
		}

		if (needsCompaction && _invocationListLock is 0)
		{
			_invocationList.RemoveAll(static rec => rec is null);
		}
	}

	public Func<Exception, bool>? ExceptionHandler { get; set; }

	#endregion

}

public class Event<T1, T2> : IEvent<T1, T2>
{

	private readonly record struct Rec(EventRegistration Reg, Action<T1, T2> Handler, Lifetime Lifetime);

	private readonly List<Rec?> _invocationList = [];
	private int32 _invocationListLock;
	private uint64 _handle;

	#region IEvent<T1, T2> Implementations

	public EventRegistration Add(Action<T1, T2> handler, Lifetime lifetime = default)
	{
		if (lifetime.IsExpired)
		{
			return default;
		}

		EventRegistration reg = new(this, ++_handle);
		_invocationList.Add(new(reg, handler, lifetime));
		return reg;
	}

	public bool Remove(EventRegistration registration)
	{
		if (registration == default)
		{
			return false;
		}

		if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
		{
			if (_invocationListLock > 0)
			{
				_invocationList[i] = null;
			}
			else
			{
				_invocationList.RemoveAt(i);
			}

			return true;
		}

		return false;
	}

	public int32 RemoveAll(object? target)
	{
		if (target is null)
		{
			return 0;
		}

		int32 count = 0;
		for (int32 i = _invocationList.Count - 1; i >= 0; --i)
		{
			Rec? rec = _invocationList[i];
			if (rec is not null && rec.Value.Handler.Target == target)
			{
				if (_invocationListLock > 0)
				{
					_invocationList[i] = null;
				}
				else
				{
					_invocationList.RemoveAt(i);
				}

				++count;
			}
		}

		return count;
	}

	public void Invoke(T1 arg1, T2 arg2)
	{
		bool needsCompaction = false;

		try
		{
			++_invocationListLock;

			int32 count = _invocationList.Count;
			for (int32 i = 0; i < count; ++i)
			{
				if (_invocationList[i] is not { } rec)
				{
					needsCompaction = true;
					continue;
				}

				if (rec.Lifetime.IsExpired)
				{
					_invocationList[i] = null;
					needsCompaction = true;
					continue;
				}

				try
				{
					rec.Handler(arg1, arg2);
				}
				catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
				{
					ExceptionGuard.PublishUnhandledException(ex);
				}
			}
		}
		finally
		{
			--_invocationListLock;
		}

		if (needsCompaction && _invocationListLock is 0)
		{
			_invocationList.RemoveAll(static rec => rec is null);
		}
	}

	public Func<Exception, bool>? ExceptionHandler { get; set; }

	#endregion

}

public class Event<T1, T2, T3> : IEvent<T1, T2, T3>
{

	private readonly record struct Rec(EventRegistration Reg, Action<T1, T2, T3> Handler, Lifetime Lifetime);

	private readonly List<Rec?> _invocationList = [];
	private int32 _invocationListLock;
	private uint64 _handle;

	#region IEvent<T1, T2, T3> Implementations

	public EventRegistration Add(Action<T1, T2, T3> handler, Lifetime lifetime = default)
	{
		if (lifetime.IsExpired)
		{
			return default;
		}

		EventRegistration reg = new(this, ++_handle);
		_invocationList.Add(new(reg, handler, lifetime));
		return reg;
	}

	public bool Remove(EventRegistration registration)
	{
		if (registration == default)
		{
			return false;
		}

		if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
		{
			if (_invocationListLock > 0)
			{
				_invocationList[i] = null;
			}
			else
			{
				_invocationList.RemoveAt(i);
			}

			return true;
		}

		return false;
	}

	public int32 RemoveAll(object? target)
	{
		if (target is null)
		{
			return 0;
		}

		int32 count = 0;
		for (int32 i = _invocationList.Count - 1; i >= 0; --i)
		{
			Rec? rec = _invocationList[i];
			if (rec is not null && rec.Value.Handler.Target == target)
			{
				if (_invocationListLock > 0)
				{
					_invocationList[i] = null;
				}
				else
				{
					_invocationList.RemoveAt(i);
				}

				++count;
			}
		}

		return count;
	}

	public void Invoke(T1 arg1, T2 arg2, T3 arg3)
	{
		bool needsCompaction = false;

		try
		{
			++_invocationListLock;

			int32 count = _invocationList.Count;
			for (int32 i = 0; i < count; ++i)
			{
				if (_invocationList[i] is not { } rec)
				{
					needsCompaction = true;
					continue;
				}

				if (rec.Lifetime.IsExpired)
				{
					_invocationList[i] = null;
					needsCompaction = true;
					continue;
				}

				try
				{
					rec.Handler(arg1, arg2, arg3);
				}
				catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
				{
					ExceptionGuard.PublishUnhandledException(ex);
				}
			}
		}
		finally
		{
			--_invocationListLock;
		}

		if (needsCompaction && _invocationListLock is 0)
		{
			_invocationList.RemoveAll(static rec => rec is null);
		}
	}

	public Func<Exception, bool>? ExceptionHandler { get; set; }

	#endregion

}

public class Event<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>
{

	private readonly record struct Rec(EventRegistration Reg, Action<T1, T2, T3, T4> Handler, Lifetime Lifetime);

	private readonly List<Rec?> _invocationList = [];
	private int32 _invocationListLock;
	private uint64 _handle;

	#region IEvent<T1, T2, T3, T4> Implementations

	public EventRegistration Add(Action<T1, T2, T3, T4> handler, Lifetime lifetime = default)
	{
		if (lifetime.IsExpired)
		{
			return default;
		}

		EventRegistration reg = new(this, ++_handle);
		_invocationList.Add(new(reg, handler, lifetime));
		return reg;
	}

	public bool Remove(EventRegistration registration)
	{
		if (registration == default)
		{
			return false;
		}

		if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
		{
			if (_invocationListLock > 0)
			{
				_invocationList[i] = null;
			}
			else
			{
				_invocationList.RemoveAt(i);
			}

			return true;
		}

		return false;
	}

	public int32 RemoveAll(object? target)
	{
		if (target is null)
		{
			return 0;
		}

		int32 count = 0;
		for (int32 i = _invocationList.Count - 1; i >= 0; --i)
		{
			Rec? rec = _invocationList[i];
			if (rec is not null && rec.Value.Handler.Target == target)
			{
				if (_invocationListLock > 0)
				{
					_invocationList[i] = null;
				}
				else
				{
					_invocationList.RemoveAt(i);
				}

				++count;
			}
		}

		return count;
	}

	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		bool needsCompaction = false;

		try
		{
			++_invocationListLock;

			int32 count = _invocationList.Count;
			for (int32 i = 0; i < count; ++i)
			{
				if (_invocationList[i] is not { } rec)
				{
					needsCompaction = true;
					continue;
				}

				if (rec.Lifetime.IsExpired)
				{
					_invocationList[i] = null;
					needsCompaction = true;
					continue;
				}

				try
				{
					rec.Handler(arg1, arg2, arg3, arg4);
				}
				catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
				{
					ExceptionGuard.PublishUnhandledException(ex);
				}
			}
		}
		finally
		{
			--_invocationListLock;
		}

		if (needsCompaction && _invocationListLock is 0)
		{
			_invocationList.RemoveAll(static rec => rec is null);
		}
	}

	public Func<Exception, bool>? ExceptionHandler { get; set; }

	#endregion

}

public class Event<T1, T2, T3, T4, T5> : IEvent<T1, T2, T3, T4, T5>
{

	private readonly record struct Rec(EventRegistration Reg, Action<T1, T2, T3, T4, T5> Handler, Lifetime Lifetime);

	private readonly List<Rec?> _invocationList = [];
	private int32 _invocationListLock;
	private uint64 _handle;

	#region IEvent<T1, T2, T3, T4, T5> Implementations

	public EventRegistration Add(Action<T1, T2, T3, T4, T5> handler, Lifetime lifetime = default)
	{
		if (lifetime.IsExpired)
		{
			return default;
		}

		EventRegistration reg = new(this, ++_handle);
		_invocationList.Add(new(reg, handler, lifetime));
		return reg;
	}

	public bool Remove(EventRegistration registration)
	{
		if (registration == default)
		{
			return false;
		}

		if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
		{
			if (_invocationListLock > 0)
			{
				_invocationList[i] = null;
			}
			else
			{
				_invocationList.RemoveAt(i);
			}

			return true;
		}

		return false;
	}

	public int32 RemoveAll(object? target)
	{
		if (target is null)
		{
			return 0;
		}

		int32 count = 0;
		for (int32 i = _invocationList.Count - 1; i >= 0; --i)
		{
			Rec? rec = _invocationList[i];
			if (rec is not null && rec.Value.Handler.Target == target)
			{
				if (_invocationListLock > 0)
				{
					_invocationList[i] = null;
				}
				else
				{
					_invocationList.RemoveAt(i);
				}

				++count;
			}
		}

		return count;
	}

	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
	{
		bool needsCompaction = false;

		try
		{
			++_invocationListLock;

			int32 count = _invocationList.Count;
			for (int32 i = 0; i < count; ++i)
			{
				if (_invocationList[i] is not { } rec)
				{
					needsCompaction = true;
					continue;
				}

				if (rec.Lifetime.IsExpired)
				{
					_invocationList[i] = null;
					needsCompaction = true;
					continue;
				}

				try
				{
					rec.Handler(arg1, arg2, arg3, arg4, arg5);
				}
				catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
				{
					ExceptionGuard.PublishUnhandledException(ex);
				}
			}
		}
		finally
		{
			--_invocationListLock;
		}

		if (needsCompaction && _invocationListLock is 0)
		{
			_invocationList.RemoveAll(static rec => rec is null);
		}
	}

	public Func<Exception, bool>? ExceptionHandler { get; set; }

	#endregion

}

public class Event<T1, T2, T3, T4, T5, T6> : IEvent<T1, T2, T3, T4, T5, T6>
{

	private readonly record struct Rec(EventRegistration Reg, Action<T1, T2, T3, T4, T5, T6> Handler, Lifetime Lifetime);

	private readonly List<Rec?> _invocationList = [];
	private int32 _invocationListLock;
	private uint64 _handle;

	#region IEvent<T1, T2, T3, T4, T5, T6> Implementations

	public EventRegistration Add(Action<T1, T2, T3, T4, T5, T6> handler, Lifetime lifetime = default)
	{
		if (lifetime.IsExpired)
		{
			return default;
		}

		EventRegistration reg = new(this, ++_handle);
		_invocationList.Add(new(reg, handler, lifetime));
		return reg;
	}

	public bool Remove(EventRegistration registration)
	{
		if (registration == default)
		{
			return false;
		}

		if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
		{
			if (_invocationListLock > 0)
			{
				_invocationList[i] = null;
			}
			else
			{
				_invocationList.RemoveAt(i);
			}

			return true;
		}

		return false;
	}

	public int32 RemoveAll(object? target)
	{
		if (target is null)
		{
			return 0;
		}

		int32 count = 0;
		for (int32 i = _invocationList.Count - 1; i >= 0; --i)
		{
			Rec? rec = _invocationList[i];
			if (rec is not null && rec.Value.Handler.Target == target)
			{
				if (_invocationListLock > 0)
				{
					_invocationList[i] = null;
				}
				else
				{
					_invocationList.RemoveAt(i);
				}

				++count;
			}
		}

		return count;
	}

	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
	{
		bool needsCompaction = false;

		try
		{
			++_invocationListLock;

			int32 count = _invocationList.Count;
			for (int32 i = 0; i < count; ++i)
			{
				if (_invocationList[i] is not { } rec)
				{
					needsCompaction = true;
					continue;
				}

				if (rec.Lifetime.IsExpired)
				{
					_invocationList[i] = null;
					needsCompaction = true;
					continue;
				}

				try
				{
					rec.Handler(arg1, arg2, arg3, arg4, arg5, arg6);
				}
				catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
				{
					ExceptionGuard.PublishUnhandledException(ex);
				}
			}
		}
		finally
		{
			--_invocationListLock;
		}

		if (needsCompaction && _invocationListLock is 0)
		{
			_invocationList.RemoveAll(static rec => rec is null);
		}
	}

	public Func<Exception, bool>? ExceptionHandler { get; set; }

	#endregion

}

public class Event<T1, T2, T3, T4, T5, T6, T7> : IEvent<T1, T2, T3, T4, T5, T6, T7>
{

	private readonly record struct Rec(EventRegistration Reg, Action<T1, T2, T3, T4, T5, T6, T7> Handler, Lifetime Lifetime);

	private readonly List<Rec?> _invocationList = [];
	private int32 _invocationListLock;
	private uint64 _handle;

	#region IEvent<T1, T2, T3, T4, T5, T6, T7> Implementations

	public EventRegistration Add(Action<T1, T2, T3, T4, T5, T6, T7> handler, Lifetime lifetime = default)
	{
		if (lifetime.IsExpired)
		{
			return default;
		}

		EventRegistration reg = new(this, ++_handle);
		_invocationList.Add(new(reg, handler, lifetime));
		return reg;
	}

	public bool Remove(EventRegistration registration)
	{
		if (registration == default)
		{
			return false;
		}

		if (_invocationList.FindIndex(rec => rec?.Reg == registration) is var i && _invocationList.IsValidIndex(i))
		{
			if (_invocationListLock > 0)
			{
				_invocationList[i] = null;
			}
			else
			{
				_invocationList.RemoveAt(i);
			}

			return true;
		}

		return false;
	}

	public int32 RemoveAll(object? target)
	{
		if (target is null)
		{
			return 0;
		}

		int32 count = 0;
		for (int32 i = _invocationList.Count - 1; i >= 0; --i)
		{
			Rec? rec = _invocationList[i];
			if (rec is not null && rec.Value.Handler.Target == target)
			{
				if (_invocationListLock > 0)
				{
					_invocationList[i] = null;
				}
				else
				{
					_invocationList.RemoveAt(i);
				}

				++count;
			}
		}

		return count;
	}

	public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
	{
		bool needsCompaction = false;

		try
		{
			++_invocationListLock;

			int32 count = _invocationList.Count;
			for (int32 i = 0; i < count; ++i)
			{
				if (_invocationList[i] is not { } rec)
				{
					needsCompaction = true;
					continue;
				}

				if (rec.Lifetime.IsExpired)
				{
					_invocationList[i] = null;
					needsCompaction = true;
					continue;
				}

				try
				{
					rec.Handler(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
				}
				catch (Exception ex) when (ExceptionHandler?.Invoke(ex) is not true)
				{
					ExceptionGuard.PublishUnhandledException(ex);
				}
			}
		}
		finally
		{
			--_invocationListLock;
		}

		if (needsCompaction && _invocationListLock is 0)
		{
			_invocationList.RemoveAll(static rec => rec is null);
		}
	}

	public Func<Exception, bool>? ExceptionHandler { get; set; }

	#endregion

}



