using Microsoft.EntityFrameworkCore;
using TiacApp.Infrastructure;
using TiacApp.Models;

namespace TiacApp.Repository
{
    public class SocialMediaRepository
    {
        private readonly AppDbContext _context;

        public SocialMediaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SocialMedia>> GetAllSocialMediasAsync()
        {
            return await _context.SocialMedias.ToListAsync();
        }

        public async Task<SocialMedia> AddSocialMediaAsync(SocialMedia socialMedia)
        {
            _context.SocialMedias.Add(socialMedia);
            await _context.SaveChangesAsync();
            return socialMedia;
        }

    }
}
