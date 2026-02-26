using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;

public class Leaderboard : Singleton<Leaderboard>
{
    public async void AddScore(string leaderboardId, int score) {
        var playerEntry = await LeaderboardsService.Instance
                .AddPlayerScoreAsync(leaderboardId, score);
    }
    
    public async Task<List<LeaderboardEntry>> GetPaginatedScores(string leaderboardId, int offset, int limit = 10) {
        var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(
            leaderboardId,
            new GetScoresOptions{ Offset = offset, Limit = limit }
        );
        return scoresResponse.Results;
    }
}
