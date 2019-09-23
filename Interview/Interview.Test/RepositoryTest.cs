using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Interview.Test
{
    [TestClass]
    public class RepositoryTest
    {
        private IRepository<IStoreable<object>, object> repository;
        private List<IStoreable<object>> storeables;

        [TestInitialize]
        public void Setup()
        {
            storeables = new List<IStoreable<object>>() { new Storeable<object> (1) };
            repository = new Repository<IStoreable<object>>(storeables);

        }

        [TestMethod]
        [TestCategory("Success")]
        public void GivenRepository_WhenRequestedForAll_ShouldReturnRepositoryList()
        {
            //Arrange 
            IEnumerable<IStoreable<object>> result;

            //Act
            result = repository.GetAll();

            // Assert
            Assert.IsInstanceOfType(result, typeof(IList<IStoreable<object>>));
            Assert.IsTrue(result.Count() > 0);
        }

        [TestMethod]
        [TestCategory("Success")]
        public void GivenRespository_WhenRequestedForSingleRecordById_ShouldReturnSingleReocrdFromRepository()
        {

            //Arrange
            int intId = 1;

            //Act
            var result = repository.Get(intId);

            //Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, intId);
        }

        [TestMethod]
        [TestCategory("Success")]
        public void GivenRepository_WhenReqeustToAddItem_ShouldSaveRecordInRepository()
        {
            //Arrange 
            int intId = 1;
            var newRecordInTypeId= new Storeable<object>(intId);
   
            //Act 
            repository.Save(newRecordInTypeId);
            var result = repository.Get(intId);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, intId);

        }

        [TestMethod]
        [TestCategory("Success")]
        public void GivenRepository_WhenRequestToRemoveItem_ShouldDeleteRecordFromRepository()
        {
            //Arrange 
            int intId = 1;

            //Act
            repository.Delete(intId);
            IStoreable<Object> result = repository.Get(intId);

            //Assert
            Assert.IsNull(result);
        }
    }
}
