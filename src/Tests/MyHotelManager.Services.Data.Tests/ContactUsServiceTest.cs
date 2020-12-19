namespace MyHotelManager.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Moq;
    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Data.Repositories;
    using Xunit;

    public class ContactUsServiceTest
    {
        [Fact]
        public async Task TestContactForm()
        {
            var contactUsList = new List<ContactForm>();
            var mockContactUsRepo = new Mock<IRepository<ContactForm>>();
            mockContactUsRepo.Setup(x => x.All()).Returns(contactUsList.AsQueryable());
            mockContactUsRepo.Setup(x => x.AddAsync(It.IsAny<ContactForm>())).Callback(
                (ContactForm contactUs) => contactUsList.Add(contactUs));

            var service = new ContactUsService(mockContactUsRepo.Object);

            await service.CreateAsync("Dimitar Neykov", "dimitarneikov@gmail.com", "Test", "Test Content in contact form!");

            Assert.Single(contactUsList);
        }
    }
}
