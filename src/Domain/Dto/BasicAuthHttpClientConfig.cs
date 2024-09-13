namespace Domain.Dto;
public class BasicAuthHttpClientConfig
{
    public string BaseUrl { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool IsAuthenticated { get; set; }
}
