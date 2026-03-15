// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public readonly record struct EventRegistration(IRemoveEventRegistrationSource? Owner, uint64 Handle) : IDisposable
{
    public bool IsValid => Owner is not null && Handle is not 0;
    
    #region IDisposable Implementations

    public void Dispose() => Owner?.Remove(this);

    #endregion
}

public interface IRemoveEventRegistrationSource
{
    bool Remove(EventRegistration registration);
}

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


