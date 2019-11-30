using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelDeBotel.Models.Repositories
{
    public interface IDiscountRepository
    {
        double GetDiscount(ReservationVM item, int specificRoll);
        double GetDiscountByWeekday(DateTime date);
        double GetDiscountByOddWeek(DateTime date);
        double GetDiscountByNumberOfRooms(double number);
        double GetDiscountByGuestName(ObservableCollection<GuestVM> guests);
        double GetDiscountMultiplierByDice();
        double ReduceDiscountToSixty(double discount);
        double CalculateNewPrice(double price, double discount);
    }
}
