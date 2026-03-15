// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions.Async;

public class NeverMemoizedObstream<T> : IMemoizedObstream<T>
{

    public static NeverMemoizedObstream<T> Instance { get; } = new();
    
    private NeverMemoizedObstream(){}

    private const int64 HANDLE = int64.MaxValue / 2;
    
    #region IMemoizedObstream<T> Implementations
    
    public ObstreamRegistration Register(Action<T?, T, bool> callback, Lifetime lifetime = default)
        => new(this, HANDLE);
    
    public bool Unregister(ObstreamRegistration registration)
        => registration.Owner == this && registration.Handle == HANDLE;
    
    public bool IsInitialized => false;

    public T Value
    {
        get => throw new InvalidOperationException();
        set{}
    }

    public T? LastValue => throw new InvalidOperationException();

    #endregion

}


