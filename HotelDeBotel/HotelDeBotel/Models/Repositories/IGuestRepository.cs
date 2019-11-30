using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDeBotel.Models.Repositories
{
    public interface IGuestRepository
    {
        GuestVM GetById(int id);
        ObservableCollection<GuestVM> GetAll();
        GuestVM Create(GuestVM item);
        GuestVM Update(GuestVM item);
        bool Delete(GuestVM item);
    }
}
