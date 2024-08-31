
using Candidate_Test_Task.Abstraction;
using Candidate_Test_Task.Contracts.Requests;
using Candidate_Test_Task.Errors;
using System.Collections.Generic;

namespace Candidate_Test_Task.Services
{
    public class CandidateServicesForCSV : ICandidateServices
    {
        private readonly string _csvFilePath = "candidates.csv";
        private const string _cachePrefix = "availableCandidate";
        private readonly ICashService _cashService;

        public CandidateServicesForCSV(ICashService cashService)
        {
            _cashService = cashService;
        }

        public async Task<IEnumerable<Candidate>> GetAll()
        {
            try
            {
                List<Candidate> candidates = new List<Candidate>();

                if (File.Exists(_csvFilePath))
                {
                    var lines = await File.ReadAllLinesAsync(_csvFilePath);
                    foreach (var line in lines.Skip(1))
                    {
                        var values = line.Split(',');
                        var candidate = new Candidate
                        {
                            Id = int.Parse(values[0]),
                            FirstName = values[1],
                            LastName = values[2],
                            PhoneNumber = values[3],
                            Email = values[4],
                            PreferredCallTime = values[5],
                            LinkedInProfile = values[6],
                            GitHubProfile = values[7],
                            FreeTextComment = values[8]
                        };
                        candidates.Add(candidate);
                    }
                }

                return candidates;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll: {ex.Message}");
                return Enumerable.Empty<Candidate>();
            }
        }

        public async Task<Candidate?> Get(string email)
        {
            var cacheKey = $"{_cachePrefix}-{email}";

            var cachedCandidates = await _cashService.GetAsync<IEnumerable<Candidate>>(cacheKey);
            IEnumerable<Candidate> candidates;

            if (cachedCandidates == null)
            {
                candidates = await GetAll();
                await _cashService.SetAsync(cacheKey, candidates);
            }
            else
            {
                candidates = cachedCandidates;
            }

            return candidates.SingleOrDefault(c => c.Email == email);
        }

        public async Task<Result> AddAsync(string email, Candidate candidate)
        {
            var candidates = (await GetAll()).ToList();
            candidate.Id = candidates.Any() ? candidates.Max(c => c.Id) + 1 : 1;

            candidates.Add(candidate);
            await SaveAllCandidatesToFile(candidates);

            // Cache the newly added candidate
            var cacheKey = $"{_cachePrefix}-{email}";
            await _cashService.SetAsync(cacheKey, candidates);

            return Result.Success(candidate); 
        }

        public async Task<Result> UpdateAsync(string email, CandidateReequest updatedCandidate)
        {
            var candidates = await GetCandidatesCached(email);
            var currentCandidate = candidates.SingleOrDefault(c => c.Email == email);

            if (currentCandidate is null)
                return Result.Failure(CandidateErrors.CandidateNotFound);

            // Update the candidate details
            currentCandidate.FirstName = updatedCandidate.FirstName;
            currentCandidate.LastName = updatedCandidate.LastName;
            currentCandidate.PhoneNumber = updatedCandidate.PhoneNumber;
            currentCandidate.Email = updatedCandidate.Email;
            currentCandidate.PreferredCallTime = updatedCandidate.PreferredCallTime;
            currentCandidate.LinkedInProfile = updatedCandidate.LinkedInProfile;
            currentCandidate.GitHubProfile = updatedCandidate.GitHubProfile;
            currentCandidate.FreeTextComment = updatedCandidate.FreeTextComment;

            await SaveAllCandidatesToFile(candidates);

            // Update the cache with the modified candidate list
            var cacheKey = $"{_cachePrefix}-{email}";
            await _cashService.SetAsync(cacheKey, candidates);

            return Result.Success();
        }

        public async Task<Result> SaveAsync(CandidateReequest candidate)
        {
            if (await IsCandidateFound(candidate.Email))
            {
                await UpdateAsync(candidate.Email, candidate);
            }
            else
            {
                await AddAsync(candidate.Email, candidate.MapToCandidate());
            }

            return Result.Success(candidate); 
        }

        public async Task<bool> IsCandidateFound(string email)
        {
            var candidates = (await GetAll()).ToList();
            return candidates.Any(c => c.Email == email);
        }

        private async Task SaveAllCandidatesToFile(IEnumerable<Candidate> candidates)
        {
            var csvLines = new List<string>
        {
            "Id,FirstName,LastName,PhoneNumber,Email,PreferredCallTime,LinkedInProfile,GitHubProfile,FreeTextComment"
        };

            csvLines.AddRange(candidates.Select(c => $"{c.Id},{c.FirstName},{c.LastName},{c.PhoneNumber},{c.Email},{c.PreferredCallTime},{c.LinkedInProfile},{c.GitHubProfile},{c.FreeTextComment}"));

            await File.WriteAllLinesAsync(_csvFilePath, csvLines);
        }

        public async Task<IEnumerable<Candidate>> GetCandidatesCached(string email)
        {
            var cacheKey = $"{_cachePrefix}-{email}";

            var cachedCandidates = await _cashService.GetAsync<IEnumerable<Candidate>>(cacheKey);
            IEnumerable<Candidate> candidates;

            if (cachedCandidates == null)
            {
                candidates = await GetAll();
                await _cashService.SetAsync(cacheKey, candidates);
            }
            else
            {
                candidates = cachedCandidates;
            }

            return candidates;
        }
    }

}
