using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PizzeriaRestApi.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetFridgessLoad()
        {
            var list = _service.GetFridgesLoad();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public IHttpActionResult GetVisitorOrderPizzas(ReportBindingModel model)
        {
            var list = _service.GetVisitorOrderPizzas(model);
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void SavePizzaPrice(ReportBindingModel model)
        {
            _service.SavePizzaPrice(model);
        }

        [HttpPost]
        public void SaveFridgesLoad(ReportBindingModel model)
        {
            _service.SaveFridgesLoad(model);
        }

        [HttpPost]
        public void SaveVisitorOrderPizzas(ReportBindingModel model)
        {
            _service.SaveVisitorOrderPizzas(model);
        }
    }
}