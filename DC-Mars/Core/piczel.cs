using System;
using System.Diagnostics;

namespace DC_Mars.Core
{
    public abstract class Piczel
    {
        private int id;
        private string title;
        private string rendered_description;
        private bool live;
        private DateTime live_since;
        private string slug;

        protected Piczel(int id, string title, string description, string rendered_description, int follower_count, bool live, DateTime live_since, string slug)
        {
            this.id = id;
            this.title = title ?? throw new ArgumentNullException(nameof(title));
            this.Description = description ?? throw new ArgumentNullException(nameof(description));
            this.rendered_description = rendered_description ?? throw new ArgumentNullException(nameof(rendered_description));
            this.Follower_count = follower_count;
            this.live = live;
            this.live_since = live_since;
            this.slug = slug ?? throw new ArgumentNullException(nameof(slug));
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get; set; }
        public string Rendered_description { get => rendered_description; set => rendered_description = value; }
        public int Follower_count { get; set; }
        public bool Live { get => live; set => live = value; }
        public DateTime Live_since { get => live_since; set => live_since = value; }
        public string Slug { get => slug; set => slug = value; }
    }
}