namespace Candidate_Test_Task.Contracts.Response
{
    public record CandidateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = null!;
        public string? PreferredCallTime { get; set; }
        public string? LinkedInProfile { get; set; }
        public string? GitHubProfile { get; set; }
        public string FreeTextComment { get; set; } = null!;

    }

}
