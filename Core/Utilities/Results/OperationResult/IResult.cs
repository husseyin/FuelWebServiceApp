using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.OperationResult
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
