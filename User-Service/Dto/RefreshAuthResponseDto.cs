namespace User_Service.Dto;

public class RefreshAuthResponseDto
{
    public bool IsSucceed { get; set; }
    public string AccessToken { get; set; }
    public string Message { get; set; }
}