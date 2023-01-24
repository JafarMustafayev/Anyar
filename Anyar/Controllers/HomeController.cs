using Anyar.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Anyar.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        

        public IActionResult Index()
        {

            List<Team> team = _dataContext.Teams.OrderBy(x => x.Order).Take(4).ToList();
            return View(team);
        }

       
    }
}