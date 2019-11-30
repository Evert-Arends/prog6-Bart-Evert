using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using HotelDeBotel.Models.Viewmodels;

namespace HotelDeBotel.Models.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private BotelContext context;

        public RoomRepository(BotelContext c)
        {
            context = c;
        }

        public RoomVM Create(RoomVM item)
        {
            var newItem = context.Rooms.Add(item.ToModel());
            context.SaveChanges();
            return new RoomVM(newItem);
        }

        public bool Delete(RoomVM item)
        {
            try
            {
                var r = GetById(item.Id);
                r.IsDeleted = true;
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public ObservableCollection<RoomVM> GetAll()
        {
            return new ObservableCollection<RoomVM>(context.Rooms.ToList().Where(r => r.IsDeleted == false).Select(r => new RoomVM(r)));
        }

        public RoomVM GetById(int id)
        {
            var room = new RoomVM(context.Rooms.ToList().Where(r => r.IsDeleted == false).FirstOrDefault(r => r.Id == id));
            return room;
        }

        public RoomVM Update(RoomVM item)
        {
            var result = GetById(item.Id);
            context.Entry(result.ToModel()).CurrentValues.SetValues(item.ToModel());
            context.SaveChanges();
            return result;
        }
    }
}