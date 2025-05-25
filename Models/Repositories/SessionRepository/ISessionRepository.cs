using MVCaptcha.Models.Entities;
using MVCaptcha.Models.Repositories.RepositoryBase;

namespace MVCaptcha.Models.Repositories.SessionRepository
{
    public interface ISessionRepository: IRepositoryBase<Session>
    {
        Task CompleteSession(int sessionId, int score);
        Task<int> CreateSession(string difficulty);
    }
}
