using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;

public class juego : MonoBehaviour
{
    public int startTimeInSeconds = 10;
    public TMP_Text timerText;
    
    public GameObject pizzaPrefab;
    
    [Header("Order")]
    public Dialog dialog;
    public Sprite pizzaType;
    


    [Header("Customer")]
    public GameObject customer;
    public GameObject customerPosition;

    void Start()
    {
        StartCoroutine(Countdown()); // Inicia la cuenta regresiva
        ShowCustomer(); // Muestra un comensal
        OrderPizza(); // Ordena una pizza
    }

    IEnumerator Countdown()
    {
        int currentTime = startTimeInSeconds;

        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            timerText.text = currentTime.ToString();
            currentTime--;
        }        
    }

    void ShowCustomer()
    {
        Instantiate(customer, customerPosition.transform);
    }

    void OrderPizza() 
    {
        dialog.title = "Quiero una pizza de jamon y queso";
        dialog.sprite = pizzaType;
        dialog.Show();
        // Lógica para ordenar pizza
    }
}
