using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooFoodCostCalculator.Common
{
    public class Result<T>
    {
        private Result()
        {

        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Value { get; set; }

        public static Result<T> CreateSuccess(string message, T value) => new Result<T> { Success = true, Message = message, Value = value };

        public static Result<T> CreateFailure(string message, T value) => new Result<T> { Success = false, Message = message, Value = value };
    }
}
