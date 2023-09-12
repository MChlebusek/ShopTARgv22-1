using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;


namespace Shop.ApplicationServices.Services
{
    public class SpaceshipServices : ISpaceshipServices
    {
        private readonly ShopContext _context;

        public SpaceshipServices
            (
                ShopContext context
            )
        {
            _context = context;
        }

        public async Task<Spaceship> Create(SpaceshipDto dto)
        {

            Spaceship spaceship = new Spaceship();

            spaceship.Id = Guid.NewGuid();
            spaceship.Name = dto.Name;
            spaceship.Type = dto.Type;
            spaceship.Passengers = dto.Passengers;
            spaceship.EnginePower = dto.EnginePower;
            spaceship.Crew = dto.Crew;
            spaceship.Company = dto.Company;
            spaceship.CargoWeight = dto.CargoWeight;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifiedAt = DateTime.Now;

            await _context.Spaceships.AddAsync(spaceship);
            await _context.SaveChangesAsync();

            return spaceship;
        }
    }
}
