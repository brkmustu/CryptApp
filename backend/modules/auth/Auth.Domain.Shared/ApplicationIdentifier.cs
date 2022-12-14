namespace Auth;

public class ApplicationIdentifier
{
    public string ApiKey { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}
