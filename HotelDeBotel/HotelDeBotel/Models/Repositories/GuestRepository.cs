using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using HotelDeBotel.Models.Viewmodels;

namespace HotelDeBotel.Models.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private BotelContext context;

        public GuestRepository(BotelContext c)
        {
            context = c;
        }

        public GuestVM Create(GuestVM item)
        {
            var newItem = context.Guests.Add(item.ToModel());
            return new GuestVM(newItem);
        }

        public bool Delete(GuestVM item)
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

        public ObservableCollection<GuestVM> GetAll()
        {
            return new ObservableCollection<GuestVM>(context.Guests.ToList().Where(u => u.IsDeleted == false).Select(u => new GuestVM(u)));
        }

        public GuestVM GetById(int id)
        {
            return new GuestVM(context.Guests.ToList().Where(u => u.IsDeleted == false).FirstOrDefault(u => u.Id == id));
        }

        public GuestVM Update(GuestVM item)
        {
            var result = GetById(item.Id);
            context.Entry(result).CurrentValues.SetValues(item);
            context.SaveChanges();
            return result;
        }
    }
}