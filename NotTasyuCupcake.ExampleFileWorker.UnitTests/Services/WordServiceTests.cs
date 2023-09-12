using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Entities;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Interfaces.Services;
using NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NotTasyuCupcake.ExampleFileWorker.ApplicationCore.Services.Tests
{
    [TestClass()]
    public class WordServiceTests
    {
        private readonly Mock<IRepository<WordCounter>> _mockBasketRepo = new();
        private readonly Mock<IAppLogger<WordService>> _mockLogger = new();

        [TestMethod()]
        [DataRow("TEST")]//4
        [DataRow("TESTTEST")]//8
        [DataRow("TESTTESTTESTTESTTEST")]//20
        public void IsValidWordTest_ValidWords_ReturnsTrue(string word)
        {
            //ARRANGE
            var service = new WordService(_mockLogger.Object, _mockBasketRepo.Object);
            //ACT
            var res = service.IsValidWord(word);
            //ASSERT
            Assert.AreEqual(true ,res);
        }

        [TestMethod()]
        [DataRow("T")]//1
        [DataRow("TE")]//2
        [DataRow("TESTTESTTESTTESTTESTTTEST")]//25
        public void IsValidWordTest_InvalidWords_ReturnsFalse(string word)
        {
            //ARRANGE
            var service = new WordService(_mockLogger.Object, _mockBasketRepo.Object);
            //ACT
            var res = service.IsValidWord(word);
            //ASSERT
            Assert.AreEqual(false, res);
        }

        [TestMethod()]
        [DataRow(5)]
        [DataRow(6)]
        [DataRow(int.MaxValue)]
        public void IsValidCountTest_InvalidCounts_ReturnsFalse(int count)
        {
            //ARRANGE
            var service = new WordService(_mockLogger.Object, _mockBasketRepo.Object);
            //ACT
            var res = service.IsValidCount(count);
            //ASSERT
            Assert.AreEqual(true, res);
        }

        [TestMethod()]
        [DataRow(int.MinValue)]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void IsValidCountTest_ValidCounts_ReturnsTrue(int count)
        {
            //ARRANGE
            var service = new WordService(_mockLogger.Object, _mockBasketRepo.Object);
            //ACT
            var res = service.IsValidCount(count);
            //ASSERT
            Assert.AreEqual(false, res);
        }
    }
}