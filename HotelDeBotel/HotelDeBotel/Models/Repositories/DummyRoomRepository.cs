using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using HotelDeBotel.Models.Viewmodels;

namespace HotelDeBotel.Models.Repositories
{
    public class DummyRoomRepository : IRoomRepository
    {
        public ObservableCollection<RoomVM> Rooms;
        public ObservableCollection<ReservationVM> Reservations;

        public DummyRoomRepository()
        {
          

            Reservations = new ObservableCollection<ReservationVM>
            {
                new ReservationVM
                {
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
                    Discount = 0
                },
                new ReservationVM
                {
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
                    Discount = 0
                }
            };

            Rooms = new ObservableCollection<RoomVM>
            {
                new RoomVM
                {
                    Id = 1,
                    Name = "Kamer 1",
                    Price = 600,
                    Size = 2,
                    Image = "https://i.imgur.com/Z6CioPg.png",
                    Reservations = this.Reservations
                },
                new RoomVM
                {
                    Id = 2,
                    Name = "Kamer 2",
                    Price = 800,
                    Size = 4,
                    Image = "https://i.imgur.com/Z6CioPg.png"
                }
            };
        }

        public RoomVM Create(RoomVM item)
        {
            Rooms.Add(item);
            return item;
        }

        public bool Delete(RoomVM item)
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

        public ObservableCollection<RoomVM> GetAll()
        {
            return new ObservableCollection<RoomVM>(Rooms.Where(r => r.IsDeleted == false));
        }

        public RoomVM GetById(int id)
        {
            return Rooms.ToList().Where(r => r.IsDeleted == false).FirstOrDefault(r => r.Id == id);
        }

        public RoomVM Update(RoomVM item)
        {
            var result = GetById(item.Id);
            result.Name = item.Name;
            result.Price = item.Price;
            result.Reservations = item.Reservations;
            result.Size = item.Size;
            result.Image = item.Image;
            return result;
        }
    }
}