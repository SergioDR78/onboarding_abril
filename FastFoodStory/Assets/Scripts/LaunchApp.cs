using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Scripts
{

    public class LaunchApp : MonoBehaviour
    {


        public void gotogameplay()
        {
            SceneManager.LoadScene("Gameplay");

        }

        public void gotolobby()
        {
            SceneManager.LoadScene("Splash");

        }

    }
}