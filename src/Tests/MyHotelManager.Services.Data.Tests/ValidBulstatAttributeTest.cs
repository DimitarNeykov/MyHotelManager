using System;
using MyHotelManager.Web.Infrastructure.Attributes;
using Xunit;

namespace MyHotelManager.Services.Data.Tests
{
    public class ValidBulstatAttributeTest
    {

        [Fact]
        public void IsValidReturnsTrueForValidBulstat()
        {
            var attribute = new ValidBulstatAttribute("Invalid");

            var isValid = attribute.IsValid("131071587");

            Assert.True(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateTimeBeforeToda()
        {
            var attribute = new ValidBulstatAttribute("Invalid");

            var isValid = attribute.IsValid("131061587");

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateTimeBeforeTod()
        {
            var attribute = new ValidBulstatAttribute("Invalid");

            var isValid = attribute.IsValid("131");

            Assert.False(isValid);
        }

        [Fact]
        public void IsValidReturnsTrueForDateTimeBeforeTo()
        {
            var attribute = new ValidBulstatAttribute("Invalid");

            var isValid = attribute.IsValid("1310615899");

            Assert.False(isValid);
        }
    }
}
