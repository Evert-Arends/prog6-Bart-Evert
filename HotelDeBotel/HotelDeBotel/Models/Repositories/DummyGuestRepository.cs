using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using HotelDeBotel.Models.Viewmodels;

namespace HotelDeBotel.Models.Repositories
{
    public class DummyGuestRepository : IGuestRepository
    {
        public ObservableCollection<GuestVM> Users;

        public DummyGuestRepository()
        {
            Users = new ObservableCollection<GuestVM>
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
            };
        }

        public GuestVM Create(GuestVM item)
        {
            Users.Add(item);
            return item;
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
            return new ObservableCollection<GuestVM>(Users.Where(r => r.IsDeleted == false));
        }

        public GuestVM GetById(int id)
        {
            return Users.ToList().Where(u => u.IsDeleted == false).FirstOrDefault(u => u.Id == id);
        }

        public GuestVM Update(GuestVM item)
        {
            var result = GetById(item.Id);
            result.Name = item.Name;
            result.Address = item.Address;
            result.Email = item.Email;
            return result;
        }
    }
}