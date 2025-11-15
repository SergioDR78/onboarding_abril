using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ingredientpicker : MonoBehaviour
    {
        public Button button;
        public PizzaData Pizza;
        public IngredientData Ingredient;

        void Start()
        {
            button.onClick.AddListener(() =>click());
        }
       
       void click()
       {
            Pizza.hasIngredient(Ingredient);
       }
    }
}