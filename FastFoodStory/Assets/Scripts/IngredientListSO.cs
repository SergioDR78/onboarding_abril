using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "NewIngredientList", menuName = "ScriptableObjects/IngredientList")]
    public class IngredientListSO : ScriptableObject
    {
        public List<IngredientData> ingredients;
    }

}