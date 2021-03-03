namespace MyHotelManager.Services.Data.Tests
{
    using System;

    using MyHotelManager.Web.Infrastructure.Attributes;
    using Xunit;

    public class DateBeforeTodayAttributeTest
    {
        [Fact]
        public void IsValidReturnsTrueForDateTimeBeforeToday()
        {
            var attribute = new DateBeforeTodayAttribute("Invalid");

            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(1));

            Assert.True(isValid);
        }

        [Fact]
        public void IsValidReturnsFalseForDateTimeBeforeToday()
        {
            var attribute = new DateBeforeTodayAttribute("Invalid");

            var isValid = attribute.IsValid(DateTime.UtcNow.AddDays(-1));

            Assert.False(isValid);
        }
    }
}
