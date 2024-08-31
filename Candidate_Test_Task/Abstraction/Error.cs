namespace Candidate_Test_Task.Abstraction
{
    public record Error(string code, string Desc)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
