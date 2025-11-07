using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class DialogMultiple : MonoBehaviour
    {
        public string title;
        public TMP_Text titleText;

        [Header("Button 1")]
        public string titleButton1;
        public Button button1;
        public Action onButton1Click;

        [Header("Button 1")]
        public Button button2;
        public string titleButton2;
        public Action onButton2Click;

        public void Show()
        {
            titleText.text = title;
            button1.GetComponentInChildren<TMP_Text>().text = titleButton1;
            button2.GetComponentInChildren<TMP_Text>().text = titleButton2;
            gameObject.SetActive(true);
            button1.onClick.AddListener( () => onButton1Click.Invoke());
            button2.onClick.AddListener( () => onButton2Click.Invoke());
        }
    }
}
