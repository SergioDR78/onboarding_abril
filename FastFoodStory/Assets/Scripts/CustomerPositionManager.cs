using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CustomerPositionManager
    {
        private Dictionary<GameObject, bool> positionAvailability = new Dictionary<GameObject, bool>();
        private static CustomerPositionManager _instance = new CustomerPositionManager();

        private CustomerPositionManager() { }

        public static CustomerPositionManager Instance { get { return _instance; }  }

        public void Initialize(List<GameObject> positions)
        {
            positionAvailability.Clear();
            foreach (var position in positions)
            {
                positionAvailability[position] = true;
            }
        }

        public GameObject GetAvailablePosition()
        {
            List<GameObject> availablePositions = new List<GameObject>();
            
            foreach (var kvp in positionAvailability)
            {
                if (kvp.Value)
                {
                    availablePositions.Add(kvp.Key);
                }
            }

            if (availablePositions.Count == 0)
                return null;

            var selectedPosition = availablePositions[Random.Range(0, availablePositions.Count)];
            positionAvailability[selectedPosition] = false;
            return selectedPosition;
        }

        public void ReleasePosition(GameObject position)
        {
            if (positionAvailability.ContainsKey(position))
            {
                positionAvailability[position] = true;
            }
        }

        public void ReleaseAllPositions()
        {
            var keys = new List<GameObject>(positionAvailability.Keys);
            foreach (var key in keys)
            {
                positionAvailability[key] = true;
            }
        }
    }
}