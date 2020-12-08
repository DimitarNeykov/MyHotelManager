namespace MyHotelManager.Web.Infrastructure.Attributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DateBeforeTodayAttribute : ValidationAttribute
    {
        private readonly string errorMessage;

        public DateBeforeTodayAttribute(string errorMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = DateTime.Parse(value.ToString());

            if (date.Date < DateTime.UtcNow.Date)
            {
                return new ValidationResult(this.errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
