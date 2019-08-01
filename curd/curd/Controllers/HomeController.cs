using Curd.Models.DbContact;
using Curd.Models.Function;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace curd.Controllers
{
    public class HomeController : Controller
    {
        protected string actionName = "";
        protected string controllerName = "";
        protected DataConversionModule DataConvert = new DataConversionModule();
        protected Dictionary<string, string> getQueryData = new Dictionary<string, string>();
        protected Dictionary<string, string> postQueryData = new Dictionary<string, string>();
        protected Dictionary<string, string> queryData = new Dictionary<string, string>();
        protected MessageModule message = new MessageModule();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            actionName = Convert.ToString(Request.RequestContext.RouteData.Values["action"]);
            controllerName = Convert.ToString(Request.RequestContext.RouteData.Values["controller"]);
        }
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            List<DataRow> result = new List<DataRow>();
            dt = message.getList();
            ViewBag.result = DataConvert.toLists(dt);

            ViewBag.actionName = actionName;
            ViewBag.controllerName = controllerName;
            ViewBag.alerts = TempData["alerts"];
            return View();
        }
        public ActionResult AddForm()
        {
            postQueryData = DataConvert.PostToKeyValue();
            string insertId = "";
            int i = 0;
            bool result = false;
            if (postQueryData.ContainsKey("op") && postQueryData["op"] == "add")
            {
                postQueryData.Remove("op");
                insertId = message.addData(postQueryData);
                result = int.TryParse(insertId, out i);
                if (!result)
                {
                    TempData["alerts"] = "資料錯誤";
                }
                else
                {
                    TempData["alerts"] = "新增完成";
                }
                return RedirectToAction("Index");
            }

            ViewBag.actionName = actionName;
            ViewBag.controllerName = controllerName;
            return View();
        }
        public ActionResult UpdateForm()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            bool result = false;
            int i = 0;
            string insertId = "";
            getQueryData = DataConvert.GetToKeyValue();
            postQueryData = DataConvert.PostToKeyValue();
            if (postQueryData.ContainsKey("op") && postQueryData["op"] == "update")
            {

                postQueryData.Remove("op");

                insertId = message.updateData(postQueryData);
                result = int.TryParse(insertId, out i);
                if (!result)
                {
                    TempData["alerts"] = "資料錯誤";
                }
                else
                {
                    TempData["alerts"] = "修改完成";
                }
                return RedirectToAction("Index");
            }
            if (!getQueryData.ContainsKey("id"))
            {
                return RedirectToAction("Index");
            }
            else
            {
                dt = message.getData(getQueryData["id"]);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dr = dt.Rows[0];
                }
                else
                {
                    TempData["alerts"] = "資料錯誤";
                    return RedirectToAction("Index");
                }
            }
            ViewBag.result = dr;
            ViewBag.actionName = actionName;
            ViewBag.controllerName = controllerName;
            return View();
        }
    }
}