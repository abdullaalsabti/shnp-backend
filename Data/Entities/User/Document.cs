namespace WebApplication1.Data.Entities;

public class Document
{
    public int UserId { get; set; }
    public int DocumentId { get; set; }


    //Navigation Property(s):
    public DocumentType DocumentType { get; set; } = new();
    public ICollection<DocumentUrl> Urls { get; set; } = new List<DocumentUrl>();
}

public class DocumentUrl
{
    public int DocumentId { get; set; }
    public int UrlId { get; set; }
    public string Url { get; set; } = string.Empty;

    public Document Document { get; set; }
}