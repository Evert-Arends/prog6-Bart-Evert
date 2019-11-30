using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDeBotel.Models.Repositories
{
    public interface IReservationRepository
    {
        ReservationVM GetById(int id);
        ReservationVM GetTemp();
        ObservableCollection<ReservationVM> GetAll();
        ObservableCollection<ReservationVM> GetAllByRoomId(int roomId);
        ReservationVM Create(ReservationVM item);
        bool CheckDate(ReservationVM reservation);
        ReservationVM Update(ReservationVM item);
        bool Delete(ReservationVM item);
        bool Save();
    }
}
