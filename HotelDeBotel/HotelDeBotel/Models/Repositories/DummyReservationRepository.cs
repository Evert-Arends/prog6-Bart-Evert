using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HotelDeBotel.Models.Repositories
{
    public class DummyReservationRepository : IReservationRepository
    {
        public ObservableCollection<ReservationVM> Reservations;
        private ReservationVM tempReservation;

        public DummyReservationRepository()
        {
            Reservations = new ObservableCollection<ReservationVM>
            {
                new ReservationVM
                {
                    Id = 0,
                    Date = new DateTime(2019, 1, 25),
                    Guests = new ObservableCollection<GuestVM>
                    {
                        new GuestVM
                        {
                            Name = "Bart Koevoets",
                            Address = "Heubergerstraat 116",
                            Email = "yzig_koevoets@hotmail.com"
                        },
                        new GuestVM
                        {
                            Name = "Evert Arends",
                            Address = "Adhswefsd 3457",
                            Email = "bermdingetje@hotmail.com"
                        }
                    },
                    Discount = 50,
                    Room = new RoomVM
                    {
                        Name = "Testkamer 1",
                        Price = 500,
                        Size = 2,
                        Image = "testImage1"
                    }
                },
                new ReservationVM
                {
                    Id = 1,
                    Date = new DateTime(2019, 3, 25),
                    Guests = new ObservableCollection<GuestVM>
                    {
                        new GuestVM
                        {
                            Name = "Abcdefghjk",
                            Address = "Heubergerstraat 116",
                            Email = "yzig_koevoets@hotmail.com"
                        },
                        new GuestVM
                        {
                            Name = "Avert Erends",
                            Address = "Adhswefsd 3457",
                            Email = "bermdingetje@hotmail.com"
                        }
                    },
                    Discount = 0,
                    Room = new RoomVM
                    {
                        Name = "Testkamer 2",
                        Price = 500,
                        Size = 2,
                        Image = "testImage1"
                    }
                }
            };
        }

        public ReservationVM Create(ReservationVM item)
        {
            int lastId = Reservations.Last().Id;
            item.Id = lastId + 1;
            tempReservation = item;
            return item;
        }

        public bool Delete(ReservationVM item)
        {
            try
            {
                var r = GetById(item.Id);
                r.IsDeleted = true;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ObservableCollection<ReservationVM> GetAll()
        {
            return new ObservableCollection<ReservationVM>(Reservations.Where(r => r.IsDeleted == false));
        }

        public ReservationVM GetById(int id)
        {
            return Reservations.ToList().Where(r => r.IsDeleted == false).FirstOrDefault(r => r.Id == id);
        }

        public ReservationVM GetTemp()
        {
            return tempReservation;
        }

        public ObservableCollection<ReservationVM> GetAllByRoomId(int roomId)
        {
            return new ObservableCollection<ReservationVM>(
                Reservations.ToList()
                .Where(r => r.IsDeleted == false)
                .Where(r => r.Room.Id == roomId)
                .Select(r => r));
        }

        public bool Save()
        {
            Reservations.Add(tempReservation);
            return true;
        }

        public ReservationVM Update(ReservationVM item)
        {
            var result = GetById(item.Id);
            result.Date = item.Date;
            result.Room = item.Room;
            result.Guests = item.Guests;
            result.Discount = item.Discount;
            return result;
        }
    }
}