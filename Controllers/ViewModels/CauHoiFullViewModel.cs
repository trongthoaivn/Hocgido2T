using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hocgido2T.Controllers.ViewModels
{
    public class CauHoiFullViewModel
    {
       public CauHoiViewModel CauHoi { get; set; }
       public List< DapAnViewModel> DapAns { get; set; }

    }
}