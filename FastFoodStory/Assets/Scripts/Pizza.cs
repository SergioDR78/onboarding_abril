using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public class Pizza
    {
        public List<Ingredient> ingredients;
        public string name;
        //public Sprite image;

        /*public Pizza(List<Ingredient> ingredients, string name, Sprite image)
        {
            this.ingredients = ingredients;
            this.name = name;
            this.image = image;
        }*/

        public Pizza(List<Ingredient> ingredients, string name)
        {
            this.ingredients = ingredients;
            this.name = name;
        }

        internal bool hasingredient(string ingredientName)
        {
            return ingredients.Any(x=>x.name == ingredientName);
        }
    }
}
