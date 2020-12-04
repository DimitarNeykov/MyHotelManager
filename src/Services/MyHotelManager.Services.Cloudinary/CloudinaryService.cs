namespace MyHotelManager.Services.CloudinaryManage
{
    using CloudinaryDotNet;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Account cloudinaryAccount;

        private readonly Cloudinary cloudinary;

        public CloudinaryService(string cloudName, string apiKey, string apiSecret)
        {
            cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
            cloudinary = new Cloudinary(cloudinaryAccount);
        }

        public string GetImgByName(string name)
        {
            return cloudinary.Api.UrlImgUp.BuildUrl(name);
        }
    }
}
