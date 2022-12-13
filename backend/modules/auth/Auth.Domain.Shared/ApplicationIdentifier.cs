namespace Auth;

public class ApplicationIdentifier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
}
