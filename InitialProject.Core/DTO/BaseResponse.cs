namespace Kawkaba.Core.DTO;

public class BaseResponse
{
    public bool status { get; set; } = true;
    public int ErrorCode { get; set; } = 200;
    public string ErrorMessage { get; set; }
    public dynamic Data { get; set; }
}