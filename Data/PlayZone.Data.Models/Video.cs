﻿namespace PlayZone.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using PlayZone.Data.Common.Models;

    public class Video : BaseDeletableModel<string>
    {
        public Video()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Comments = new HashSet<Comment>();
            this.VideoHistories = new HashSet<VideoHistory>();
            this.FavoriteVideos = new HashSet<FavoriteVideo>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        public string Url { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ChannelId { get; set; }

        public virtual Channel Channel { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual IEnumerable<Comment> Comments { get; set; }

        public virtual IEnumerable<VideoHistory> VideoHistories { get; set; }

        public virtual IEnumerable<FavoriteVideo> FavoriteVideos { get; set; }

        public virtual IEnumerable<Vote> Votes { get; set; }
    }
}
