using Microsoft.EntityFrameworkCore;
using MVCaptcha.Models.Entities;

namespace MVCaptcha.Data
{
    public class AppDataContext: DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options)
        {
        }

        public DbSet<Captcha> Captchas { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Captcha>().HasData(
                new Captcha { Id = 1, CaptchaName = "easy1.png", CaptchaValue = "4321", ImageUrl = "/image/captcha/easy1.png", Level = "E" },
                new Captcha { Id = 2, CaptchaName = "easy2.png", CaptchaValue = "45687", ImageUrl = "/image/captcha/easy2.png", Level = "E" },
                new Captcha { Id = 3, CaptchaName = "easy3.png", CaptchaValue = "965774123", ImageUrl = "/image/captcha/easy3.png", Level = "E" },
                new Captcha { Id = 4, CaptchaName = "normal1.png", CaptchaValue = "sPdY", ImageUrl = "/image/captcha/normal1.png", Level = "N" },
                new Captcha { Id = 5, CaptchaName = "normal2.png", CaptchaValue = "cRse", ImageUrl = "/image/captcha/normal2.png", Level = "N" },
                new Captcha { Id = 6, CaptchaName = "normal3.png", CaptchaValue = "opMuMI", ImageUrl = "/image/captcha/normal3.png", Level = "N" },
                new Captcha { Id = 7, CaptchaName = "hard1.png", CaptchaValue = "1ess2", ImageUrl = "/image/captcha/hard1.png", Level = "H" },
                new Captcha { Id = 8, CaptchaName = "hard2.png", CaptchaValue = "2wP34", ImageUrl = "/image/captcha/hard2.png", Level = "H" },
                new Captcha { Id = 9, CaptchaName = "hard3.png", CaptchaValue = "Lz00Oda", ImageUrl = "/image/captcha/hard3.png", Level = "H" }
            );
        }
    }
}
