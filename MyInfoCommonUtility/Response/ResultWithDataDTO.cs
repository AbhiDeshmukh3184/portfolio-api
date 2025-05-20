using System;
using System.Collections.Generic;
using System.Text;

namespace MyInfoCommonUtility.Response
{
    public class ResultWithDataDTO<T> : ResultDTO
    {
        public T Data { get; set; }
    
    }
}
