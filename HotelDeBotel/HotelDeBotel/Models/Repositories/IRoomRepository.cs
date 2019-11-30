using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDeBotel.Models.Repositories
{
    public interface IRoomRepository
    {
        RoomVM GetById(int id);
        ObservableCollection<RoomVM> GetAll();
        RoomVM Create(RoomVM item);
        RoomVM Update(RoomVM item);
        bool Delete(RoomVM item);
    }
}
