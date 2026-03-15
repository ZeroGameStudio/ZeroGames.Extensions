// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public readonly record struct EventRegistration(IRemoveEventRegistrationSource? Owner, uint64 Handle) : IDisposable
{
	public bool IsValid => Owner is not null && Handle is not 0;
    
	#region IDisposable Implementations

	public void Dispose() => Owner?.Remove(this);

	#endregion
}


