// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions;

public static class DelegateExtensions
{
    extension<T>(T @this) where T : Delegate
    {
        public void GuardedInvoke(Action<T> @try, Action<Exception> @catch)
        {
            foreach (var cb in @this.GetInvocationList().OfType<T>())
            {
                try
                {
                    @try(cb);
                }
                catch (Exception ex)
                {
                    @catch(ex);
                }
            }
        }
    }
}


