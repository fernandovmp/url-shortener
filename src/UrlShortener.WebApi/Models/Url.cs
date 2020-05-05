using System;

namespace UrlShortener.WebApi.Models
{
    public class Url
    {
        public string Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
