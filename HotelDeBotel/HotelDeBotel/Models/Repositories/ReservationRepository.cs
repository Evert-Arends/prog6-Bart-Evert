using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace HotelDeBotel.Models.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private BotelContext context;
        private ReservationVM tempReservation;

        public ReservationRepository(BotelContext c)
        {
            context = c;
        }

        public ReservationVM Create(ReservationVM item)
        {
            //var newItem = context.Reservations.Add(item.ToModel());
            //context.SaveChanges();
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
            return new ObservableCollection<ReservationVM>(context.Reservations.ToList().Where(r => r.IsDeleted == false).Select(r => new ReservationVM(r)));
        }

        public ObservableCollection<ReservationVM> GetAllByRoomId(int roomId)
        {
            return new ObservableCollection<ReservationVM>(
                context.Reservations.ToList()
                .Where(r => r.IsDeleted == false)
                .Where(r => r.Room.Id == roomId)
                .Select(r => new ReservationVM(r)));
        }

        public ReservationVM GetById(int id)
        {
            return new ReservationVM(context.Reservations.ToList().Where(r => r.IsDeleted == false).FirstOrDefault(r => r.Id == id));
        }

        public ReservationVM GetTemp()
        {
            return tempReservation;
        }

        public bool Save()
        {
            try
            {
                context.Reservations.Add(tempReservation.ToModel());
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ReservationVM Update(ReservationVM item)
        {
            var result = GetById(item.Id);
            context.Entry(result).CurrentValues.SetValues(item);
            context.SaveChanges();
            return result;
        }

        public bool CheckDate(ReservationVM reservation)
        {
            var listOfReservations = GetAllByRoomId(reservation.Room.Id);
            for (int x = 0; x < listOfReservations.Count; x++)
            {
                var res = listOfReservations[x];
                if (res.Date == reservation.Date && res.Id != reservation.Id)
                {
                    return false;
                }
            }
            return true;
        }
    }
}