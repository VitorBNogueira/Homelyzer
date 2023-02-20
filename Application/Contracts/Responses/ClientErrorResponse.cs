using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Responses;

public class ClientErrorResponse : IClientFailure
{
    public string ErrorCode { get; }
	public ClientErrorResponse(string errorCode)
	{
        ErrorCode = errorCode;
    }
}
