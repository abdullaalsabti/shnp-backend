using WebApplication1.Data.Entities;

namespace WebApplication1.Dto;

public class MeDto
{
    public MeDto(User u)
    {
        UserId = u.UserId;
        NameEn = u.NameEn;
        NameAr = u.NameAr;
        Email = u.Email;
        BankAccountIban = u.BankAccountIban;
        ManagementPhoneNumber = u.ManagementPhoneNumber;
        CommercialRegistrationNumber = u.CommercialRegistrationNumber;
        TwitterSocialMediaLink = u.TwitterSocialMediaLink;
        InstagramSocialMediaLink = u.InstagramSocialMediaLink;

        foreach (var doc in u.Documents)
            Documents.Add(new DocumentDto
            {
                DocumentTypeCode = doc.DocumentTypeCode,
                DocumentUrls = doc.Urls.Select(url => url.Url).ToList()
            });
    }

    public int UserId { get; set; }
    public string NameEn { get; set; }
    public string NameAr { get; set; }
    public string Email { get; set; }
    public string BankAccountIban { get; set; }
    public string ManagementPhoneNumber { get; set; }
    public string CommercialRegistrationNumber { get; set; }
    public string? TwitterSocialMediaLink { get; set; }
    public string? InstagramSocialMediaLink { get; set; }
    public string? ImageUrl { get; set; } = string.Empty;

    //Navigation Property
    public ICollection<DocumentDto> Documents { get; set; } = new List<DocumentDto>();
}
