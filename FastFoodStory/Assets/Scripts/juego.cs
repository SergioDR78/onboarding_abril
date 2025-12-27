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
            CustomerPositionManager.Instance.Initialize(customerPositions);
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

        CustomerDisplay ShowCustomer(CustomerData customer)
        {
            var customerPosition = CustomerPositionManager.Instance.GetAvailablePosition();
            if (customerPosition == null)
            {
                Debug.LogWarning("No hay posiciones disponibles para mostrar al cliente");
                return null;
            }
            
            var newCustomer = Instantiate(customer.Prefab, customerPosition.transform);
            newCustomer.SetCustomer(customer);
            return newCustomer;
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
                takeOrderButton.enabled = true;
                if (levels.CurrentLevel.CurrentOrder.MoveToNextPizza())
                {
                    // Hay más pizzas en el pedido actual
                }
                else
                {
                    // El pedido actual está completo, remover al cliente
                    RemoveCustomerFromCurrentOrder();

                    if (levels.CurrentLevel.IsCompleted())
                    {
                        StartCoroutine(AdvanceLevelAfterDelay(1.5f));
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

        IEnumerator AdvanceLevelAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            AdvanceLevel();
        }

        void RemoveCustomerFromCurrentOrder()
        {
            var currentOrder = levels.CurrentLevel.CurrentOrder;
            if (currentOrder.CustomerInstance != null)
            {
                var customerTransform = currentOrder.CustomerInstance.transform;
                if (customerTransform.parent != null)
                {
                    CustomerPositionManager.Instance.ReleasePosition(customerTransform.parent.gameObject);
                }

                // Activar animación de salida
                var animator = currentOrder.CustomerInstance.GetComponent<Animator>();
                if (animator != null)
                {
                    animator.SetBool("out", true);
                }

                // Destruir después de 1 segundo
                StartCoroutine(DestroyCustomerAfterDelay(currentOrder.CustomerInstance.gameObject, 1f));
            }
        }

        IEnumerator DestroyCustomerAfterDelay(GameObject customer, float delay)
        {
            yield return new WaitForSeconds(delay);
            DestroyImmediate(customer);
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
            CustomerPositionManager.Instance.ReleaseAllPositions();
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
                var instance = ShowCustomer(order.Customer); // Muestra un comensal
                order.SetCustomerInstance(instance);
                StartCoroutine(ShowPizzasAfterDelay(order, 1f));
            }));
        }

        IEnumerator ShowPizzasAfterDelay(OrderData order, float delay)
        {
            yield return new WaitForSeconds(delay);
            foreach (var pizza in order.Pizzas)
            {
                OrderPizza(pizza);
            }
        }

        IEnumerator TimerCallback(int durationInSeconds, Action action)
        {

            yield return new WaitForSeconds(durationInSeconds);
            action.Invoke();
        }
    }
}