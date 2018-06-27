using System.Linq;
using NUnit.Framework;
using Waes.Controllers;
using Microsoft.AspNetCore.Mvc;
using Waes.Core;
using System;
using Microsoft.EntityFrameworkCore;

namespace Waes.Tests
{
    [TestFixture]
    public class WaesControllerTest
    {
        const string left = "abc";
        const string right = "123";
        const string sqlDrop = "DROP TABLE ENTITIES;";
        const string sqlSelect = "SELECT * FROM ENTITIES LIMIT 1;";
        const string sqlCreate = 
            "CREATE TABLE \"Entities\" (\"Id\" INTEGER NOT NULL CONSTRAINT \"PK_Entities\" PRIMARY KEY,\"Left\" TEXT NULL,\"Right\" TEXT NULL);";

        [SetUp]
        public void SetUp() => new WaesContext().Database.EnsureCreated();

        [OneTimeTearDown]
        public void OneTimeTearDown() => new WaesContext().Entities.FromSql($"{sqlDrop}{sqlCreate}{sqlSelect}").Take(1);

        [Test]
        public void ShouldSaveLeft()
        {
            var e = new Entity(){ Id = 500, Left = left };
            e.Id = new Random().Next(20, 30);
            var ctrl = new DiffController();
            var res = ctrl.PostLeft(e.Id, e) as OkObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void ShouldSaveRight()
        {
            var e = new Entity(){ Id = 600, Right = right };
            var ctrl = new DiffController();
            var res = ctrl.PostRight(e.Id, e) as OkObjectResult;
            Assert.AreEqual(200, res.StatusCode);
        }

        [Test]
        public void ShouldNotSaveRight()
        {
            var e = new Entity(){ Id = 700};
            var ctrl = new DiffController();
            var res = ctrl.PostRight(e.Id, e) as BadRequestObjectResult;
            Assert.AreEqual(400, res.StatusCode);
            
        }


        [Test]
        public void ShouldNotSaveLeft()
        {
            var e = new Entity(){ Id = 700};
            var ctrl = new DiffController();
            var res = ctrl.PostLeft(e.Id, e) as BadRequestObjectResult;
            Assert.AreEqual(400, res.StatusCode);
        }


        [Test]
        public void ShouldGetEntityLeft()
        {
            var e = new Entity(){ Id = 800, Left = left};
            var controller = new DiffController();
            controller.PostLeft(e.Id, e);
            var res = controller.GetLeft(e.Id).Value;
            Assert.AreEqual(e.Left, res);
            
        }

        [Test]
        public void ShouldGetEntityRight()
        {
            var e = new Entity(){ Id = 800, Right = right };
            var controller = new DiffController();
            controller.PostRight(e.Id, e);
            var res = controller.GetRight(e.Id).Value;
            Assert.AreEqual(e.Right, res);
            
        }

    }
}