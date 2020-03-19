﻿namespace PlayZone.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PlayZone.Data.Common.Models;

    public class Chanel : BaseDeletableModel<string>
    {
        public Chanel()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Videos = new HashSet<Video>();
        }

        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual IEnumerable<Video> Videos { get; set; }
    }
}
