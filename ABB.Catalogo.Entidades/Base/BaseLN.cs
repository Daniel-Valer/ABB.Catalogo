using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ABB.Catalogo.Entidades.Base
{
    public class BaseLN : Controller
    {
        protected log4net.ILog Log
        {
            get
            {
                return log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().GetType());
            }
        }
    }
}
