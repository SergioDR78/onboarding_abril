using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{

    public class juego : MonoBehaviour
    {
        public TMP_Text timerText;
        public TMP_Text levelText;
        public GameObject PizzaObject;

        [Header("Order")]
        public Dialog dialog;
        public Sprite pizzaType;
        public GameObject OrderList;
        public OrderDisplay OrderPrefab;

        [Header("Ingredients")]
        public List<ingredientpicker> IngredientsPicker;

        [Header("Customer")]
        public List<GameObject> customerPositions;

        [Header("Dialogs")]
        public DialogMultiple dialogMultiple;

        [Header("Data")]
        public LevelsData levels;

        public Button deliverButton;
        public Button takeOrderButton;

        public FeedbackDisplay feedbackDisplay;

        Coroutine countdownCoroutine;

        void Awake()
        {
            Reset();
        }

        private void Reset()
        {
            levels.ResetLevels();
            timerText.text = "";
            levelText.text = "";
        }

        void Start()
        {
            StartGame();
        }

        void StartGame()
        {
            ResetTimer(levels.CurrentLevel.durationInSeconds);
            deliverButton.onClick.AddListener(DeliverPizza);
            takeOrderButton.onClick.AddListener(TakeOrder);
            StartLevel();
        }

        void ResetTimer(int durationInSeconds)
        {
            timerText.text = durationInSeconds.ToString();
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }
        }

        IEnumerator Countdown(int durationInSeconds)
        {
            int currentTime = durationInSeconds;

            while (currentTime > 0)
            {
                yield return new WaitForSeconds(1f);
                timerText.text = currentTime.ToString();
                currentTime--;
            }
            yield return new WaitForSeconds(1f);
            timerText.text = "0";
            LostLevel();
        }

        void AdvanceLevel() 
        {
            levels.AdvanceToNextLevel();
            if (levels.IsCompleted())
            {
                SceneManager.LoadScene("Win");
                return;
            }
            ResetTimer(levels.CurrentLevel.durationInSeconds);
            StartLevel();
        }

        void ShowCustomer(CustomerData customer)
        {
            var customerPosition = customerPositions[UnityEngine.Random.Range(0, customerPositions.Count)];
            var newCustomer = Instantiate(customer.Prefab, customerPosition.transform);
            newCustomer.SetCustomer(customer);
        }

        void OrderPizza(PizzaData pizza)
        {
            var orderDisplay = Instantiate(OrderPrefab, OrderList.transform);
            orderDisplay.SetOrder(pizza);
        }


        void DisplayOrdererPizza(string pizzaName, Sprite pizzaType)
        {
            dialog.title = "Quiero una pizza de " + pizzaName;
            dialog.sprite = pizzaType;
            dialog.Show();
        }

        void DeliverPizza()
        {
            if (levels.CurrentLevel.CurrentOrder.GetCurrentPizza().IsCompleted())
            {
                feedbackDisplay.ShowSuccess();
                ClearPizza();
                deliverButton.enabled = false;
                if (levels.CurrentLevel.CurrentOrder.MoveToNextPizza())
                {
                    // Hay más pizzas en el pedido actual
                    takeOrderButton.enabled = true;
                }
                else
                {
                    if (levels.CurrentLevel.IsCompleted())
                    {
                        AdvanceLevel();
                        return;
                    }
                    if (levels.CurrentLevel.AdvanceToNextOrder())
                    {
                        // Hay más pedidos en el nivel actual
                    }
                    else
                    {
                        // No hay más pedidos en el nivel actual
                    }
                }
            }
            else
            {
                feedbackDisplay.ShowFailure();
            }
        }

        void LostLevel()
        {
            dialogMultiple.title = "Se te acabo el tiempo!";
            dialogMultiple.titleButton1 = "Reintentar";
            dialogMultiple.onButton1Click = () =>
            {
                // Lógica para reintentar
                StartLevel();
            };
            dialogMultiple.titleButton2 = "Salir";
            dialogMultiple.onButton2Click = () =>
            {
                // Lógica para salir
                SceneManager.LoadScene("Splash");
            };
            dialogMultiple.Show();
        }

        void ClearLevel()
        {
            ClearPizza();
            foreach (Transform child in OrderList.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var customerPosition in customerPositions)
            {
                foreach (Transform child in customerPosition.transform)
                {
                    Destroy(child.gameObject);
                }
            }
            deliverButton.enabled = false;
            takeOrderButton.enabled = true;
        }

        void ClearPizza()
        {
            foreach (Transform child in PizzaObject.transform)
            {
                Destroy(child.gameObject);
            }
        }

        void StartLevel()
        {
            ClearLevel();
            levels.CurrentLevel.ResetLevel();
            countdownCoroutine = StartCoroutine(Countdown(levels.CurrentLevel.durationInSeconds));
            levelText.text = "Nivel " + (levels.CurrentLevelIndex + 1).ToString();
            foreach (var order in levels.CurrentLevel.Orders)
            {
                CreateOrder(order);
            }
        }

        void TakeOrder()
        {
            takeOrderButton.enabled = false;
            var pizza = levels.CurrentLevel.CurrentOrder.GetCurrentPizza();
            if (pizza == null) return;
            deliverButton.enabled = true;
            var newPizza = Instantiate(pizza.Prefab, PizzaObject.transform);
            pizza.Prefab = newPizza;
            IngredientsPicker.ForEach(x => x.Pizza = pizza);
            DisplayOrdererPizza(pizza.PizzaName, pizza.PizzaImage);
        }

        void CreateOrder(OrderData order)
        {
            StartCoroutine(TimerCallback(order.durationInSecondsToAppear, () =>
            {
                ShowCustomer(order.Customer); // Muestra un comensal
                foreach (var pizza in order.Pizzas)
                {
                    OrderPizza(pizza);
                }                    
            }));
        }

        IEnumerator TimerCallback(int durationInSeconds, Action action)
        {

            yield return new WaitForSeconds(durationInSeconds);
            action.Invoke();
        }
    }
}