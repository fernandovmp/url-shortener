using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.WebApi.Models;
using UrlShortener.WebApi.Repositories;
using UrlShortener.WebApi.ViewModels;

namespace UrlShortener.WebApi.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class UrlsController : ControllerBase
    {
        private readonly IUrlRepository _urlRepository;

        public UrlsController(IUrlRepository urlRepository) => _urlRepository = urlRepository;

        [HttpGet("/{shortUrl}")]
        public async Task<IActionResult> GetUrl(string shortUrl)
        {
            Url url = await _urlRepository.GetByShortUrl(shortUrl);
            if (url is null)
            {
                return NotFound();
            }
            return Redirect(url.OriginalUrl);
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl(UrlsPostViewModel data)
        {
            string shortUrl = Guid.NewGuid().ToString("N").Substring(0, 6);
            var url = new Url()
            {
                OriginalUrl = data.Url,
                ShortUrl = shortUrl
            };
            await _urlRepository.Save(url).ConfigureAwait(false);

            return CreatedAtAction(nameof(GetUrl), new
            {
                shortUrl
            }, new
            {
                ShortUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}/{shortUrl}"
            });
        }
    }
}
