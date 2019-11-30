using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelDeBotel.Controllers;
using HotelDeBotel.Models.Repositories;
using System.Web.Mvc;

namespace HotelDeBotel.Tests.Controllers
{
    /// <summary>
    /// Summary description for ReservationControllerTest
    /// </summary>
    [TestClass]
    public class ReservationControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
            ReservationController controller = new ReservationController(new DummyReservationRepository(), new DummyRoomRepository(), new DummyDiscountRepository());

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.ViewBag.Repo is IDiscountRepository);
        }

    }
}
