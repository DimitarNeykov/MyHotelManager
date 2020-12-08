namespace MyHotelManager.Web.Infrastructure.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class ValidIdentificationNumberAttribute : ValidationAttribute
    {
        private readonly byte[] mass = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
        private readonly string errorMessage;

        public ValidIdentificationNumberAttribute(string errorMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var identificationNumber = value.ToString();

            if (identificationNumber != null && identificationNumber.Length == this.mass.Length + 1)
            {
                if (int.TryParse(identificationNumber, out var data))
                {
                    var isValidIdentificationNumber = (new CheckSum().CalculateCheckSum(data, this.mass) % 10) == (data % 10);
                    if (!isValidIdentificationNumber)
                    {
                        return new ValidationResult(this.errorMessage);
                    }

                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(this.errorMessage);
        }
    }
}
