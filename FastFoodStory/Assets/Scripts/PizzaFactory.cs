using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class PizzaFactory
    {
        public static SpritePizzas SpritePizzas;
        public static Pizza CreateHamAndCheesePizza()
        {
            var ingredients = new Ingredient[] { new Ingredient("Ham"), new Ingredient("Cheese") };
            return new Pizza(ingredients.ToList(), "jamon y queso");
        }

        public static Pizza CreateHamAndTomatoPizza()
        {
            var ingredients = new Ingredient[] { new Ingredient("Ham"), new Ingredient("Tomato") };
            //var sprite = _getSpritePizzaByName("jamon y tomate");
            return new Pizza(ingredients.ToList(), "jamon y tomate");
        }
        public static Sprite _getSpritePizzaByName(string pizzaName)
        {
            var pizzaSprite = SpritePizzas.Pizzas.FirstOrDefault(p => p.PizzaName == pizzaName);
            return pizzaSprite.Sprite;
        }
    }

}