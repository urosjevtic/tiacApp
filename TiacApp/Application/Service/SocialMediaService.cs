using TiacApp.Application.Service.Interface;
using TiacApp.Repository;
using TiacApp.Models;

namespace TiacApp.Application.Service
{
    public class SocialMediaService : ISocialMediaService
    {
        private readonly SocialMediaRepository _socialMediaRepository;

        public SocialMediaService(SocialMediaRepository socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        public async Task<object> AddSocialMedia(object socialMedia)
        {
            return await Task.FromResult(_socialMediaRepository.AddSocialMediaAsync((SocialMedia)socialMedia));
        }

        public Task<object> GetSocialMedias()
        {
            throw new NotImplementedException();
        }
    }
}
