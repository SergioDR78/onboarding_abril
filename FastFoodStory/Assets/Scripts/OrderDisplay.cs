using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class OrderDisplay : MonoBehaviour
    {
        public GameObject panel;
        public Image image;
        public TMP_Text text;
        public Button button;

        private void Start()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        void OnButtonClick()
        {
            panel.SetActive(!panel.activeSelf);
        }

        public void SetOrder(PizzaData pizza)
        {
            image.sprite = pizza.PizzaImage;
            text.text = pizza.PizzaName;
        }
    }
}
