using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSoundcloudApi.Models
{
    public class Collection
    {
        public string artwork_url { get; set; }
        public object caption { get; set; }
        public bool commentable { get; set; }
        public int comment_count { get; set; }
        public DateTime created_at { get; set; }
        public string description { get; set; }
        public bool downloadable { get; set; }
        public int download_count { get; set; }
        public int duration { get; set; }
        public int full_duration { get; set; }
        public string embeddable_by { get; set; }
        public string genre { get; set; }
        public bool has_downloads_left { get; set; }
        public int id { get; set; }
        public string kind { get; set; }
        public string label_name { get; set; }
        public DateTime last_modified { get; set; }
        public string license { get; set; }
        public int likes_count { get; set; }
        public string permalink { get; set; }
        public string permalink_url { get; set; }
        public int playback_count { get; set; }
        public bool @public { get; set; }
        public object publisher_metadata { get; set; }
        public object purchase_title { get; set; }
        public object purchase_url { get; set; }
        public object release_date { get; set; }
        public int reposts_count { get; set; }
        public object secret_token { get; set; }
        public string sharing { get; set; }
        public string state { get; set; }
        public bool streamable { get; set; }
        public string tag_list { get; set; }
        public string title { get; set; }
        public string uri { get; set; }
        public string urn { get; set; }
        public int user_id { get; set; }
        public object visuals { get; set; }
        public string waveform_url { get; set; }
        public DateTime display_date { get; set; }
        public Media media { get; set; }
        public string station_urn { get; set; }
        public string station_permalink { get; set; }
        public string track_authorization { get; set; }
        public string monetization_model { get; set; }
        public string policy { get; set; }
        public User user { get; set; }
    }
}
