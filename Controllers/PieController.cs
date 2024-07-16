using Microsoft.AspNetCore.Mvc;
using PieShop.Models;
using PieShop.ViewModels;

namespace PieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
      //  private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
           
        }

        public IActionResult List()
        {
            PieListViewModel piesListViewModdel = new PieListViewModel(_pieRepository.AllPies, "All Pies");
            return View(piesListViewModdel);    
        }
        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie is null)
            {
                return NotFound();
            }
            return View(pie);
        }
        public IActionResult Search()
        {
            return View();
        }
    }
}
