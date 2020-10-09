using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemsREST.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Items;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ItemsREST.Controllers.Tests
{
    [TestClass()]
    public class ItemsControllerTests
    {
        ItemsController cmd = new ItemsController();
        
       
        [TestMethod()]
        public void GetTest()
        {
            Assert.AreEqual(cmd.Get().Count(), 6);
        }

        [TestMethod()]
        public void GetTest1()
        {
            Assert.AreEqual(2,cmd.Get(2).Nr);
            Assert.IsNotNull(cmd.Get(2));

        }

        [TestMethod()]
        public void PostTest()
        {
            Item i = new Item(8, "string", 50);
            cmd.Post(i);
            Assert.AreEqual(7 ,cmd.Get().Count());
        }

        [TestMethod()]
        public void PutTest()
        {
            Item i = new Item(1, "string", 50);
            cmd.Put(1, i);
            Assert.AreEqual(50, cmd.Get(1).Price);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            cmd.Delete(6);
            Assert.AreEqual(5, cmd.Get().Count());
        }
    }
}