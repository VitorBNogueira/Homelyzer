namespace Application.Contracts;

public interface IServerFailure : IResponse
{
    string ErrorCode { get; }
}
