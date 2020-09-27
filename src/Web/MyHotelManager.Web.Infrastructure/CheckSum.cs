namespace MyHotelManager.Web.Infrastructure
{
    public class CheckSum
    {
        internal long CalculateCheckSum(long data, byte[] mass)
        {
            long sum = 0;

            data /= 10;

            for (long i = mass.Length - 1; i > -1; --i, data /= 10)
            {
                sum += (data % 10) * mass[i];
            }

            return sum % 11;
        }
    }
}
