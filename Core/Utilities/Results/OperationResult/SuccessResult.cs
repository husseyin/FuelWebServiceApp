using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.OperationResult
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
            Console.WriteLine(message);
        }
        public SuccessResult() : base(true)
        {

        }
    }
}
