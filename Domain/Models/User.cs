 public record UserDto(string Username, string Password);

 public record UserModel
 {
    [Required]
    public string Username {get; set;} = string.Empty;
    [Required]
    public string Password {get; set;}
 }