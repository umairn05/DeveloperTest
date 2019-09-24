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
        public readonly int intID1 = 1;
        public readonly int intID2 = 2;

        [TestInitialize]
        public void Setup()
        {
            storeables = new List<IStoreable<object>>() { new Storeable<object> (intID1), new Storeable<object>(intID2) };
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
            var result = repository.Get(intID1);

            //Assert 
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, intID1);
        }

        [TestMethod]
        [TestCategory("Success")]
        public void GivenRepository_WhenReqeustToAddItem_ShouldSaveRecordInRepository()
        {
            //Arrange 
            int intId = 3;
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
            repository.Delete(intID1);
            IEnumerable<IStoreable<Object>> result = repository.GetAll();

            //Assert
            Assert.IsFalse(result.Any(s=>s.Id.Equals(intID1)));
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
            Guid newGuid = Guid.NewGuid();

            // Act 
            var result = repository.Get(newGuid);

            // Assert - Expects exception
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [TestCategory("Exception")]
        public void GivenRepository_WhenRequestToAddItemWithExistingId_ShouldRaiseArgumentException()
        {
            // Arrange
            var newRecord= new Storeable<object>(intID2);

            // Act 
            repository.Save(newRecord);

            // Assert - Expects exception
        }

        [TestMethod]
        [TestCategory("Exception")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GivenRepository_WhenRequestToRemoveItemWithNullId_ShouldRaiseArgumentNullExceptionException()
        {
            //Arrange 
            int? newId = null;

            //Act
            repository.Delete(newId);

            // Assert - Expects exception

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Exception")]
        public void GivenRepository_WhenRequestToAddItemWWithNullId_ShouldRaiseArgumentNullException()
        {
            // Arrange
            int? newId = null;
            var newRecordInTypeId = new Storeable<object>(newId);

            // Act 
            repository.Save(newRecordInTypeId);

            // Assert - Expects exception
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [TestCategory("Exception")]
        public void GivenRepository_WhenRequesForRecordByNullId_ShouldRaiseArgumentNullException()
        {
            // Arrange
            int? newId = null;

            // Act 
            var result =  repository.Get(newId);

            // Assert - Expects exception
        }

    }
}
