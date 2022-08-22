using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TesteLuta.UI.App_Start;
using TesteLuta.UI.Models;
using TesteLuta.UI.Services;
using TesteLuta.UI.Services.Interfaces;

namespace TesteLuta.UI.Controllers
{
    public class HomeController: Controller
    {
        public ActionResult Index()
        {
            return View(GetFighters());
        }

        public List<Fighter> GetFighters()
        {
            try
            {
                ApiAccess fighters = new ApiAccess();
                return fighters.AllFighter;
            }
            catch (Exception)
            {
                return new List<Fighter>();
            }
        }

        [HttpGet]
        public JsonResult Fights(string ids)
        {
            try
            {
                Tournament tournament = new Tournament(Array.ConvertAll(ids.Split(','), a => int.Parse(a)));

                //Start Fights
                tournament.QuarterFinals();
                tournament.SemiFinals();
                return Json(new { success = true, data = tournament.Final() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}