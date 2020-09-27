namespace MyHotelManager.Web.Infrastructure.Attributes
{
    using System.ComponentModel.DataAnnotations;

    public class ValidBulstatAttribute : ValidationAttribute
    {
        private readonly byte[] mass = { 1, 2, 3, 4, 5, 6, 7, 8 };
        private readonly byte[] massExt = { 3, 4, 5, 6, 7, 8, 9, 10 };
        private readonly byte[] massDomain = { 2, 7, 3, 5 };
        private readonly byte[] massDomainExt = { 4, 9, 5, 7 };

        private readonly string errorMessage;

        public ValidBulstatAttribute(string errorMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var bulstat = value.ToString();
            if (bulstat != null && (bulstat.Length == this.mass.Length + 1 || bulstat.Length == this.mass.Length + this.massDomain.Length))
            {
                if (long.TryParse(bulstat.Substring(0, 9), out long workData))
                {
                    long res = new CheckSum().CalculateCheckSum(workData, this.mass);
                    if (res == 10)
                    {
                        res = new CheckSum().CalculateCheckSum(workData, this.massExt);
                    }

                    if (res % 10 != workData % 10)
                    {
                        return new ValidationResult(this.errorMessage);
                    }

                    if (bulstat.Length == 13)
                    {
                        if (long.TryParse(bulstat.Substring(8, 4), out workData))
                        {
                            res = new CheckSum().CalculateCheckSum(workData, this.massDomain);
                            if (res == 10)
                            {
                                res = new CheckSum().CalculateCheckSum(workData, this.massDomainExt);
                            }

                            if (res % 10 != workData % 10)
                            {
                                return new ValidationResult(this.errorMessage);
                            }
                        }
                    }

                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(this.errorMessage);
        }
    }
}
