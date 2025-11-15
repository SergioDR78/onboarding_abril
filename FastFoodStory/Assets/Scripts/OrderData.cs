using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "Order", menuName = "ScriptableObjects/OrderData", order = 1)]
    public class OrderData : ScriptableObject
    {
        public List<PizzaData> FactoryPizza = new List<PizzaData>();
        public List<CustomerData> FactoryCustomer = new List<CustomerData>();

        public int durationInSecondsToAppear;
        public string OrderID;
        public List<PizzaData> Pizzas = new List<PizzaData>();
        public CustomerData Customer;
        public float TotalPrice;
        public DateTime OrderDate;
        public int Quantity;
        public int currentPizzaIndex = 0;
        public PizzaData GetCurrentPizza()
        {
            if (currentPizzaIndex < Pizzas.Count)
            {
                return Pizzas[currentPizzaIndex];
            }
            return null;
        }
        public bool MoveToNextPizza()
        {
            if (currentPizzaIndex < Pizzas.Count - 1)
            {
                currentPizzaIndex++;
                return true;
            }
            return false;
        }

        public bool IsCompleted() 
        {
            return Pizzas.TrueForAll( pizza => pizza.IsCompleted());
        }

        internal void Reset()
        {
            currentPizzaIndex = 0;
            Pizzas.Clear();
            SetCustomerRandom();
            SetPizzasRandom();
        }

        void SetCustomerRandom() 
        {
            if (FactoryCustomer.Count == 0) return;
            int randomIndex = UnityEngine.Random.Range(0, FactoryCustomer.Count);
            Customer = FactoryCustomer[randomIndex];
        }

        void SetPizzasRandom() 
        {
            for (int i = 0; i < Quantity; i++)
            {
                if (FactoryPizza.Count == 0) return;
                int randomIndex = UnityEngine.Random.Range(0, FactoryPizza.Count);
                var newPizza = Instantiate(FactoryPizza[randomIndex]);
                Pizzas.Add(newPizza);
            }
        }
    }
}
