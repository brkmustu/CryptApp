namespace Auth.Application.Contracts;

public class SignInRequestDto
{
    public string ApiKey { get; set; }
    public string Password { get; set; }
}
