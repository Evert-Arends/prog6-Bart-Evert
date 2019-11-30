using HotelDeBotel.Models;
using HotelDeBotel.Models.Repositories;
using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.ObjectModel;
using System.Web.Mvc;

namespace HotelDeBotel.Controllers
{
    public class ReservationController : Controller
    {
        private IReservationRepository _reservationRepository { get; }
        private IRoomRepository _roomRepository { get; }
        private IDiscountRepository _discountRepository { get; }

        public ReservationController(IReservationRepository reservationRepository, IRoomRepository roomRepository, IDiscountRepository discountRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
            _discountRepository = discountRepository;
        }

        // GET: Reservation
        public ActionResult Index()
        {
            ViewBag.Repo = _discountRepository;
            return View(_reservationRepository.GetAll());
        }

        // GET: Reservation/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Repo = _discountRepository;
            return View(_reservationRepository.GetById(id));
        }

        // GET: Reservation/Create
        public ActionResult Create(int id)
        {
            var reservation = new ReservationVM { Room = _roomRepository.GetById(id) };
            reservation.Step = 1;
            reservation.Date = DateTime.Now.Date;

            return View(_reservationRepository.Create(reservation));
        }

        [HttpPost]
        public ActionResult StepOne()
        {
            var reservation = _reservationRepository.GetTemp();
            reservation.AmountOfGuests = int.Parse(Request.Form.Get("AmountOfGuests"));
            reservation.Date = DateTime.Parse(Request.Form.Get("Date"));

            var listOfReservations = _reservationRepository.GetAllByRoomId(reservation.Room.Id);
            for (int x = 0; x < listOfReservations.Count; x++)
            {
                var res = listOfReservations[x];
                if (res.Date == reservation.Date && res.Id != reservation.Id)
                {
                    reservation.Step = 1;
                    ViewBag.ErrorMsgDate = "This room already has a booking on this date, please choose another";
                    return View("Create", reservation);
                }
            }

            if(reservation.AmountOfGuests > reservation.Room.Size)
            {
                reservation.Step = 1;
                ViewBag.ErrorMsgSize = "This room does not have enough beds for this amount of guests";
                return View("Create", reservation);
            }

            var listOfGuests = new ObservableCollection<GuestVM>();
            for (int i = 0; i < reservation.AmountOfGuests; i++)
            {
                listOfGuests.Add(new GuestVM());
            }
            reservation.Guests = listOfGuests;
            reservation.Step = 2;
            return View("Create", reservation);
        }

        [HttpPost]
        public ActionResult StepTwo()
        {
            var reservation = _reservationRepository.GetTemp();
            for (int i = 0; i < reservation.Guests.Count; i++)
            {
                var guest = reservation.Guests[i];
                guest.Name = Request.Form.Get("guestname-" + i);
                guest.Address = Request.Form.Get("guestaddress-" + i);
                guest.Email = Request.Form.Get("guestemail-" + i);
            }

            reservation.Discount = _discountRepository.GetDiscount(reservation, -1);
            reservation.FinalPrice = _discountRepository.CalculateNewPrice(reservation.Room.Price, reservation.Discount);

            reservation.Step = 3;
            return View("Create", reservation);
        }

        [HttpPost]
        public ActionResult StepThree()
        {
            var reservation = _reservationRepository.GetTemp();
            reservation.Step = 4;
            return View("Create", reservation);
        }

        [HttpPost]
        public ActionResult StepFour()
        {
            _reservationRepository.Save();
            return RedirectToAction("Index", "Home");
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_reservationRepository.GetById(id));
        }

        // POST: Reservation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ReservationVM reservation = _reservationRepository.GetById(id);
                _reservationRepository.Delete(reservation);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
