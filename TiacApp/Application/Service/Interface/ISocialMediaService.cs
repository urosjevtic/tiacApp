namespace TiacApp.Application.Service.Interface
{
    public interface ISocialMediaService
    {
        Task<object> GetSocialMedias();
        Task<object> AddSocialMedia(object socialMedia);
    }
}
