namespace Application.Contracts;

public interface IClientFailure : IResponse
{
    string ErrorCode { get; }
}
