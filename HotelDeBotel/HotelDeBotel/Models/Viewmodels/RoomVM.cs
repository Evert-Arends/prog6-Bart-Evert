using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HotelDeBotel.Models.Viewmodels
{
    public class RoomVM
    {
        private Room _model;

        public RoomVM(Room r = null)
        {
            if (r != null)
            {
                _model = r;
            }
            else
            {
                _model = new Room();
            }
        }

        public RoomVM()
        {
            _model = new Room();

        }

        public int Id
        {
            get { return _model.Id; }
            set { _model.Id = value; }
        }

        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; }
        }
        [Range(2,5)]
        public int Size
        {
            get { return _model.Size; }
            set { _model.Size = value; }
        }

        [RegularExpression(@"^\d+([\,\.]{1}\d{1,2})?$", ErrorMessage = "Price can't have more than 2 decimal places.")]
        public double Price
        {
            get { return _model.Price; }
            set { _model.Price = value; }
        }

        public string Image
        {
            get { return _model.Image; }
            set { _model.Image = value; }
        }

        public ObservableCollection<ReservationVM> Reservations
        {
            get { return new ObservableCollection<ReservationVM>(_model.Reservations.ToList().Where(g => g.IsDeleted == false).Select(r => new ReservationVM(r))); }
            set { _model.Reservations = new ObservableCollection<Reservation>(value.ToList().Select(r => r.ToModel())); }
        }

        public bool IsDeleted
        {
            get { return _model.IsDeleted; }
            set { _model.IsDeleted = value; }
        }

        public Room ToModel()
        {
            return _model;
        }
    }
}