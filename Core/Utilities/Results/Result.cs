using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public bool Success ;

        public string Message => throw new NotImplementedException();
    }
}
