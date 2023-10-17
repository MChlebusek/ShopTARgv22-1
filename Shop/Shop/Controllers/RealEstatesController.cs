using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Shop.ApplicationServices.Services;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models;
using Shop.Models.RealEstates;
using System.Reflection;

namespace Shop.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly ShopContext _context;
        private readonly IRealEstatesServices _RealEstatesServices;
        private readonly IFileServices _fileServices;
        public RealEstatesController
            (
                ShopContext context,
                IRealEstatesServices realEstatesServices,
                IFileServices fileServices
            )
        {
            _context = context;
            _RealEstatesServices = realEstatesServices;
            _fileServices = fileServices;
        }


        public IActionResult Index()
        {
            var result = _context.RealEstates
                .Select(x => new RealEstateIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Type = x.Type,
                    EnginePower = x.EnginePower,
                    Passengers = x.Passengers
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            RealEstateCreateUpdateViewModel RealEstate = new RealEstateCreateUpdateViewModel();

            return View("CreateUpdate", RealEstate);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Passengers = vm.Passengers,
                EnginePower = vm.EnginePower,
                Crew = vm.Crew,
                Company = vm.Company,
                CargoWeight = vm.CargoWeight,
                Files = vm.Files,
                Image = vm.Image.Select(x=> new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    RealEstateId = x.RealEstateId
                }).ToArray()

            };

            var result = await _RealEstatesServices.Create(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var RealEstate = await _RealEstatesServices.GetAsync(id);

            if (RealEstate == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    RealEstateId = y.RealEstateId,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/git;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();


            var vm = new RealEstateDetailsViewModel();

            vm.Id = RealEstate.Id;
            vm.Name = RealEstate.Name;
            vm.Type = RealEstate.Type;
            vm.Passengers = RealEstate.Passengers;
            vm.EnginePower = RealEstate.EnginePower;
            vm.Crew = RealEstate.Crew;
            vm.Company = RealEstate.Company;
            vm.CargoWeight = RealEstate.CargoWeight;
            vm.CreatedAt = RealEstate.CreatedAt;
            vm.ModifiedAt = RealEstate.ModifiedAt;
            vm.Image.AddRange(photos);


            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var RealEstate = await _RealEstatesServices.GetAsync(id);

            if (RealEstate == null)
            {
                return NotFound();
            }

            var photos = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    RealEstateId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64, {0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();

            var vm = new RealEstateCreateUpdateViewModel();

            vm.Id = RealEstate.Id;
            vm.Name = RealEstate.Name;
            vm.Type = RealEstate.Type;
            vm.Passengers = RealEstate.Passengers;
            vm.EnginePower = RealEstate.EnginePower;
            vm.Crew = RealEstate.Crew;
            vm.Company = RealEstate.Company;
            vm.CargoWeight = RealEstate.CargoWeight;
            vm.CreatedAt = RealEstate.CreatedAt;
            vm.ModifiedAt = RealEstate.ModifiedAt;
            vm.Image.AddRange(photos);

            return View("CreateUpdate", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RealEstateCreateUpdateViewModel vm)
        {
            var dto = new RealEstateDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                Type = vm.Type,
                Passengers = vm.Passengers,
                EnginePower = vm.EnginePower,
                Crew = vm.Crew,
                Company = vm.Company,
                CargoWeight = vm.CargoWeight,
                CreatedAt = vm.CreatedAt,
                ModifiedAt = vm.ModifiedAt,
                Files = vm.Files,
                Image = vm.Image.Select(x => new FileToDatabaseDto
                {
                    Id = x.ImageId,
                    ImageData = x.ImageData,
                    ImageTitle = x.ImageTitle,
                    RealEstateId = x.RealEstateId,
                }).ToArray()
            };

            var result = await _RealEstatesServices.Update(dto);

            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index), vm);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var RealEstate = await _RealEstatesServices.GetAsync(id);

            if (RealEstate == null)
            {
                return NotFound();
            }
            var photos = await _context.FileToDatabases
                .Where(x => x.RealEstateId == id)
                .Select(y => new ImageToDatabaseViewModel
                {
                    RealEstateId = y.Id,
                    ImageId = y.Id,
                    ImageData = y.ImageData,
                    ImageTitle = y.ImageTitle,
                    Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(y.ImageData))
                }).ToArrayAsync();



            var vm = new RealEstateDeleteViewModel();

            vm.Id = RealEstate.Id;
            vm.Name = RealEstate.Name;
            vm.Type = RealEstate.Type;
            vm.Passengers = RealEstate.Passengers;
            vm.EnginePower = RealEstate.EnginePower;
            vm.Crew = RealEstate.Crew;
            vm.Company = RealEstate.Company;
            vm.CargoWeight = RealEstate.CargoWeight;
            vm.CreatedAt = RealEstate.CreatedAt;
            vm.ModifiedAt = RealEstate.ModifiedAt;
            vm.ImageToDatabase.AddRange(photos);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var RealEstateId = await _RealEstatesServices.Delete(id);

            if (RealEstateId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> RemoveImage(ImageToDatabaseViewModel file)
        {
            var dto = new FileToDatabaseDto()
            {
                Id = file.ImageId
            };

            var image = await _fileServices.RemoveImageFromDatabase(dto);

            if (image == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
