// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public class NeverObstream<T> : IObstream<T>
{

    public static NeverObstream<T> Instance { get; } = new();
    
    private NeverObstream(){}

    private const int64 HANDLE = int64.MaxValue / 2;
    
    #region IObstream<T> Implementations
    
    public ObstreamRegistration Register(Action<T, bool> callback, Lifetime lifetime = default)
        => new(this, HANDLE);
    
    public bool Unregister(ObstreamRegistration registration)
        => registration.Owner == this && registration.Handle == HANDLE;
    
    public bool IsInitialized => false;

    public T Value
    {
        get => throw new InvalidOperationException();
        set{}
    }
    
    #endregion
    
}


