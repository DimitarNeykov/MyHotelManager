namespace MyHotelManager.Web.Infrastructure
{
    public class CheckSum
    {
        internal int CalculateCheckSum(int data, byte[] mass)
        {
            var sum = 0;

            data /= 10;

            for (var i = mass.Length - 1; i > -1; --i, data /= 10)
            {
                sum += (data % 10) * mass[i];
            }

            return (sum % 11);
        }
    }
}
