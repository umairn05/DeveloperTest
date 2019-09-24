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
        public readonly int ID = 1;

        [TestInitialize]
        public void Setup()
        {
            storeables = new List<IStoreable<object>>() { new Storeable<object> (ID) };
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

            //Act
            var result = repository.Get(ID);

            //Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, ID);
        }

        [TestMethod]
        [TestCategory("Success")]
        public void GivenRepository_WhenReqeustToAddItem_ShouldSaveRecordInRepository()
        {
            //Arrange 
            int intId = 2;
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
        public void GivenRepository_WhenRequestToRemoveExistingItem_ShouldDeleteRecordFromRepository()
        {
            //Arrange 

            //Act
            repository.Delete(ID);
            IStoreable<Object> result = repository.Get(ID);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        [TestCategory("Exception")]
        [ExpectedException(typeof(ArgumentException))]
        public void GivenRepository_WhenRequestToRemoveNonExistingItem_ShouldRaiseAnException()
        {
            //Arrange 
            Guid newGuid = Guid.NewGuid();

            //Act
            repository.Delete(newGuid);

            // Assert - Expects exception

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [TestCategory("Exception")]
        public void GivenRepository_WhenRequesForRecordByNonExisitngId_ShouldRaiseArgumentOutOfRangeException()
        {
            // Arrange
            int? newId = null;

            // Act 
            var result = repository.Get(newId);

            // Assert - Expects exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [TestCategory("Exception")]
        public void GivenRepository_WhenRequestToAddExistingItem_ShouldRaiseArgumentOutOfRangeException()
        {
            // Arrange
            int? newId = null;
            var newRecordInTypeId = new Storeable<object>(newId);

            // Act 
            repository.Save(newRecordInTypeId);

            // Assert - Expects exception
        }

        [TestMethod]
        [TestCategory("Exception")]
        [ExpectedException(typeof(NullReferenceException))]
        public void GivenRepository_WhenRequestToRemoveNonExistingItem_ShouldRaiseNullReferenceException()
        {
            //Arrange 
            Guid newGuid = Guid.NewGuid();

            //Act
            repository.Delete(newGuid);

            // Assert - Expects exception

        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exception")]
        public void GivenRepository_WhenRequestToAddNullItem_ShouldRaiseArgumentOutODRangeException()
        {
            // Arrange
            int? newId = null;
            var newRecordInTypeId = new Storeable<object>(newId);

            // Act 
            repository.Save(newRecordInTypeId);

            // Assert - Expects exception
        }
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        [TestCategory("Exception")]
        public void GivenRepository_WhenRequesForRecordByNullId_ShouldRaiseNullRefernceException()
        {
            // Arrange
            int? newId = null;

            // Act 
            var result =  repository.Get(newId);

            // Assert - Expects exception
        }

    }
}
