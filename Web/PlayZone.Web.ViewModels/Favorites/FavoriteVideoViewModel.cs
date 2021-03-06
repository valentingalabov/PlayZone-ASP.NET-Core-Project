﻿namespace PlayZone.Web.ViewModels.Favorites
{
    using System;

    using PlayZone.Data.Models;
    using PlayZone.Services.Mapping;

    public class FavoriteVideoViewModel : IMapFrom<FavoriteVideo>
    {
        public string VideoId { get; set; }

        public string VideoChannelId { get; set; }

        public string VideoChannelTitle { get; set; }

        public string VideoUrl { get; set; }

        public string VideoTitle { get; set; }

        public string VideoDescription { get; set; }

        public string ShortDescription
        {
            get
            {
                if (this.VideoDescription != null)
                {
                    return this.VideoDescription.Length > 150
                       ? this.VideoDescription.Substring(0, 150) + "..."
                       : this.VideoDescription;
                }

                return null;
            }
        }

        public DateTime VideoHistoryCreatedOn { get; set; }

        public string VideoImageUrl => $"https://i3.ytimg.com/vi/{this.VideoUrl}/maxresdefault.jpg";
    }
}
