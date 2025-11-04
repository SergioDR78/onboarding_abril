using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Dialog : MonoBehaviour
    {
        public string title;
        public Sprite sprite;

        public TMP_Text titleText;
        public Image messageImage;

        public void Show()
        {
            titleText.text = title;
            messageImage.sprite = sprite;
            gameObject.SetActive(true);
        }
    }
}
