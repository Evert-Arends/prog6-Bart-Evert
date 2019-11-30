using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelDeBotel.Models
{
    public partial class Reservation : BaseModel
    {
        public Reservation()
        {
            Guests = new ObservableCollection<Guest>();
        }

        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public DateTime Date { get; set; }
        public double Discount { get; set; }
        public virtual ObservableCollection<Guest> Guests { get; set; }
    }
}