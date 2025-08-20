using WebApplication1.Data.Entities;

namespace WebApplication1.Dto;

public class UserForRegistrationDto
{
    // Login / Security
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;

    // Restaurant Info
    public string NameEn { get; set; } = string.Empty;
    public string NameAr { get; set; } = string.Empty;
    public string BankAccountIban { get; set; } = string.Empty;
    public string CommercialRegistrationNumber { get; set; } = string.Empty;

    // Representative Info
    public string OperationRepresentativeEmail { get; set; } = string.Empty;
    public string OperationRepresentativePhoneNumber { get; set; } = string.Empty;
    public string OperationRepresentativeFullNameEn { get; set; } = string.Empty;
    public string OperationRepresentativeFullNameAr { get; set; } = string.Empty;
    public string OperationRepresentativeEmailAddress { get; set; } = string.Empty;

    public string ManagementPhoneNumber { get; set; } = string.Empty;

    // Branch Info
    public string MainBranchNameAr { get; set; } = string.Empty;
    public string MainBranchNameEn { get; set; } = string.Empty;
    public string BranchDistrict { get; set; } = string.Empty;
    public string BranchAddressName { get; set; } = string.Empty;
    public string BranchStreet { get; set; } = string.Empty;
    public int BranchBuildingNumber { get; set; }
    public string BranchAddressDescription { get; set; } = string.Empty;

    // Social Media
    public string TwitterSocialMediaLink { get; set; } = string.Empty;
    public string InstagramSocialMediaLink { get; set; } = string.Empty;

    // Related Data (just IDs or codes instead of full navigation objects)

    public IList<int> FoodCategories { get; set; } = new List<int>();
    public IList<DocumentDto> Documents { get; set; } = new List<DocumentDto>();
    public IList<WorkingDetail> WorkingDetails { get; set; } = new List<WorkingDetail>();
}