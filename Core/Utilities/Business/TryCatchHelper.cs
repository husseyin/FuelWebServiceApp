using Core.Utilities.Results.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Busines
{
    public class TryCatchHelper
    {
        public static IResult RunTheMethod(Action method)
        {
            try
            {
                method();                
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }

            return new SuccessResult();
        }
    }
}
