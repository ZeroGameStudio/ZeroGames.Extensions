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


