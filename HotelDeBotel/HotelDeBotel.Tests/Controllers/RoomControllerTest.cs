using System;
using System.Web.Mvc;
using HotelDeBotel.Controllers;
using HotelDeBotel.Models.Repositories;
using HotelDeBotel.Models.Viewmodels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelDeBotel.Tests.Controllers
{
    [TestClass]
    public class RoomControllerTest
    {
     
        [TestMethod]
        public void Index()
        {
            RoomController roomController = new RoomController(new DummyRoomRepository());

            ViewResult result = roomController.Index() as ViewResult;

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Details()
        {
            RoomController roomController = new RoomController(new DummyRoomRepository());
            ActionResult result = roomController.Details(1);

            Assert.IsNotNull(result);
        }

 
        [TestMethod]
        public void GetCreate()
        {
            RoomController roomController = new RoomController(new DummyRoomRepository());
            ActionResult result = roomController.Create();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostCreate()
        {
            var repo = new DummyRoomRepository();
            RoomController roomController = new RoomController(repo);
            RoomVM newRoom = new RoomVM
            {
                Name = "Test room 1",
                Price = 1337,
                Size = 4,
                IsDeleted = false,
                Id = 12
            };

            ActionResult result = roomController.Create(newRoom);
            Assert.IsNotNull(result);

            RoomVM createdRoom = repo.GetById(12);
            Assert.AreEqual(createdRoom.Name, newRoom.Name);
            Assert.AreEqual(createdRoom.Price, newRoom.Price);
            Assert.AreEqual(createdRoom.Size, newRoom.Size);
        }


        [TestMethod]
        public void GetEdit()
        {
            RoomController roomController = new RoomController(new DummyRoomRepository());
            ActionResult result = roomController.Edit(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PostEdit()
        {
            var repo = new DummyRoomRepository();
            RoomController roomController = new RoomController(repo);
            RoomVM newRoom = new RoomVM
            {
                Name = "Test room 1",
                Price = 1337,
                Size = 4,
                IsDeleted = false,
                Id = 12
            };

            ActionResult result = roomController.Create(newRoom);
            Assert.IsNotNull(result);

            RoomVM createdRoom = repo.GetById(12);

            createdRoom.Name = "Test room 2 (Editted)";
            createdRoom.Price = 404;
            _ = roomController.Edit(createdRoom);

            RoomVM edditedRoomVM = repo.GetById(12);
            Assert.AreEqual("Test room 2 (Editted)", edditedRoomVM.Name);
            Assert.AreEqual(404, edditedRoomVM.Price);

        }

        [TestMethod]
        public void GetDelete()
        {
            var repo = new DummyRoomRepository();
            RoomController roomController = new RoomController(repo);
            ActionResult result = roomController.Delete(1);

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void PostDelete()
        {
            var repo = new DummyRoomRepository();
            RoomController roomController = new RoomController(repo);
            ActionResult result = roomController.Delete(1, new FormCollection());

            Assert.IsNotNull(result);

            RoomVM createdRoom = repo.GetById(1);
            Assert.AreEqual(createdRoom, null);

        }
    }
}
