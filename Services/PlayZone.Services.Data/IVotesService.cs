﻿namespace PlayZone.Services.Data
{
    using System.Threading.Tasks;

    public interface IVotesService
    {
        Task VoteAsync(string videoId, string userId, bool isUpvote);

        int GetUpVotes(string videoId);

        int GetDownVotes(string videoId);
    }
}
