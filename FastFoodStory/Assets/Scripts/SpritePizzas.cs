using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SpritePizzas
    {
        public List<PizzaSprite> Pizzas;
    }

    [SerializeField]
    public struct PizzaSprite
    {
        public string PizzaName;
        public Sprite Sprite;
    }
}
