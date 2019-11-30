using HotelDeBotel.Models;
using HotelDeBotel.Models.Repositories;
using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelDeBotel.Controllers
{
    public class RoomController : Controller
    {
        private IRoomRepository _roomRepository { get; set; }
        
        public RoomController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        // GET: Room
        public ActionResult Index()
        {
            return View(_roomRepository.GetAll());
        }

        // GET: Room/Details/5
        public ActionResult Details(int id)
        {
            
            return View(_roomRepository.GetById(id));
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            RoomVM roomVM = new RoomVM();
            return View(roomVM);
        }

        // POST: Room/Create
        [HttpPost]
        public ActionResult Create(RoomVM newRoomVM)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(newRoomVM);
                }
                var a = _roomRepository.Create(newRoomVM);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: Room/Edit/5
        public ActionResult Edit(int id)
        {

            return View(_roomRepository.GetById(id));
        }

        // POST: Room/Edit/
        [HttpPost]
        public ActionResult Edit(RoomVM room)
        {
            try
            {
                _roomRepository.Update(room);
                return RedirectToAction("Index", "Room");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }

        // GET: Room/Delete/5
        public ActionResult Delete(int id)
        {

            return View(_roomRepository.GetById(id));
        }

        // POST: Room/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                RoomVM room = _roomRepository.GetById(id);
                _roomRepository.Delete(room);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
