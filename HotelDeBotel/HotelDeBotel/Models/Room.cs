using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelDeBotel.Models
{
    public partial class Room : BaseModel
    {
        public Room()
        {
            Reservations = new ObservableCollection<Reservation>();
        }
        public string Name { get; set; }
        public int Size { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public virtual ObservableCollection<Reservation> Reservations { get; set; }
    }
}