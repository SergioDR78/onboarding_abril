using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "CustomerData", menuName = "ScriptableObjects/CustomerData")]
    public class CustomerData : ScriptableObject
    {
        public string CustomerName;
        public Sprite CustomerImage;
        public CustomerDisplay Prefab;
    }
}
