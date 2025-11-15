using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PizzaDisplay : MonoBehaviour
    {
        public IngredientDisplay[] IngredientDisplays;

        internal void DisplayIngredient(IngredientData ingredient)
        {
            Debug.Log("Displaying ingredient: " + ingredient.Name);
            IngredientDisplay display = Array.Find(IngredientDisplays, x => x.Ingredient.Name == ingredient.Name);
            if (display != null)
            {
                Debug.Log("Found display for ingredient: " + ingredient.Name);
                display.IngredientObject.SetActive(true);
            }
            else
            {
                Debug.LogWarning("No display found for ingredient: " + ingredient.Name);
            }                
        }
    }

    [Serializable]
    public class IngredientDisplay
    {
        public IngredientData Ingredient;
        public GameObject IngredientObject;
    }
}
