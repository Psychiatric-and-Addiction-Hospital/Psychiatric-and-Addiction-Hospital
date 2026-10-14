namespace Application.Common.Interfaces.Authentication
{
    public interface ICurrentUser
    {
        string? UserId { get; }

        string? Email { get; }

        bool IsAuthenticated { get; }
    }
}
