using Anyar.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Anyar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _evn;

        public TeamController(DataContext dataContext, IWebHostEnvironment evn)
        {
            _dataContext = dataContext;
            _evn = evn;
        }

        public IActionResult Index()
        {
            List<Team> team = _dataContext.Teams.ToList();


            return View(team);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Team team)
        {
            if (team == null) { return View("Error"); }
            if (!ModelState.IsValid || team.ImageFile is null) { return View(team); }



            if (FileManager.IsImage(team.ImageFile))
            {
                if (FileManager.CheckFileSize(team.ImageFile))
                {
                    team.Image = FileManager.CheckAndEditName(team.ImageFile);

                    string path = Path.Combine(_evn.WebRootPath, "Upload/Teams", team.Image);

                    FileManager.SaveFile(team.ImageFile, path);
                    _dataContext.Teams.Add(team);
                    _dataContext.SaveChanges();

                    return RedirectToAction("Index");

                }
                else { ModelState.AddModelError("ImageFile", "The maximum file size can be 3 MB."); }
            }
            else { ModelState.AddModelError("ImageFile", "Can only be PNG and JPEG format"); }

            return View();
        }

        public IActionResult Update(int id)
        {
            Team oldTeam = _dataContext.Teams.FirstOrDefault(x => x.Id == id);

            if (oldTeam is null) return View("Error");

            return View(oldTeam);
        }

        [HttpPost]
        public IActionResult Update(Team team)
        {
            Team oldTeam = _dataContext.Teams.FirstOrDefault(x => x.Id == team.Id);
            if (oldTeam is null) return View("Error");

            if (team.ImageFile is not null)
            {
                if (FileManager.IsImage(team.ImageFile))
                {
                    if (FileManager.CheckFileSize(team.ImageFile))
                    {
                        string path = Path.Combine(_evn.WebRootPath, "Upload/Teams");


                        FileManager.Delete(Path.Combine(path, oldTeam.Image));

                        team.Image = FileManager.CheckAndEditName(team.ImageFile);
                        FileManager.SaveFile(team.ImageFile, Path.Combine(path, team.Image));

                        oldTeam.Image = team.Image;

                    }
                    else { ModelState.AddModelError("ImageFile", "The maximum file size can be 3 MB."); }
                }
                else { ModelState.AddModelError("ImageFile", "Can only be PNG and JPEG format"); }
            }

            oldTeam.Fullname = team.Fullname;
            oldTeam.Description = team.Description;
            oldTeam.Position = team.Position;
            oldTeam.FacebookUrl = team.FacebookUrl;
            oldTeam.TwitterUrl = team.TwitterUrl;
            oldTeam.LinkedinUrl= team.LinkedinUrl;
            oldTeam.InstagramUrl= team.InstagramUrl;
            oldTeam.Order = team.Order;


            _dataContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Team team = _dataContext.Teams.FirstOrDefault(x => x.Id == id);
            if (team is null) return NotFound();

            string path = Path.Combine(_evn.WebRootPath, "Upload/Teams", team.Image);
            FileManager.Delete(path);
            _dataContext.Teams.Remove(team);
            _dataContext.SaveChanges();
            return Ok();
        }




    }
}
