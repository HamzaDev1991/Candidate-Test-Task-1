


namespace Candidate_Test_Task.Services
{
    public class CashService(IDistributedCache distributedCache) : ICashService
    {
        private readonly IDistributedCache _distributedCache = distributedCache;

        public async Task<T?> GetAsync<T>(string _Key) where T : class
        {
            var cashedValue = await _distributedCache.GetStringAsync(_Key);

            return cashedValue is null 
                ? null
                : JsonSerializer.Deserialize<T>(cashedValue);
        }


        public async Task SetAsync<T>(string _Key, T value) where T : class
        {
            await _distributedCache.SetStringAsync(_Key, JsonSerializer.Serialize(value));
        }

        public async Task RemoveAsync(string _Key) 
        {
            await _distributedCache.RemoveAsync(_Key);
        }

        
    }
}
