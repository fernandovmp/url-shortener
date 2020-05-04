using System.Threading.Tasks;
using UrlShortener.WebApi.Models;

namespace UrlShortener.WebApi.Repositories
{
    public interface IUrlRepository
    {
        Task<Url> GetByShortUrl(string shortUrl);
        Task Save(Url url);
    }
}
