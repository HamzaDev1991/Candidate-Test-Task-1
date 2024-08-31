namespace Candidate_Test_Task.Services
{
    public interface ICashService
    {
        Task<T?> GetAsync<T>(string _Key) where T :class;
        Task SetAsync<T>(string _Key,T value) where T :class;
        Task RemoveAsync(string _Key);
    }
}
