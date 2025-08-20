namespace WebApplication1.Dto;

public class DocumentDto
{
    public string DocumentTypeCode { get; set; } = string.Empty;
    public List<string> DocumentUrls { get; set; } = new();
}