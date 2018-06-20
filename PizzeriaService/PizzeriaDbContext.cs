using PizzeriaModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService
{
    [Table("PizzeriaDatabase")]
    public class PizzeriaDbContext : DbContext
    {
        public PizzeriaDbContext()
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Visitor> Visitors { get; set; }

        public virtual DbSet<Ingredient> Ingredients { get; set; }

        public virtual DbSet<Cook> Cooks { get; set; }

        public virtual DbSet<OrderPizza> OrderPizzas { get; set; }

        public virtual DbSet<Pizza> Pizzas { get; set; }

        public virtual DbSet<PizzaIngredient> PizzaIngredients { get; set; }

        public virtual DbSet<Fridge> Fridges { get; set; }

        public virtual DbSet<FridgeIngredient> FridgeIngredients { get; set; }

        public virtual DbSet<MessageInfo> MessageInfos { get; set; }

        /// <summary>
        /// Перегружаем метод созранения изменений. Если возникла ошибка - очищаем все изменения
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }
    }
}
