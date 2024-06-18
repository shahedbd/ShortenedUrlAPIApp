using Microsoft.EntityFrameworkCore;
using ShortenedUrlAPIApp.Data;
using ShortenedUrlAPIApp.Helper;

namespace ShortenedUrlAPIApp.Services
{
    public class UrlShorteningService(ApplicationDbContext dbContext)
    {
        private readonly Random _random = new();
        public async Task<string> GenerateUniqueCode()
        {
            while (true)
            {
                var _GenerateCode = GenerateCode(ShortLinkSettings.Length, ShortLinkSettings.Alphabet, _random);
                if (!await IsCodeUniqueAsync(_GenerateCode))
                {
                    return _GenerateCode;
                }
            }
        }
        private string GenerateCode(int length, string alphabet, Random random)
        {
            var codeChars = new char[length];
            int alphabetLength = alphabet.Length;

            for (var i = 0; i < length; i++)
            {
                var randomIndex = random.Next(alphabetLength);
                codeChars[i] = alphabet[randomIndex];
            }

            return new string(codeChars);
        }
        private async Task<bool> IsCodeUniqueAsync(string code)
        {
            return await dbContext.ShortenedUrls.AnyAsync(s => s.Code == code);
        }
    }
}
