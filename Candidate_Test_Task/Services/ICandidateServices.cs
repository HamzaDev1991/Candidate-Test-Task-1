using Candidate_Test_Task.Abstraction;
using Candidate_Test_Task.Contracts.Requests;

namespace Candidate_Test_Task.Services
{
    public interface ICandidateServices
    {
        Task<IEnumerable<Candidate>> GetAll();
        Task<Candidate?> Get(string email);

        // i will not send CandidateReequest here becuse i need to add id manual 
        Task<Result> AddAsync(string email, Candidate candidate);
        Task<Result> UpdateAsync(string email, CandidateReequest candidate);
        Task<Result> SaveAsync(CandidateReequest candidate);
    }
}
