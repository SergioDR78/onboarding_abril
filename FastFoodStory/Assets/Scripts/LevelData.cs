using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
    [Serializable]
    public class LevelData : ScriptableObject
    {
        public string levelName;
        public int durationInSeconds;
        public List<OrderData> Orders;

        public int CurrentOrderIndex = 0;
        public OrderData CurrentOrder => Orders[CurrentOrderIndex];

        public bool AdvanceToNextOrder()
        {
            if (CurrentOrderIndex < Orders.Count - 1)
            {
                CurrentOrderIndex++;
                return true;
            }
            return false;
        }

        public bool IsCompleted()        
        {
            return Orders.TrueForAll(x => x.IsCompleted());
        }

        internal void ResetLevel()
        {
            foreach (var item in Orders)
            {
                item.Reset();
            }
        }
    }
}
