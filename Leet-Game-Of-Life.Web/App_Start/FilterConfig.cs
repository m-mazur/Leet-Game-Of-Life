using System.Web;
using System.Web.Mvc;

namespace Leet_Game_Of_Life.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
