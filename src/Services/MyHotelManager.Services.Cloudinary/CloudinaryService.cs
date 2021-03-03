namespace MyHotelManager.Services.CloudinaryManage
{
    using CloudinaryDotNet;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Account cloudinaryAccount;

        private readonly Cloudinary cloudinary;

        public CloudinaryService(string cloudName, string apiKey, string apiSecret)
        {
            this.cloudinaryAccount = new Account(cloudName, apiKey, apiSecret);
            this.cloudinary = new Cloudinary(this.cloudinaryAccount);
        }

        public string GetImgByName(string name)
        {
            return this.cloudinary.Api.UrlImgUp.BuildUrl(name).Insert(4, "s");
        }
    }
}
