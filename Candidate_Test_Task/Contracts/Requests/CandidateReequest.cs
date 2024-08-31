namespace Candidate_Test_Task.Contracts.Requests
{
    public record CandidateReequest
    {


        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [Phone]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;
        [MaxLength(50)]
        public string? PreferredCallTime { get; set; }
        [Url(ErrorMessage = "Invalid LinkedIn URL format.")]
        [RegularExpression(@"^(https?:\/\/)?([\w]+\.)?linkedin\.com\/.*$", ErrorMessage = "Invalid LinkedIn URL format.")]
        public string? LinkedInProfile { get; set; }

        [Url(ErrorMessage = "Invalid GitHub URL format.")]
        [RegularExpression(@"^(https?:\/\/)?([\w]+\.)?github\.com\/.*$", ErrorMessage = "Invalid GitHub URL format.")]
        public string? GitHubProfile { get; set; }
        [Required]
        [MaxLength(5000)]
        public string FreeTextComment { get; set; } = null!;


    }

}
