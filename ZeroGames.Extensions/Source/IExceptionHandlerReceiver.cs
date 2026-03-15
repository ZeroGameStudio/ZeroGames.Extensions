// Copyright Zero Games. All Rights Reserved.

namespace ZeroGames.Extensions;

public interface IExceptionHandlerReceiver
{
	Func<Exception, bool>? ExceptionHandler { set; }
}


