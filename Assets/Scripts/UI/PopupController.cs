using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float displayDuration = 3f;
    void Start()
    {
        GetComponent<Animator>();
    }

    // Update is called once per frame
    public void ShowPopup(string message)
    {
        System.Collections.IEnumerator ShowPopupCoroutine(string message)
        {
            messageText.text = message;
            animator.SetTrigger("Show");
            yield return new WaitForSeconds(displayDuration);
            animator.SetTrigger("Hide");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            Destroy(gameObject);
        }
        StartCoroutine(ShowPopupCoroutine(message));
    }
}
