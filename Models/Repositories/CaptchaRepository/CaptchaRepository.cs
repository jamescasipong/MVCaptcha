using Microsoft.Extensions.Options;
using MVCaptcha.Configs;
using MVCaptcha.Data;
using MVCaptcha.Models.Entities;
using MVCaptcha.Models.Repositories.RepositoryBase;

namespace MVCaptcha.Models.Repositories.CaptchaRepository
{
    public class CaptchaRepository: RepositoryBase<Captcha>, ICaptchaRepository
    {
        private readonly CaptchaSettings _captchaSettings;
        public CaptchaRepository(AppDataContext _context, IOptions<CaptchaSettings> captchaSettings) : base(_context)
        {
            _captchaSettings = captchaSettings.Value;
        }

        public async Task<IEnumerable<Captcha>> GetByDifficulty(string difficulty, int? count = null)
        {
            return await GetAsync(a => a.Level == difficulty, count ?? _captchaSettings.CaptchaCount);
        }
    }
}
