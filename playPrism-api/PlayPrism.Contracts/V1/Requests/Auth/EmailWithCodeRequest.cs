namespace PlayPrism.Contracts.V1.Requests.Auth;

public class EmailWithCodeRequest
{
    public string Email { get; set; }
    public string Code { get; set; }
}