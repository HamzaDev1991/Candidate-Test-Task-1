using Candidate_Test_Task.Contracts.Requests;
using Candidate_Test_Task.Contracts.Response;
using Candidate_Test_Task.Entites;

namespace Candidate_Test_Task.Mapping
{
    public static class ContractMapping
    {
        public static CandidateResponse MapToResponse(this  Candidate candidate) 
        {

            return new()
            {
                Id=candidate.Id,
                FirstName=candidate.FirstName,
                LastName=candidate.LastName,
                PhoneNumber =candidate.PhoneNumber,
                Email =candidate.Email,
                PreferredCallTime = candidate.PreferredCallTime,
                LinkedInProfile = candidate.LinkedInProfile,
                GitHubProfile = candidate.GitHubProfile,
                FreeTextComment = candidate.FreeTextComment,
               
            };
        }

        public static Candidate MapToCandidate(this CandidateReequest reequest)
        {

            return new()
            {
               
                FirstName = reequest.FirstName,
                LastName = reequest.LastName,
                PhoneNumber = reequest.PhoneNumber,
                Email = reequest.Email,
                PreferredCallTime = reequest.PreferredCallTime,
                LinkedInProfile = reequest.LinkedInProfile,
                GitHubProfile = reequest.GitHubProfile,
                FreeTextComment = reequest.FreeTextComment,
            };
        }

    }
}
