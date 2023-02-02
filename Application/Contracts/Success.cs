namespace Application.Contracts;
public class Success : ISuccess, IResponse
{
    // Singleton pattern: Ensures only one instance of Success is ever created and used
    // We're using this pattern instead of making the class static, because this way we can implement interfaces,
    // while still ensuring only one instance exists
    public static ISuccess Instance => new Success();

    private Success()
    {
    }
}

