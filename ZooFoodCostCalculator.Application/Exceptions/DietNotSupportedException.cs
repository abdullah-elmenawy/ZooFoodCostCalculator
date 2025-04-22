
namespace ZooFoodCostCalculator.Application.Exceptions
{
    [Serializable]
    public class DietNotSupportedException : Exception
    {
        public DietNotSupportedException() : base()
        {

        }
        public DietNotSupportedException(string message) : base(message)
        {

        }

        public DietNotSupportedException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
