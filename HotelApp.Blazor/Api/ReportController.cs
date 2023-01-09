using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using BoldReports.Web.ReportViewer;

namespace HotelApp.Blazor.Api
{
    [Route(template: "api/{controller}/{action}/{id?}")]
    public class ReportController : ControllerBase, IReportController
    {
        private readonly IMemoryCache _cache;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ReportController(IWebHostEnvironment hostEnvironment, IMemoryCache cache)
        {
            _hostEnvironment = hostEnvironment;
            _cache = cache;
        }

        [ActionName(name: "GetResource")]
        [AcceptVerbs(method:"GET")]
        public object GetResource(string key, string resourcetype, bool isPrint)
        {
            throw new NotImplementedException();
        }

        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            throw new NotImplementedException();
        }

        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
            throw new NotImplementedException();
        }

        public object PostReportAction(Dictionary<string, object> jsonResult)
        {
            throw new NotImplementedException();
        }
    }
}
