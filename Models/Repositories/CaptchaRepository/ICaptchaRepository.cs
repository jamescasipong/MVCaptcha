using MVCaptcha.Models.Entities;
using MVCaptcha.Models.Repositories.RepositoryBase;

namespace MVCaptcha.Models.Repositories.CaptchaRepository
{
    public interface ICaptchaRepository: IRepositoryBase<Captcha>
    {
        Task<IEnumerable<Captcha>> GetByDifficulty(string difficulty, int? count = null);
    }
}
