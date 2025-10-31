using UnityEngine;
using System.Collections;

public class ComensalSpawner : MonoBehaviour
{
    public GameObject[] comensales; // Array con los 3 comensales

    void Start()
    {
        // Desactiva todos los comensales al inicio
        foreach (GameObject comensal in comensales)
        {
            comensal.SetActive(false);
        }

        // Inicia la corrutina para activar uno cada minuto
        StartCoroutine(SpawnComensalCadaMinuto());
    }

    IEnumerator SpawnComensalCadaMinuto()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f); // Espera 1 minuto

            // Desactiva todos antes de activar uno nuevo
            foreach (GameObject comensal in comensales)
            {
                comensal.SetActive(false);
            }

            int index = Random.Range(0, comensales.Length); // Elige uno al azar
            comensales[index].SetActive(true); // Activa el comensal elegido
        }
    }
}