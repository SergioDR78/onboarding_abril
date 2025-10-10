using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LaunchApp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LoadGameplayScene());
    }

    // Coroutine to wait for 2 seconds and load the Gameplay scene
    private IEnumerator LoadGameplayScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Gameplay");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
