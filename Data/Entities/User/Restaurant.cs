using WebApplication1.Dto;

namespace WebApplication1.Data.Entities;

public class Restaurant
{
    public int UserId { get; set; }
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string BankAccountIban { get; set; } = string.Empty;
    public string OperationRepresentativeEmail { get; set; } = string.Empty;
    public string OperationRepresentativePhoneNumber { get; set; } = string.Empty;
    public string OperationRepresentativeFullNameEn { get; set; } = string.Empty;
    public string OperationRepresentativeFullNameAr { get; set; } = string.Empty;
    public string OperationRepresentativeEmailAddress { get; set; } = string.Empty;
    public string ManagementPhoneNumber { get; set; } = string.Empty;
    public string CommercialRegistrationNumber { get; set; } = string.Empty;
    public string MainBranchNameAr { get; set; } = string.Empty;
    public string MainBranchNameEn { get; set; } = string.Empty;
    public string BranchDistrict { get; set; } = string.Empty;
    public string BranchAddressName { get; set; } = string.Empty;
    public string BranchStreet { get; set; } = string.Empty;
    public int BranchBuildingNumber { get; set; }
    public string BranchAddressDescription { get; set; } = string.Empty;
    public string TwitterSocialMediaLink { get; set; } = string.Empty;

    public string InstagramSocialMediaLink { get; set; } = string.Empty;

    //Navigation Property:
    public ICollection<RestaurantType> RestaurantTypes { get; set; } = new List<RestaurantType>();
    public ICollection<Document> Documents { get; set; } = new List<Document>();
    public ICollection<WorkingDetail> WorkingDetails { get; set; } = new List<WorkingDetail>();

    public RestaurantCount RestaurantCount { get; set; } = new();


    public static Restaurant CreateFromRegistration(UserForRegistrationDto u, DataContextEf contextEf)
    {
        var user = new Restaurant
        {
            NameEn = u.NameEn,
            NameAr = u.NameAr,
            Email = u.Email,
            BankAccountIban = u.BankAccountIban,
            OperationRepresentativeEmail = u.OperationRepresentativeEmail,
            OperationRepresentativePhoneNumber = u.OperationRepresentativePhoneNumber,
            OperationRepresentativeFullNameEn = u.OperationRepresentativeFullNameEn,
            OperationRepresentativeFullNameAr = u.OperationRepresentativeFullNameAr,
            OperationRepresentativeEmailAddress = u.OperationRepresentativeEmailAddress,
            ManagementPhoneNumber = u.ManagementPhoneNumber,
            CommercialRegistrationNumber = u.CommercialRegistrationNumber,
            MainBranchNameAr = u.MainBranchNameAr,
            MainBranchNameEn = u.MainBranchNameEn,
            BranchDistrict = u.BranchDistrict,
            BranchAddressName = u.BranchAddressName,
            BranchStreet = u.BranchStreet,
            BranchBuildingNumber = u.BranchBuildingNumber,
            BranchAddressDescription = u.BranchAddressDescription,
            TwitterSocialMediaLink = u.TwitterSocialMediaLink,
            InstagramSocialMediaLink = u.InstagramSocialMediaLink
        };


        // Link RestaurantTypes
        foreach (var typeId in u.FoodCategories)
        {
            var existingType = contextEf.RestaurantTypes.FirstOrDefault(rt => rt.TypeId == typeId);
            if (existingType != null) user.RestaurantTypes.Add(existingType);
        }

        // Add Documents
        foreach (var docDto in u.Documents)
        {
            var documentType = contextEf.DocumentTypeCodes.FirstOrDefault(tc => tc.TypeCode == docDto.DocumentTypeCode);
            if (documentType == null) continue;
            var document = new Document
            {
                DocumentType = documentType,
                Urls = docDto.DocumentUrls.Select(url => new DocumentUrl
                {
                    Url = url
                }).ToList()
            };
            Console.WriteLine($"document: {document.DocumentType}");
            user.Documents.Add(document); // This ensures proper tracking
        }


        // Add WorkingDetails
        foreach (var wd in u.WorkingDetails) user.WorkingDetails.Add(wd);

        return user;
    }
}
