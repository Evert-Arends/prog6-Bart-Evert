using HotelDeBotel.Models.Viewmodels;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace HotelDeBotel.Models.Repositories
{
    public class DummyDiscountRepository : IDiscountRepository
    {
        public double GetDiscount(ReservationVM item, int specificRoll = -1)
        {
            double discount = 0;
            discount += GetDiscountByWeekday(item.Date);
            discount += GetDiscountByOddWeek(item.Date);
            discount += GetDiscountByNumberOfRooms(5);
            discount += GetDiscountByGuestName(item.Guests);
            if (specificRoll == -1)
                discount *= GetDiscountMultiplierByDice();
            else
                discount *= specificRoll;
            discount = ReduceDiscountToSixty(discount);
            return discount;
        }

        public double GetDiscountByWeekday(DateTime date)
        {
            DayOfWeek weekday = date.DayOfWeek;
            if (weekday == DayOfWeek.Monday || weekday == DayOfWeek.Tuesday)
                return 15;
            else
                return 0;
        }

        public double GetDiscountByOddWeek(DateTime date)
        {
            int weekOfYear = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (weekOfYear % 2 != 0)
                return 3;
            else
                return 0;
        }

        public double GetDiscountByNumberOfRooms(double number)
        {
            return number;
        }

        public double GetDiscountByGuestName(ObservableCollection<GuestVM> guests)
        {
            if (guests != null && guests.Count != 0)
            {
                double additionalDiscount = 0;
                double highestValue = 0;
                foreach (GuestVM guest in guests)
                {
                    foreach (char c in "abcdefghijklmnopqrstuvwxyz")
                    {
                        if (guest.Name.ToLower().Contains(c))
                            additionalDiscount += 2;
                        else
                            break;
                    }
                    if (additionalDiscount > highestValue)
                    {
                        highestValue = additionalDiscount;
                        additionalDiscount = 0;
                    }
                }
                return highestValue;
            }
            else
                throw new ArgumentNullException();
        }

        public double GetDiscountMultiplierByDice()
        {
            return new Random().Next(1, 7);
        }

        public double ReduceDiscountToSixty(double discount)
        {
            return discount > 60 ? 60 : discount;
        }

        public double CalculateNewPrice(double price, double discount)
        {
            double factor = 1 - discount / 100;
            return price * factor;
        }
    }
}