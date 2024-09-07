using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoundcloudApi.Models
{
    public class User
    {
        public string avatar_url { get; set; }
        public string city { get; set; }
        public int? comments_count { get; set; }
        public string country_code { get; set; }
        public DateTime created_at { get; set; }
        public List<CreatorSubscription> creator_subscriptions { get; set; }
        public CreatorSubscription creator_subscription { get; set; }
        public object description { get; set; }
        public int? followers_count { get; set; }
        public int? followings_count { get; set; }
        public string first_name { get; set; }
        public string full_name { get; set; }
        public int? groups_count { get; set; }
        public int? id { get; set; }
        public string kind { get; set; }
        public DateTime last_modified { get; set; }
        public string last_name { get; set; }
        public int? likes_count { get; set; }
        public int? playlist_likes_count { get; set; }
        public string permalink { get; set; }
        public string permalink_url { get; set; }
        public int? playlist_count { get; set; }
        public object reposts_count { get; set; }
        public int? track_count { get; set; }
        public string uri { get; set; }
        public string urn { get; set; }
        public string username { get; set; }
        public bool verified { get; set; }
        public object visuals { get; set; }
        public Badges badges { get; set; }
        public string station_urn { get; set; }
        public string station_permalink { get; set; }
    }
}
