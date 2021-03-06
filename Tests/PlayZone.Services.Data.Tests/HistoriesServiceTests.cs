﻿namespace PlayZone.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using PlayZone.Data;
    using PlayZone.Data.Models;
    using PlayZone.Data.Repositories;
    using PlayZone.Services.Mapping;
    using Xunit;

    public class HistoriesServiceTests
    {
        private readonly EfDeletableEntityRepository<VideoHistory> historyRepository;
        private readonly HistoriesService service;

        private readonly VideoHistory videoHistory;

        public HistoriesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());

            this.historyRepository = new EfDeletableEntityRepository<VideoHistory>(new ApplicationDbContext(options.Options));

            this.service = new HistoriesService(this.historyRepository);

            this.videoHistory = new VideoHistory
            {
                Id = 1,
                UserId = "user1",
                VideoId = "video1",
            };
        }

        [Fact]
        public async Task GetVideosHistoryByUserWorkCorrecrTest()
        {
            await this.historyRepository.AddAsync(this.videoHistory);
            await this.historyRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(VideoHistoriesViewModel).Assembly);
            var videos = this.service.GetVideosHistoryByUser<VideoHistoriesViewModel>("user1");

            Assert.Single(videos);
        }

        [Fact]
        public async Task AddVideoToHistoryAsyncWorckCorrectTest()
        {
            await this.service.AddVideoToHistoryAsync(this.videoHistory.VideoId, this.videoHistory.UserId);

            Assert.Single(this.historyRepository.All());
        }

        [Fact]
        public async Task AddVideoToHistoryAsyncNotAddSameRecoardTest()
        {
            await this.service.AddVideoToHistoryAsync(this.videoHistory.VideoId, this.videoHistory.UserId);
            await this.service.AddVideoToHistoryAsync(this.videoHistory.VideoId, this.videoHistory.UserId);

            Assert.Single(this.historyRepository.All());
        }

        [Fact]
        public async Task DeleteFromHistoryAsyncWorkCorrectTest()
        {
            await this.historyRepository.AddAsync(this.videoHistory);
            await this.historyRepository.SaveChangesAsync();

            await this.service.DeleteFromHistoryAsync("video1", "user1");

            Assert.Empty(this.historyRepository.All());
        }

        [Fact]
        public async Task DeleteFromHistoryAsyncNotDeleteIfNotExistTest()
        {
            await this.historyRepository.AddAsync(this.videoHistory);
            await this.historyRepository.SaveChangesAsync();

            await this.service.DeleteFromHistoryAsync("test", "user1");

            Assert.Single(this.historyRepository.All());
        }

        public class VideoHistoriesViewModel : IMapFrom<VideoHistory>
        {
            public string VideoId { get; set; }

            public string UserId { get; set; }
        }
    }
}
