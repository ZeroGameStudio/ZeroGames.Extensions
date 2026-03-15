// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions;

public static class ExceptionGuard
{
	public static void PublishUnhandledException(Exception? exception)
	{
		if (exception is null)
		{
			return;
		}

		if (OnUnhandedException is not null)
		{
			foreach (var handler in OnUnhandedException.GetInvocationList())
			{
				if (handler.To<Func<Exception, bool>>()(exception))
				{
					return;
				}
			}
		}

		throw exception;
	}
	
	public static event Func<Exception, bool>? OnUnhandedException;
}


