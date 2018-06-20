using PizzeriaRestApi.Services;
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
    public class MainController : ApiController
    {
        private readonly IMainService _service;

        public MainController(IMainService service)
        {
            _service = service;
        }

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpPost]
        public void CreateOrderPizza(OrderPizzaBindingModel model)
        {
            _service.CreateOrderPizza(model);
        }

        [HttpPost]
        public void TakeOrderPizzaInWork(OrderPizzaBindingModel model)
        {
            _service.TakeOrderPizzaInWork(model);
        }

        [HttpPost]
        public void FinishOrderPizza(OrderPizzaBindingModel model)
        {
            _service.FinishOrderPizza(model.Id);
        }

        [HttpPost]
        public void PayOrderPizza(OrderPizzaBindingModel model)
        {
            _service.PayOrderPizza(model.Id);
        }

        [HttpPost]
        public void PutIngredientInFridge(FridgeIngredientBindingModel model)
        {
            _service.PutIngredientInFridge(model);
        }

        [HttpGet]
        public IHttpActionResult GetInfo()
        {
            ReflectionService service = new ReflectionService();
            var list = service.GetInfoByAssembly();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
    }
}