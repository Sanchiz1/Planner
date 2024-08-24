namespace Web.Models.User;

public class UserQueryParameters
{
    public string Email { get; init; } = string.Empty;
    public int Page { get; init; } = 1;
    public int Size { get; init; } = 10;
}
