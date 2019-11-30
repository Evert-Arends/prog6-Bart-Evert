using System.ComponentModel.DataAnnotations;

namespace HotelDeBotel.Models.Viewmodels
{
    public class GuestVM
    {
        private Guest _model;

        public GuestVM(Guest u = null)
        {
            if (u != null)
                _model = u;
            else
                _model = new Guest();
        }

        public int Id
        {
            get { return _model.Id; }
        }

        [Required]
        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; }
        }

        [Required]
        public string Address
        {
            get { return _model.Address; }
            set { _model.Address = value; }
        }

        [EmailAddress]
        public string Email {
            get { return _model.Email; }
            set { _model.Email = value; }
        }
        public bool IsDeleted
        {
            get { return _model.IsDeleted; }
            set { _model.IsDeleted = value; }
        }

        public Guest ToModel()
        {
            return _model;
        }
    }
}