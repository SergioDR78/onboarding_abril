using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CustomerDisplay : MonoBehaviour
    {
        public Image image;

        public void SetCustomer(CustomerData customerData)
        {
            if (customerData != null && customerData.CustomerImage != null)
            {
                image.sprite = customerData.CustomerImage;
            }
        }
    }
}
