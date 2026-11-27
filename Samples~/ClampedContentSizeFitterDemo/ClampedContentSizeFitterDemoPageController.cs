using UnityEngine;
using UnityEngine.UI;

namespace MUD.Modules.ClampedContentSizeFitter.Samples
{
    public class ClampedContentSizeFitterDemoPageController : MonoBehaviour
    {
        [SerializeField] private GameObject[] pages;
        [SerializeField] private Text pageTitle;
        [SerializeField] private Text pageDescription;
        [SerializeField] private Button prevButton;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button[] contentButtons;
        [SerializeField] private string[] titles;
        [SerializeField, TextArea] private string[] descriptions;

        private int index;

        private void Awake()
        {
            if (prevButton != null) prevButton.onClick.AddListener(Prev);
            if (nextButton != null) nextButton.onClick.AddListener(Next);
            if (contentButtons != null)
            {
                for (int i = 0; i < contentButtons.Length; i++)
                {
                    int captured = i;
                    if (contentButtons[i] != null)
                        contentButtons[i].onClick.AddListener(() => SetContent(captured));
                }
            }
            Show(0);
        }

        private void SetContent(int contentIndex)
        {
            if (pages == null) return;
            for (int i = 0; i < pages.Length; i++)
            {
                if (pages[i] == null) continue;
                var controllers = pages[i].GetComponentsInChildren<ClampedContentSizeFitterDemoController>(true);
                for (int j = 0; j < controllers.Length; j++)
                    controllers[j].SetContent(contentIndex);
            }
        }

        private void Prev()
        {
            Show(index - 1);
        }

        private void Next()
        {
            Show(index + 1);
        }

        private void Show(int nextIndex)
        {
            if (pages == null || pages.Length == 0)
                return;

            index = Mathf.Clamp(nextIndex, 0, pages.Length - 1);
            for (int i = 0; i < pages.Length; i++)
            {
                if (pages[i] != null)
                    pages[i].SetActive(i == index);
            }

            if (pageTitle != null && titles != null && index < titles.Length)
                pageTitle.text = titles[index];

            if (pageDescription != null && descriptions != null && index < descriptions.Length)
                pageDescription.text = descriptions[index];

            if (prevButton != null) prevButton.interactable = index > 0;
            if (nextButton != null) nextButton.interactable = index < pages.Length - 1;
        }
    }
}
