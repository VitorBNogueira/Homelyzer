using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Responses;

public class ServerErrorResponse : IServerFailure
{
    public string ErrorCode { get; }
	public ServerErrorResponse(string errorCode)
	{
        ErrorCode = errorCode;
        //Log.LogError($"{ErrorCode}");
    }
}
