//using Hocgido2T.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Hocgido2T.Controllers
//{
//    public class DAOController
//    {
//        public dbhocgido db = new dbhocgido();
//        public Boolean XoaDapAns(List<DapAn>  list)
//        {
//            try
//            {
//                if (list != null)
//                {
//                    foreach (DapAn item in list)
//                    {
//                        db.DapAns.Remove(item);
//                        db.SaveChanges();
//                    }

//                    return true;
//                }
//                else return false;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
            
//        }

//        public Boolean XoaCauHois(List<CauHoi> list)
//        {
//            try
//            {
//                if (list != null)
//                {
//                    foreach (CauHoi item in list)
//                    {
                        
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                return false;
//            }
            
//        }
//    }
//}