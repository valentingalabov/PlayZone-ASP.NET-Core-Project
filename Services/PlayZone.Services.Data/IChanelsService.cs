﻿namespace PlayZone.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using PlayZone.Data.Models;

    public interface IChanelsService
    {
        Task<string> CreateChanelAsync(string title, string description, ApplicationUser user);

        bool IsValidChanel(string title);

        Task UploadAsync(IFormFile file, string id);

        T GetChanelById<T>(string id);

        int GetAllVideosByChanelCount(string id);

        bool IsOwner(string chanelId, ApplicationUser user);

        Task CreateImage(string url, string cloudinaryPublicId, Chanel currentChanel);

        IEnumerable<T> GetVieosByChanel<T>(string id, int? take, int skip = 0);

        Task EditChanelAsync(string id, string title, string description);
    }
}
