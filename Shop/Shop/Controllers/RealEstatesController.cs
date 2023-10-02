using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using Shop.ApplicationServices.Services;
using Shop.Core.Dto;
using Shop.Core.ServiceInterface;
using Shop.Data;
using Shop.Models;
using Shop.Models.RealEstates;


namespace Shop.Controllers
{
    public class RealEstatesController : Controller
    {
        private readonly ShopContext _context;
        private readonly IRealEstatesServices _RealEstatesServices;

        public RealEstatesController
            (
                ShopContext context,
                IRealEstatesServices RealEstatesServices
            )
        {
            _context = context;
            _RealEstatesServices = RealEstatesServices;
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
    }
}
