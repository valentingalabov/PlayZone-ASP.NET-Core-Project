﻿namespace PlayZone.Web.ViewModels.Histories
{
    using System;

    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class HistoryVideoViewModel : IMapFrom<VideoHistory>
    {
        public string VideoId { get; set; }

        public string VideoChanelId { get; set; }

        public string VideoChanelTitle { get; set; }

        public string VideoUrl { get; set; }

        public string VideoTitle { get; set; }

        public string VideoDescription { get; set; }

        public string ShortDescription
        {
            get
            {
                return this.VideoDescription.Length > 150
                        ? this.VideoDescription.Substring(0, 150) + "..."
                        : this.VideoDescription;
            }
        }

        public DateTime VideoHistoryCreatedOn { get; set; }

        public string VideoImageUrl => $"https://i3.ytimg.com/vi/{this.VideoUrl}/maxresdefault.jpg";
    }
}