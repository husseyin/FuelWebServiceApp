using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.OperationResult
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
            Console.WriteLine(message);
        }
        public ErrorResult() : base(false)
        {

        }
    }
}
