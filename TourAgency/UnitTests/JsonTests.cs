using TourAgency;
using TourAgency.Models;

namespace UnitTests
{
    [TestClass]
    public class JsonTests
    {
        [TestMethod]
        public void SaveToJson_EmptyList_ReturnsTrue()
        {
            // Arrange
            List<object> list = new();
            string path = "test.json";

            // Act
            bool result = JsonLogic.SaveToJson(list, path);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SaveToJson_NonEmptyList_ReturnsTrue()
        {
            // Arrange
            List<object> list = new() { "test", 1, true };
            string path = "test.json";

            // Act
            bool result = JsonLogic.SaveToJson(list, path);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void SaveToJson_InvalidPath_ThrowsException()
        {
            // Arrange
            List<object> list = new() { "test", 1, true };
            string path = "";

            // Act
            bool result = JsonLogic.SaveToJson(list, path);

            // Assert
            // Expect an exception to be thrown
        }

        [TestMethod]
        public void ReadFromJson_ReturnsCorrectData()
        {
            // Arrange
            string path = "test1.json";
            List<Tour> expectedData = new List<Tour>()
            {
                new Tour("France", "Paris", DateTime.Now.AddDays(7), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love"),
                new Tour("France2", "Paris2", DateTime.Now.AddDays(14), 7, 1000, TourStatus.Actual, 20, "Explore the City of Love")
            };

            // Act
            bool result = JsonLogic.ReadFromJson(path, out List<Tour> actualData);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedData.Count, actualData.Count);
            for (int i = 0; i < expectedData.Count; i++)
            {
                Assert.AreEqual(expectedData[i].Country, actualData[i].Country);
                Assert.AreEqual(expectedData[i].City, actualData[i].City);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadFromJson_ThrowsIOException()
        {
            // Arrange
            string path = "non_existing_file.json";

            // Act
            bool result = JsonLogic.ReadFromJson(path, out List<RegisteredUser> data);

            // Assert
            // Expects Exception to be thrown
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ReadFromJson_ThrowsDeserializationException()
        {
            // Arrange
            string path = "test2.json";
            List<string> data = new List<string>()
            {
                "{\"Id\":1,\"Name\":\"Test 1\"}",
                "{\"Id\":2}" // missing "Name" property
            };
            File.WriteAllLines(path, data);

            // Act
            bool result = JsonLogic.ReadFromJson(path, out List<string> actualData);

            // Assert
            // Expects Exception to be thrown
        }

        [TestMethod]
        public void ReadFromJson_ReturnsFalseForEmptyFile()
        {
            // Arrange
            string path = "empty.json";
            File.WriteAllText(path, "");

            // Act
            bool result = JsonLogic.ReadFromJson(path, out List<string> actualData);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, actualData.Count);
        }

    }
}
