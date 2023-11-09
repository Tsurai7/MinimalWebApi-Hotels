public class UserRepository : IUserRepository
{
    private List<UserDto> _users => new()
    {
        new UserDto("Nikita", "1234"),
        new UserDto("Ruslan", "1234")
    };

    public UserDto GetUser(UserModel userModel) =>
       _users.FirstOrDefault(u =>
       string.Equals(u.Username, userModel.Username) &&
       string.Equals(u.Password, userModel.Password)) ??
       throw new Exception("User not found"); 

}