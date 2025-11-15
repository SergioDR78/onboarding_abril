using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "PizzaData", menuName = "ScriptableObjects/PizzaData")]
    public class PizzaData : ScriptableObject
    {
        public string PizzaName;
        public Sprite PizzaImage;
        public PizzaDisplay Prefab;
        public List<IngredientData> Ingredients = new List<IngredientData>();
        public List<IngredientData> IngredientsUsed = new List<IngredientData>();

        public bool IsCompleted()
        {
            return Ingredients.Count == IngredientsUsed.Count;
        }

        public bool hasIngredient(IngredientData ingredient)
        {
            foreach (var item in Ingredients)
            {
                if (ingredient.Name == item.Name)
                {
                    if (IngredientsUsed.Contains(ingredient))
                    {
                        return false; // Ingredient already used
                    }
                    IngredientsUsed.Add(ingredient);
                    Prefab.DisplayIngredient(ingredient);
                    return true;
                }
            }
            return false;
        }
    }
}
