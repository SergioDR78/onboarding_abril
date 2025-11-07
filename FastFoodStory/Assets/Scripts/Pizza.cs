using System.Collections.Generic;
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
    }
}
