using Assets.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [Header("Dialogs")]
    public DialogMultiple dialogMultiple;


    void Start()
    {
        StartLevel();
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
        yield return new WaitForSeconds(1f);
        timerText.text = "0";
        LostLevel();
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

    void LostLevel() 
    {
        dialogMultiple.title = "Se te acabo el tiempo!";
        dialogMultiple.titleButton1 = "Reintentar";
        dialogMultiple.onButton1Click = () => {
            // Lógica para reintentar
            StartLevel();
        };
        dialogMultiple.titleButton2 = "Salir";
        dialogMultiple.onButton2Click = () => {
            // Lógica para salir
            SceneManager.LoadScene("Splash");
        };
        dialogMultiple.Show();
    }

    void StartLevel() 
    {
        // Lógica para iniciar el nivel
        StartCoroutine(Countdown()); // Inicia la cuenta regresiva
        ShowCustomer(); // Muestra un comensal
        OrderPizza(); // Ordena una pizza
    }
}
