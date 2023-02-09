using Application.Contracts;
using Application.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ErrorResults
{
    // Client
    public static IClientFailure Unauthorized(string message = "") => new ClientErrorResponse($"UNAUTHORIZED_USER: {message}");
    public static IClientFailure ResourceNotFound(string message = "") => new ClientErrorResponse($"RESOURCE_NOT_FOUND: {message}");
    public static IClientFailure NothingToUpdate(string message = "") => new ClientErrorResponse($"NOTHING_TO_UPDATE: {message}");

    // Server
    public static IServerFailure DatabaseError(string message = "") => new ServerErrorResponse($"DATABASE_ERROR: {message}");
    public static IServerFailure UnexpectedError(string message = "") => new ServerErrorResponse($"UNEXPECTED_ERROR: {message}");
    public static IServerFailure DataConsistencyError(string message = "") => new ServerErrorResponse($"INCONSISTENT_DATA: {message}");

}
