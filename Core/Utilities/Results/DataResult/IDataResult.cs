using Core.Utilities.Results.OperationResult;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results.DataResult
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
