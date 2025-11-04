using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Common.Models
{
    public class Result<T>
    {
        public bool Succeeded { get; init; }
        public T? Value { get; init; }
        public IEnumerable<string> Errors { get; init; } = Array.Empty<string>();

        public static Result<T> Success(T value) =>
            new() { Succeeded = true, Value = value };
        public static Result<T> Failure(params string[] errors) => 
            new() { Succeeded = false, Errors = errors };
    }
}
