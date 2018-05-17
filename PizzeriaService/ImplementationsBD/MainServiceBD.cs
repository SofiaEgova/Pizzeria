using Microsoft.Office.Interop.Word;
using PizzeriaModel;
using PizzeriaService.BindingModels;
using PizzeriaService.Interfaces;
using PizzeriaService.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PizzeriaService.ImplementationsBD
{
    public class MainServiceBD : IMainService
    {
        private PizzeriaDbContext context;

        public MainServiceBD(PizzeriaDbContext context)
        {
            this.context = context;
        }

        public List<OrderPizzaViewModel> GetList()
        {
            List<OrderPizzaViewModel> result = context.OrderPizzas
                .Select(rec => new OrderPizzaViewModel
                {
                    Id = rec.Id,
                    VisitorId = rec.VisitorId,
                    PizzaId = rec.PizzaId,
                    CookId = rec.CookId,
                    TimeCreate = SqlFunctions.DateName("dd", rec.TimeCreate) + " " +
                                SqlFunctions.DateName("mm", rec.TimeCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.TimeCreate),
                    TimeDone = rec.TimeDone == null ? "" :
                                        SqlFunctions.DateName("dd", rec.TimeDone.Value) + " " +
                                        SqlFunctions.DateName("mm", rec.TimeDone.Value) + " " +
                                        SqlFunctions.DateName("yyyy", rec.TimeDone.Value),
                    Status = rec.Status.ToString(),
                    Count = rec.Count,
                    Sum = rec.Sum,
                    VisitorFIO = rec.Visitor.VisitorFIO,
                    PizzaName = rec.Pizza.PizzaName,
                    CookName = rec.Cook.CookFIO
                })
                .ToList();
            return result;
        }

        public void CreateOrderPizza(OrderPizzaBindingModel model)
        {
            var orderPizza = new OrderPizza
            {
                VisitorId = model.VisitorId,
                PizzaId = model.PizzaId,
                TimeCreate = DateTime.Now,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderPizzaStatus.Принят
            };
            context.OrderPizzas.Add(orderPizza);
            context.SaveChanges();
            var visitor = context.Visitors.FirstOrDefault(x => x.Id == model.VisitorId);
            SendEmail(visitor.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} создан успешно", orderPizza.Id,
                orderPizza.TimeCreate.ToShortDateString()));
        }

        public void TakeOrderPizzaInWork(OrderPizzaBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    OrderPizza element = context.OrderPizzas.Include(x => x.Visitor).FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    var PizzaIngredients = context.PizzaIngredients
                                                .Where(rec => rec.PizzaId == element.PizzaId);
                    // списываем
                    foreach (var PizzaIngredient in PizzaIngredients)
                    {
                        int countOnFridges = PizzaIngredient.Count * element.Count;
                        var FridgeIngredients = context.FridgeIngredients
                                                    .Where(rec => rec.IngredientId == PizzaIngredient.IngredientId);
                        foreach (var FridgeIngredient in FridgeIngredients)
                        {
                            // компонентов на одном слкаде может не хватать
                            if (FridgeIngredient.Count >= countOnFridges)
                            {
                                FridgeIngredient.Count -= countOnFridges;
                                countOnFridges = 0;
                                context.SaveChanges();
                                break;
                            }
                            else
                            {
                                countOnFridges -= FridgeIngredient.Count;
                                FridgeIngredient.Count = 0;
                                context.SaveChanges();
                            }
                        }
                        if (countOnFridges > 0)
                        {
                            throw new Exception("Не достаточно компонента " +
                                PizzaIngredient.Ingredient.IngredientName + " требуется " +
                                PizzaIngredient.Count + ", не хватает " + countOnFridges);
                        }
                    }
                    element.CookId = model.CookId;
                    element.TimeDone = DateTime.Now;
                    element.Status = OrderPizzaStatus.Выполняется;
                    context.SaveChanges();
                    SendEmail(element.Visitor.Mail, "Оповещение по заказам",
                        string.Format("Заказ №{0} от {1} передеан в работу", element.Id, element.TimeCreate.ToShortDateString()));
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void FinishOrderPizza(int id)
        {
            OrderPizza element = context.OrderPizzas.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderPizzaStatus.Готов;
            context.SaveChanges();
            SendEmail(element.Visitor.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} передан на оплату", element.Id,
                element.TimeCreate.ToShortDateString()));
        }

        public void PayOrderPizza(int id)
        {
            OrderPizza element = context.OrderPizzas.FirstOrDefault(rec => rec.Id == id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.Status = OrderPizzaStatus.Оплачен;
            context.SaveChanges();
            SendEmail(element.Visitor.Mail, "Оповещение по заказам",
                string.Format("Заказ №{0} от {1} оплачен успешно", element.Id, element.TimeCreate.ToShortDateString()));
        }

        public void PutIngredientInFridge(FridgeIngredientBindingModel model)
        {
            FridgeIngredient element = context.FridgeIngredients
                                                .FirstOrDefault(rec => rec.FridgeId == model.FridgeId &&
                                                                    rec.IngredientId == model.IngredientId);
            if (element != null)
            {
                element.Count += model.Count;
            }
            else
            {
                context.FridgeIngredients.Add(new FridgeIngredient
                {
                    FridgeId = model.FridgeId,
                    IngredientId = model.IngredientId,
                    Count = model.Count
                });
            }
            context.SaveChanges();
        }

        private void SendEmail(string mailAddress, string subject, string text)
        {
            System.Net.Mail.MailMessage objMailMessage = new System.Net.Mail.MailMessage();
            SmtpClient objSmtpClient = null;

            try
            {
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailLogin"]);
                objMailMessage.To.Add(new MailAddress(mailAddress));
                objMailMessage.Subject = subject;
                objMailMessage.Body = text;
                objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                objSmtpClient = new SmtpClient("smtp.gmail.com", 587);
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.EnableSsl = true;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailLogin"],
                    ConfigurationManager.AppSettings["MailPassword"]);

                objSmtpClient.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objMailMessage = null;
                objSmtpClient = null;
            }
        }
    }
}
