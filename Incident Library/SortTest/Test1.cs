using Incident_Library.SORTING;
using Incident_Library.INTERFACES;
using Incident_Library.MODELS__Data_;

namespace SortTest
{
    [TestClass]
    public sealed class Test1
    {
        private List<IncidentReport> TestList(params DateTime[] dates) //Form for hjælpe liste så der ikke skal skrives list.Add i arrange
        {
            var list = new List<IncidentReport>();
            foreach (var d in dates)
            {
                list.Add(new IncidentReport { CreatedDate = d });
            }
            return list;
        }

        [TestMethod]
        public void BubbleSort_Oldest()
        {
            //Arrange
            var sort = new SortByDateOldest();
            var list = TestList(new DateTime(2025,3,5), new DateTime(2025,8,7), new DateTime(2025,1,4));
            

            //Act
            var result = sort.Sort(list);

            //Assert
            Assert.AreEqual(new DateTime(2025, 1, 4), result[0].CreatedDate);
            Assert.AreEqual(new DateTime(2025, 3, 5), result[1].CreatedDate);
            Assert.AreEqual(new DateTime(2025, 8, 7), result[2].CreatedDate);
        }

        [TestMethod]
        public void BubbleSort_Newest()
        {
            //Arrange
            var sort = new SortbyDateNewest();
            var list = TestList(new DateTime(2025, 3, 5), new DateTime(2025, 8, 7), new DateTime(2025, 1, 4));

            //Act
            var result = sort.Sort(list);

            //Assert
            Assert.AreEqual(new DateTime(2025, 8, 7), result[0].CreatedDate);
            Assert.AreEqual(new DateTime(2025, 3, 5), result[1].CreatedDate);
            Assert.AreEqual(new DateTime(2025, 1, 4), result[2].CreatedDate);
        }

        [TestMethod]
        public void BubbleSort_Sorted()
        {
            //Arrange
            var sort = new SortbyDateNewest();
            var list = TestList(new DateTime(2025, 8, 7), new DateTime(2025, 3, 5), new DateTime(2025, 1, 4));

            //Act
            var result = sort.Sort(list);

            //Assert
            Assert.AreEqual(new DateTime(2025, 8, 7), result[0].CreatedDate);
            Assert.AreEqual(new DateTime(2025, 3, 5), result[1].CreatedDate);
            Assert.AreEqual(new DateTime(2025, 1, 4), result[2].CreatedDate);

        }

        [TestMethod]
        public void BubbleSort_Empty()
        {
            //Arrange
            var sort = new SortByDateOldest();
            var list = TestList();

            //Act
            var result = sort.Sort(list);

            //Assert
            Assert.AreEqual(0, result.Count);
        }
    }
}
