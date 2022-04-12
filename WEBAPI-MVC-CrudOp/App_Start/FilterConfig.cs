using System.Web;
using System.Web.Mvc;

namespace WEBAPI_MVC_CrudOp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
