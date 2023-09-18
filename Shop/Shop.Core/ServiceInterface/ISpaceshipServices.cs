using Shop.Core.Domain;
using Shop.Core.Dto;


namespace Shop.Core.ServiceInterface
{
    public interface ISpaceshipServices
    {
        Task<Spaceship> Create(SpaceshipDto dto);
        Task<Spaceship> GetAsync(Guid id);
    }
}
