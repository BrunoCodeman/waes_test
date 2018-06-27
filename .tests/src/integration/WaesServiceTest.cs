using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Waes.Core;
using System;

namespace Waes.Testss.integration 
{
    [TestFixture]
    public class WaesServiceTest 
    {
        private const string left = "abc";
        private const string right = "123";

        [SetUp]
        public void SetUp() => new WaesContext().Database.EnsureCreated();


        [OneTimeTearDown]
        public void TearDown() 
        {
            var ctx= new WaesContext();
            ctx.Entities.RemoveRange(ctx.Entities.Where((e) => e.Id != null));
        }
        
        [Test]
        public void MustSaveNewEntityIfValid()
        {
            var entity = new Entity(){Id = new Random().Next(1,1000000), Left = left };
            var res = WaesService.SaveOrUpdate(entity);
            Assert.IsTrue(res);
        }

        [Test]
        public void MustSaveAndUpdate()
        {
            var id = 999;
            var entity = new Entity() { Id = id, Left = left };
            WaesService.SaveOrUpdate(entity);
            entity = new Entity() { Id = id, Right = right };
            WaesService.SaveOrUpdate(entity);
            var res = WaesService.Get(id);
            Assert.AreEqual(res.Left, left);
            Assert.AreEqual(res.Right, right);
        }

        [Test]
        public void MustNOTSaveEntityIfInvalidId()
        {
            const string msg = "INVALID ID";
            try
            {
                var res = WaesService.SaveOrUpdate(new Entity());    
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(msg));
            }    
        }

        [Test]
        public void MustNOTSaveEntityIfInvalidLeftAndRight()
        {
            const string msg = "INVALID ENTITY";
            try
            {
                var res = WaesService.SaveOrUpdate(new Entity(){Id = new Random().Next(1000, 2000)});    
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains(msg));
            }
            
            
        }


        public void MustGetEntityIfIdExists()
        {

        }

        public void MustReturnNullIfIdDoesNotExits()
        {
            
        }

    }
}