using Candidate_Test_Task.Abstraction;

namespace Candidate_Test_Task.Errors
{
    public class CandidateErrors
    {
        public static readonly Error CandidateNotFound = new ("Candidat", "Candidate Not Found");
    }
}
