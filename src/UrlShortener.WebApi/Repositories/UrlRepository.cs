using System.Threading.Tasks;
using MongoDB.Driver;
using UrlShortener.WebApi.Models;

namespace UrlShortener.WebApi.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly IMongoCollection<Url> _urls;
        public UrlRepository(string connection)
        {
            IMongoClient client = new MongoClient(connection);
            IMongoDatabase database = client.GetDatabase("UrlShortenerDb");
            _urls = database.GetCollection<Url>("urls");
        }
        public async Task<Url> GetByShortUrl(string shortUrl)
        {
            IAsyncCursor<Url> cursor = await _urls.FindAsync(url => url.ShortUrl == shortUrl);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task Delete(Url url)
        {
            await _urls.DeleteOneAsync(dbUrl => dbUrl.Id == url.Id).ConfigureAwait(false);
        }

        public async Task Save(Url url)
        {
            await _urls.InsertOneAsync(url).ConfigureAwait(false);
        }
    }
}
