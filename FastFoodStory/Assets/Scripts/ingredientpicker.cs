using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ingredientpicker : MonoBehaviour
    {

        // Use this for initialization

        public Button button;
        public Pizza Pizza;
        public string IngredientName;
        public GameObject PizzaObject;
        public GameObject IngredientPrefab;
        public IngredientListSO SpriteIngredients;
        void Start()
        {
            button.onClick.AddListener(() =>click());
        }

        // Update is called once per frame
       void click(){
            Debug.Log("Click");
            var ingredient = new Ingredient(IngredientName);
            Debug.Log(IngredientName);


            var contains = Pizza.hasingredient(IngredientName);
            if(contains){
                Debug.Log("contiene");
                var data = SpriteIngredients.ingredients.First(x=>x.name == ingredient.name);
                var child = Instantiate(IngredientPrefab, PizzaObject.transform);
                child.GetComponent<Image>().sprite = data.ingredient;
        }

    }

    }
}