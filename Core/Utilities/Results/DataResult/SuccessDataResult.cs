﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.DataResult
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
            Console.WriteLine(message);
        }
        public SuccessDataResult(T data) : base(data, true)
        {

        }
        public SuccessDataResult(string message) : base(default, true, message)
        {
            Console.WriteLine(message);
        }
        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
