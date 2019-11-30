using HotelDeBotel.Models.Repositories;
using HotelDeBotel.Models.Viewmodels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.ObjectModel;

namespace HotelDeBotel.Tests.Repositories
{
    [TestClass]
    public class DiscountTests
    {
        [TestMethod]
        public void WeekdayGivesDiscount()
        {
            //Assert that the method GetDiscountByWeekday returns a value of 15 when the inserted date is a monday or a tuesday
            var repo = new DummyDiscountRepository();
            double result1 = repo.GetDiscountByWeekday(new DateTime(2019, 1, 7));
            double result2 = repo.GetDiscountByWeekday(new DateTime(2019, 1, 8));
            double result3 = repo.GetDiscountByWeekday(new DateTime(2019, 1, 9));

            Assert.AreEqual(15, result1);
            Assert.AreEqual(15, result2);
            Assert.AreEqual(0, result3);
        }

        [TestMethod]
        public void OddWeekGivesDiscount()
        {
            //Assert that the method GetDiscountByOddWeek returns a value of 3 when the inserted date is on an odd numbered week
            var repo = new DummyDiscountRepository();
            double result1 = repo.GetDiscountByOddWeek(new DateTime(2019, 1, 4));
            double result2 = repo.GetDiscountByOddWeek(new DateTime(2019, 1, 11));

            Assert.AreEqual(3, result1);
            Assert.AreEqual(0, result2);
        }

        [TestMethod]
        public void NumberOfRoomsEqualsDiscount()
        {
            //Assert that the method GetDiscountByNumbersOfRooms returns a discount equal to the number inserted
            var repo = new DummyDiscountRepository();
            int input = new Random().Next(1, 11);
            double result1 = repo.GetDiscountByNumberOfRooms(input);

            Assert.AreEqual(input, result1);
        }

        [TestMethod]
        public void LettersInGuestNameGrantsDiscount()
        {
            //Assert that the method GetDiscountByGuestName returns a discount based on the letters of the inserted guest names
            //For each guest, assign a value of 2 to each unique letter in alphabetic, uninterrupted order
            //Return the discount for the highest value name only
            var repo = new DummyDiscountRepository();

            var guests1 = new ObservableCollection<GuestVM>
            {
                new GuestVM{ Name = "Bart Koevoets" },
                new GuestVM{ Name = "Evert Arends" }
            };

            var guests2 = new ObservableCollection<GuestVM> { new GuestVM { Name = "Abcdfgh" } };

            var guests3 = new ObservableCollection<GuestVM> { new GuestVM { Name = "AbcdEfgh" } };

            var guests4 = new ObservableCollection<GuestVM>();

            double result1 = repo.GetDiscountByGuestName(guests1);
            double result2 = repo.GetDiscountByGuestName(guests2);
            double result3 = repo.GetDiscountByGuestName(guests3);

            Assert.AreEqual(4, result1);
            Assert.AreEqual(8, result2);
            Assert.AreEqual(16, result3);
            Assert.ThrowsException<ArgumentNullException>(() => repo.GetDiscountByGuestName(null));
            Assert.ThrowsException<ArgumentNullException>(() => repo.GetDiscountByGuestName(guests4));
        }

        [TestMethod]
        public void DiceRollBetweenOneAndSix()
        {
            //Assert that the method GetDiscountMultiplierByDice only returns values between one and six in one hundred attempts
            var repo = new DummyDiscountRepository();

            for (int i = 0; i < 100; i++)
            {
                double result = repo.GetDiscountMultiplierByDice();

                Assert.IsTrue(result >= 1 && result <= 6);
            }
        }

        [TestMethod]
        public void ReduceToSixty()
        {
            //Assert that the method ReduceDiscountToSixty returns the inserted value if it is lower than or equal to sixty,
            //and sixty if it is higher
            var repo = new DummyDiscountRepository();

            double result1 = repo.ReduceDiscountToSixty(120);
            double result2 = repo.ReduceDiscountToSixty(45);
            double result3 = repo.ReduceDiscountToSixty(60);

            Assert.AreEqual(60, result1);
            Assert.AreEqual(45, result2);
            Assert.AreEqual(60, result3);
        }

        [TestMethod]
        public void FullDiscountMethod()
        {
            //Assert that the entire method GetDiscount returns the correct discount for a given reservation
            var reservationRepo = new DummyReservationRepository();
            var discountRepo = new DummyDiscountRepository();
            var reservations = reservationRepo.GetAll();

            double result1 = discountRepo.GetDiscount(reservations[0], 5);
            double result2 = discountRepo.GetDiscount(reservations[1], 4);
            double result3 = discountRepo.GetDiscount(reservations[1], 1);

            Assert.AreEqual(45, result1);
            Assert.AreEqual(60, result2);
            Assert.AreEqual(39, result3);
        }

        [TestMethod]
        public void CheckNewCalculatedPrice()
        {
            //Assert that the method CalculateNewPrice correctly calculates the new price
            var repo = new DummyDiscountRepository();

            double result1 = repo.CalculateNewPrice(300, 45);
            double result2 = repo.CalculateNewPrice(700, 60);
            double result3 = repo.CalculateNewPrice(299, 39);

            Assert.AreEqual(165, result1);
            Assert.AreEqual(280, result2);
            Assert.AreEqual(182.39, result3);
        }
    }
}
