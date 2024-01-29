using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskTracker.Models;

namespace TaskTracker.Tests.Models
{
    public class MyTaskUnitTests
    {
        [Theory]
        [InlineData("ValidName", "ValidDescription")]                             
        public void MyTask_Should_Validate_Name_And_Description(string name, string description)
        {
            // Arrange
            var myTask = new MyTask { Name = name, Description = description };

            // Act            

            // Assert
            Assert.Equal(name, myTask.Name);
            Assert.Equal(description, myTask.Description);
            Assert.Equal(0, myTask.Id);
        }

        [Fact]
        public void MyTask_Id_Should_HaveKeyAttribute_and_DatabaseGeneratedOptionIdentityAttribute()
        {
            // Arrange
            var propertyInfo = typeof(MyTask).GetProperty("Id");

            // Act
            var keyAttribute = propertyInfo.GetCustomAttributes(typeof(KeyAttribute), false);
            var dbGeneratedAttribute = propertyInfo.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), false);

            // Assert
            Assert.Single(keyAttribute);
            Assert.Single(dbGeneratedAttribute);
            Assert.Equal(DatabaseGeneratedOption.Identity, ((DatabaseGeneratedAttribute)dbGeneratedAttribute[0]).DatabaseGeneratedOption);
        }
        

        [Fact]
        public void MyTask_CreatedDate_UpdatedDate_Should_HaveDefaultValue()
        {
            // Arrange
            var myTask = new MyTask();

            // Act

            // Assert
            Assert.NotEqual(default(DateTime), myTask.CreatedDate);
            Assert.Equal(default(DateTime), myTask.UpdatedDate);
        }   
                
    }
}
