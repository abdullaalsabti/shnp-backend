namespace WebApplication1.Data.Entities;

public class DocumentType
{
    public string TypeCode { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
