using System;
using PizzeriaModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaService
{
    class DataListSingleton
    {
        private static DataListSingleton instance;

        public List<Visitor> Visitors { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<Cook> Cooks { get; set; }

        public List<OrderPizza> OrderPizzas { get; set; }

        public List<Pizza> Pizzas { get; set; }

        public List<PizzaIngredient> PizzaIngredients { get; set; }

        public List<Fridge> Fridges { get; set; }

        public List<FridgeIngredient> FridgeIngredients { get; set; }

        private DataListSingleton()
        {
            Visitors = new List<Visitor>();
            Ingredients = new List<Ingredient>();
            Cooks = new List<Cook>();
            OrderPizzas = new List<OrderPizza>();
            Pizzas = new List<Pizza>();
            PizzaIngredients = new List<PizzaIngredient>();
            Fridges = new List<Fridge>();
            FridgeIngredients = new List<FridgeIngredient>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }

            return instance;
        }

}
}
