using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelDeBotel.Models.Viewmodels
{
    public class ReservationVM
    {
        private Reservation _model;

        public ReservationVM(Reservation r = null)
        {
            if (r != null)
                _model = r;
            else
                _model = new Reservation();
        }

        public int Id {
            get { return _model.Id; } set { _model.Id = value;  }
        }

        public RoomVM Room {
            get { return new RoomVM(_model.Room); }
            set { _model.Room = value.ToModel(); }
        }
        
        public DateTime Date {
            get { return _model.Date; }
            set { _model.Date = value; }
        }

        public string DateString
        {
            get { return Date.Date.ToShortDateString(); }
            set { Date = DateTime.Parse(value);}
        }

        public double Discount {
            get { return _model.Discount; }
            set { _model.Discount = value; }
        }

        public string DiscountPercentage
        {
            get { return Discount + "%"; }
        }

        public ObservableCollection<GuestVM> Guests
        {
            get { return new ObservableCollection<GuestVM> ( _model.Guests.ToList().Where(g => g.IsDeleted == false).Select(g => new GuestVM(g)) ); }
            set { _model.Guests = new ObservableCollection<Guest>(value.ToList().Select(g => g.ToModel())); }
        }

        [Range(2,5)]
        public int AmountOfGuests { get; set; }

        public double FinalPrice { get; set; }
        public bool Completed { get; set; }

        public int Step { get; set; }

        public bool IsDeleted
        {
            get { return _model.IsDeleted; }
            set { _model.IsDeleted = value; }
        }

        public Reservation ToModel()
        {
            return _model;
        }
    }
}