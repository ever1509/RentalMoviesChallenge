namespace RentalMovies.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; } 
        string RoleId { get; set; }
    }
}
