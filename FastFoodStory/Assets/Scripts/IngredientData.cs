using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "IngredientData", menuName = "ScriptableObjects/IngredientData")]
    public class IngredientData : ScriptableObject
    {
        public string Name;
        public Sprite Image;
        public bool IsSelected;
    }
}