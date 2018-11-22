


namespace CryptoWatcher.Shared.Helpers
{
    public static class MathHelper
    {
        public static decimal Sum(params decimal[] customerssalary)
        {
            var result = 0m;

            for (int i = 0; i < customerssalary.Length; i++)
            {
                result += customerssalary[i];
            }

            return result;
        }
        public static decimal Average(params decimal[] customerssalary)
        {
            var sum = Sum(customerssalary);
            var result = sum / customerssalary.Length;
            return result;
        }
    }
}