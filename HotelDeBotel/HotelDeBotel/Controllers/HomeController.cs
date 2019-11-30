using HotelDeBotel.Models.Repositories;
using System.Web.Mvc;
using System.Linq;
using HotelDeBotel.Models;
using HotelDeBotel.Models.Viewmodels;
using System.Collections.ObjectModel;

namespace HotelDeBotel.Controllers
{
    public class HomeController : Controller
    {
        private IRoomRepository _roomRepository { get; set; }
        public HomeController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }
        public ActionResult Index()
        {

            return View(_roomRepository.GetAll());
        }

        public ActionResult About()
        {
            ViewBag.Message = "2019 PROG6 Assessment assignment made by Evert Arends and Bart Koevoets";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult NewRoom()
        {
            ViewBag.Message = "Book A Room";

            return View();

        }
        public ActionResult EditRoom(int id)
        {
            ViewBag.Message = "Book A Room";

            return View();

        }
    }
}