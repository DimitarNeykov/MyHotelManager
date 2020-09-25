namespace MyHotelManager.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using MyHotelManager.Data.Common.Repositories;
    using MyHotelManager.Data.Models;
    using MyHotelManager.Services.Mapping;

    public class StarsService : IStarsService
    {
        private readonly IDeletableEntityRepository<Stars> starsRepository;

        public StarsService(IDeletableEntityRepository<Stars> starsRepository)
        {
            this.starsRepository = starsRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var query = this.starsRepository.All();

            return query.To<T>().ToList();
        }
    }
}
