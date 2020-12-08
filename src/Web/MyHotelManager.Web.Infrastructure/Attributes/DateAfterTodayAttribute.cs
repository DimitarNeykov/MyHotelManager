namespace MyHotelManager.Web.Infrastructure.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateAfterTodayAttribute : ValidationAttribute
    {
        private readonly string errorMessage;

        public DateAfterTodayAttribute(string errorMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (date.Date > DateTime.UtcNow.Date)
            {
                return new ValidationResult(this.errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
