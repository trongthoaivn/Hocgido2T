using Hocgido2T.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hocgido2T.Controllers
{
    public class DAOController
    {
        public dbhocgido db = new dbhocgido();
        public Boolean XoaDapAn(String madapan)
        {
            var dapan = db.DapAns.First(p => p.MaDapAn.Equals(madapan));
            if (dapan != null)
            {
                db.DapAns.Remove(dapan);
                db.SaveChanges();
                return true;
            }
            else return false;
        }

        //public Boolean XoaCauHoi(String macauhoi)
        //{

        //    var cauhoi = db.CauHois.First(p => p.MaCauHoi.Equals(macauhoi));
        //    if (cauhoi != null)
        //    {
        //        var result = true;
        //        foreach (DapAn  item in db.DapAns.Where(p => p.MaCauHoi.Equals(macauhoi)))
        //        {
        //            if (XoaDapAn(item.MaDapAn))
        //                result = true;
        //        }
        //    }
        }
    }
}