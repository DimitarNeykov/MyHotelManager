namespace MyHotelManager.Services.Data.Tests
{
    using System;

    using MyHotelManager.Web.Infrastructure.Attributes;
    using Xunit;

    public class DateAfterTodayAttributeTest
    {
        [Fact]
        public void IsValidReturnsFalseForDateTimeAfterToday()
        {
            var attribute = new DateAfterTodayAttribute("Invalid");

            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(1));

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateTimeAfterToday()
        {
            var attribute = new DateAfterTodayAttribute("Invalid");

            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(-1));

            Assert.True(isValid);
        }
    }
}
