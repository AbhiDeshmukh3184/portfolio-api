using System;
using System.Collections.Generic;
using System.Text;

namespace MyInfoCommonUtility.Paging
{
    public interface IPagingParameter
    {
        int? PageNumber { get; set; }
        int? PageSize { get; set; }
    }
}
