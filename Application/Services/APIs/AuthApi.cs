public class AuthApi : IApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/login", [AllowAnonymous] async (HttpContext context,
        ITokenService TokenService, IUserRepository userRepository) =>
        {
            UserModel userModel = new()
            {
                Username = context.Request.Query["username"],
                Password = context.Request.Query["password"]
            };

            var userDto = userRepository.GetUser(userModel);
            if (userDto == null) return Results.Unauthorized();
            var token = TokenService.BuildToken(app.Configuration["Jwt:Key"],
                app.Configuration["Jwt:Issuer"], userDto);
            
            return Results.Ok(token);
        });
    }
}