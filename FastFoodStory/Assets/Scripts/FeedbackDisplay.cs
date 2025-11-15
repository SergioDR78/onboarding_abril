using UnityEngine;

namespace Assets.Scripts
{
    public class FeedbackDisplay : MonoBehaviour
    {
        public GameObject SuccessFeedback;
        public GameObject FailureFeedback;
        public int FeedbackDurationInSeconds = 2;

        public void ShowSuccess()
        {
            SuccessFeedback.SetActive(true);
            FailureFeedback.SetActive(false);
            StartCoroutine(HideFeedbackAfterDelay());
        }
        public void ShowFailure()
        {
            SuccessFeedback.SetActive(false);
            FailureFeedback.SetActive(true);
            StartCoroutine(HideFeedbackAfterDelay());
        }

        private System.Collections.IEnumerator HideFeedbackAfterDelay()
        {
            yield return new WaitForSeconds(FeedbackDurationInSeconds);
            SuccessFeedback.SetActive(false);
            FailureFeedback.SetActive(false);
        }
    }
}
