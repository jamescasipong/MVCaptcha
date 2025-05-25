using MVCaptcha.Data;
using MVCaptcha.Models.Entities;
using MVCaptcha.Models.Repositories.RepositoryBase;

namespace MVCaptcha.Models.Repositories.SessionRepository
{
    public class SessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        public SessionRepository(AppDataContext context) : base(context)
        {
        }

        public async Task CompleteSession(int sessionId, int score)
        {
            var session = await GetByIdAsync(sessionId);

            session.DateTimeEnded = DateTime.UtcNow;
            session.Score = score.ToString();

            await UpdateAsync(session);
        }

        public async Task<int> CreateSession(string difficulty)
        {
            var session = new Session
            {
                Difficulty = difficulty,
                Score = "0",
            };

            await AddAsync(session);

            return session.SessionId;
        }

        
    }
}
