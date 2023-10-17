using Microsoft.EntityFrameworkCore;
using Shop.Core.Domain;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Shop.ApplicationServices.Services
{
    public class RealEstatesServices : IRealEstatesServices
    {
        private readonly ShopContext _context;
        private readonly IFileServices _fileServices;

        public RealEstatesServices
            (
                ShopContext context,
                IFileServices fileServices
            )
        {
            _context = context;

        }

        public async Task<RealEstate> Create(RealEstateDto dto)
        {

            RealEstate RealEstate = new RealEstate();

            RealEstate.Id = Guid.NewGuid();
            RealEstate.Name = dto.Name;
            RealEstate.Type = dto.Type;
            RealEstate.Passengers = dto.Passengers;
            RealEstate.EnginePower = dto.EnginePower;
            RealEstate.Crew = dto.Crew;
            RealEstate.Company = dto.Company;
            RealEstate.CargoWeight = dto.CargoWeight;
            RealEstate.CreatedAt = DateTime.Now;
            RealEstate.ModifiedAt = DateTime.Now;
            
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, RealEstate);
            }



            await _context.RealEstates.AddAsync(RealEstate);
            await _context.SaveChangesAsync();

            return RealEstate;
        }


        public async Task<RealEstate> Update(RealEstateDto dto)
        {
            var domain = new RealEstate()
            {
                Id = dto.Id,
                Name = dto.Name,
                Type = dto.Type,
                Passengers = dto.Passengers,
                EnginePower = dto.EnginePower,
                Crew = dto.Crew,
                Company = dto.Company,
                CargoWeight = dto.CargoWeight,
                CreatedAt = dto.CreatedAt,
                ModifiedAt = DateTime.Now,
            };
            if (dto.Files != null)
            {
                _fileServices.UploadFilesToDatabase(dto, domain);
            }

            _context.RealEstates.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<RealEstate> Delete(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.RealEstates.Remove(result);
            await _context.SaveChangesAsync();

            return result;
        }


        public async Task<RealEstate> DetailsAsync(Guid id)
        {
            var result = await _context.RealEstates
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }

        public Task<RealEstate> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
