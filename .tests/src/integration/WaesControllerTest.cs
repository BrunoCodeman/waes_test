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
      
        private Entity _entity;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var ctx = new WaesContext();
            _entity = new Entity() { Id = new Random().Next(1, 10), Left = left, Right = right };
            
            ctx.Add(_entity);
            ctx.SaveChanges();
        }

        [SetUp]
        public void SetUp() => new WaesContext().Database.EnsureCreated();

        [OneTimeTearDown]
        public void OneTimeTearDown() => new WaesContext().Entities.FromSql($"{sqlDrop}{sqlCreate}{sqlSelect}").Take(1);

        [Test]
        public void ShouldSaveLeft()
        {
            var e = _entity;
            e.Id = new Random().Next(20, 30);
            var ctrl = new DiffController();
            var res = ctrl.PostLeft(e.Id, e).Value;
            Assert.IsTrue(res);
        }

        [Test]
        public void ShouldSaveRight()
        {
            var e = _entity;
            var ctrl = new DiffController();
            var res = ctrl.PostRight(_entity.Id, _entity).Value;
            Assert.IsTrue(res);
        }

        [Test]
        public void ShouldNotSaveRight()
        {
            var e = new Entity(){ Id = new Random().Next(30, 40)};
            var ctrl = new DiffController();
            var res = ctrl.PostRight(_entity.Id, _entity).Value;
            Assert.IsFalse(res);
        }


        [Test]
        public void ShouldNotSaveLeft()
        {
            var e = _entity;
            var ctrl = new DiffController();
            var res = ctrl.PostLeft(_entity.Id, _entity).Value;
            Assert.IsFalse(res);
        }


        [Test]
        public void ShouldGetEntityLeft()
        {

            var controller = new DiffController();
            ActionResult<string> res = controller.GetLeft(_entity.Id).Value;
            Assert.AreEqual(_entity.Left, res);
            
        }

        [Test]
        public void ShouldGetEntityRight()
        {

            var controller = new DiffController();
            var res = controller.GetRight(_entity.Id).Value;
            Assert.AreEqual(_entity.Right, res);
            
        }

    }
}