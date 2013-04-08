using System.Linq;
using System.Web.Mvc;
using PizzaMasterEmporium.Framework.PerformanceCounters;
using PizzaMasterEmporium.Framework.Utils;
using PizzaMasterEmporium.Framework.Web.Tracking;

namespace PizzaMasterEmporium.Framework.Web.ActionFilters
{
    public class RequestStatusAttribute : ActionFilterAttribute
    {
        public RequestStatus RequestStatus { get; set; }
        public IDateTimeWrapper DateTimeWrapper { get; set; }

        private TimeMeasurer _totalRequestTimeMeasurer;
        private TimeMeasurer _totalActionTimeMeasurer;
        private TimeMeasurer _totalResultTimeMeasurer;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            RequestStatus.ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            RequestStatus.ActionName = filterContext.ActionDescriptor.ActionName;
            RequestStatus.Parameters = filterContext.ActionDescriptor.GetParameters()
                                                    .Select(x => string.Concat((string) x.ParameterType.Name, (string) " ", (string) x.ParameterName)).ToArray();
            _totalRequestTimeMeasurer = new TimeMeasurer().Start();
            _totalActionTimeMeasurer = new TimeMeasurer().Start();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            RequestStatus.AddTimeSpanHolder("ActionTime", _totalActionTimeMeasurer.Stop().TimeSpan);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            _totalResultTimeMeasurer = new TimeMeasurer().Start();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RequestStatus.AddTimeSpanHolder("ResultTime", _totalResultTimeMeasurer.Stop().TimeSpan);
            if (filterContext.Exception == null)
            {
                // var timeMeasurer = new TimeMeasurer().Start();
                // Store the UnitOfWork.
                // RequestStatus.AddTimeSpanHolder("UnitOfWork.Store()", timeMeasurer.Stop().TimeSpan);

                //using (var session = CommandDocumentSessionHolder.DocumentSession)
                //{
                //    var timeMeasurer = new TimeMeasurer().Start();
                //    session.SaveChanges();
                //    RequestStatus.AddTimeSpanHolder("DocumentSession.SaveChanges()", timeMeasurer.Stop().TimeSpan);
                //}
            }
            RequestStatus.AddTimeSpanHolder("RequestTime", _totalRequestTimeMeasurer.Stop().TimeSpan);
            StoreRequestStatus(RequestStatus);
        }

        private void StoreRequestStatus(RequestStatus RequestStatus)
        {
            var now = DateTimeWrapper.UtcNow;
        }
    }
}