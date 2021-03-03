namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class GendersService : IGendersService
    {
        private readonly IDeletableEntityRepository<Gender> genderRepository;

        public GendersService(IDeletableEntityRepository<Gender> genderRepository)
        {
            this.genderRepository = genderRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var gender = this.genderRepository
                .AllAsNoTracking()
                .To<T>()
                .ToList();

            return gender;
        }
    }
}
