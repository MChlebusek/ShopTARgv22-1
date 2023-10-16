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

namespace Shop.ApplicationServices.Services
{
    public class KinderGartenServices : IKindergartenServices
    {
        private readonly ShopContext _context;
        private readonly IFileServices _fileServices;

        public KinderGartenServices
            (
                ShopContext context,
                IFileServices fileServices
            )
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<KinderGarten> Create(KinderGartenDto dto)
        {
            KinderGarten kindergarten = new KinderGarten();

            kindergarten.Id = Guid.NewGuid();
            kindergarten.GroupName = dto.GroupName;
            kindergarten.ChildrenCount = dto.ChildrenCount;
            kindergarten.KindergartenName = dto.KindergartenName;
            kindergarten.Teacher = dto.Teacher;

            kindergarten.CreatedAt = DateTime.Now;
            kindergarten.UpdatedAt = DateTime.Now;
            //_fileServices.FilesToApi(dto, kindergarten);


            //if (dto.Files != null)
            //{
            //    _fileServices.UploadFilesToDatabase(dto, kindergarten);

            //}

            await _context.KinderGartens.AddAsync(kindergarten);
            await _context.SaveChangesAsync();



            return kindergarten;
        }
        public async Task<KinderGarten> Update(KinderGartenDto dto)
        {
            var domain = new KinderGarten()
            {
                Id = dto.Id,
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName,
                Teacher = dto.Teacher,


                CreatedAt = dto.CreatedAt,
                UpdatedAt = DateTime.Now,



            };

            _context.KinderGartens.Update(domain);
            await _context.SaveChangesAsync();


            return domain;
        }

        public async Task<KinderGarten> Delete(Guid id)
        {
            var kindergartenId = await _context.KinderGartens
                .FirstOrDefaultAsync(x => x.Id == id);

            _context.KinderGartens.Remove(kindergartenId);
            await _context.SaveChangesAsync();

            return kindergartenId;
        }



        public async Task<KinderGarten> GetAsync(Guid id)
        {
            var result = await _context.KinderGartens
                .FirstOrDefaultAsync(x => x.Id == id);

            return result;
        }
    }

}
