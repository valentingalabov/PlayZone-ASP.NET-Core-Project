﻿namespace PlayZone.Web.ViewModels.Videos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class VideoDetailsViewModel : IMapFrom<Video>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public string ChanelTitle { get; set; }

        public string ChanelId { get; set; }

        public string ChanelImageUrl { get; set; }

        public string EmbedVideoUrl => $"https://www.youtube.com/embed/{this.Url}";

        public string EmbedChanelImageUrl => $"https://res.cloudinary.com/dqh6dvohu/image/upload/w_100,c_fill,ar_1:1,g_auto,r_max,bo_2px_solid_blue,b_rgb:ffffff/{this.ChanelImageUrl}";

        public DateTime CreatedOn { get; set; }

        public int UpVotesCount { get; set; }

        public int DownVotesCount { get; set; }

        public IEnumerable<VideoCommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Video, VideoDetailsViewModel>()
                .ForMember(x => x.UpVotesCount, options =>
                {
                    options.MapFrom(v => v.Votes.Where(v => (int)v.Type == 1).Sum(vt => (int)vt.Type));
                });

            configuration.CreateMap<Video, VideoDetailsViewModel>()
                .ForMember(x => x.DownVotesCount, options =>
                {
                    options.MapFrom(v => v.Votes.Where(v => (int)v.Type == -1).Sum(vt => (int)vt.Type) * -1);
                });
        }
    }
}
