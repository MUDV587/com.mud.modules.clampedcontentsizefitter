using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MUD.Modules.ClampedContentSizeFitter;

namespace MUD.Modules.ClampedContentSizeFitter.Samples
{
    public class ClampedContentSizeFitterDemoController : MonoBehaviour
    {
        [SerializeField] private string title = "Demo";
        [SerializeField] private Text titleLabel;
        [SerializeField] private Text label;
        [SerializeField] private Text info;
        [SerializeField] private ContentSizeFitter fitter;
        [SerializeField] private string[] messages;
        [SerializeField] private bool autoCycle = false;
        [SerializeField] private float cycleInterval = 1.2f;
        [SerializeField] private Color normalInfoColor = new Color(0.45f, 0.48f, 0.52f, 1f);
        [SerializeField] private Color hitInfoColor = new Color(0.78f, 0.36f, 0.20f, 1f);
        [SerializeField] private Text widthDimText;
        [SerializeField] private Text heightDimText;
        [SerializeField] private RectTransform widthDimRoot;
        [SerializeField] private RectTransform heightDimRoot;
        [SerializeField] private Outline contentOutline;
        [SerializeField] private Color normalOutlineColor = new Color(0.16f, 0.52f, 0.55f, 0.35f);
        [SerializeField] private Color hitOutlineColor = new Color(0.78f, 0.36f, 0.20f, 0.75f);
        [SerializeField] private LayoutElement labelLayoutElement;
        [SerializeField] private Text overflowLabel;

        private int currentIndex;

        private static readonly string[] DefaultMessages =
        {
            "Short",
            "A bit longer text",
            "This text is much longer and should show the clamp behavior clearly",
            "Line 1\nLine 2\nLine 3\nLine 4\nLine 5\nLine 6"
        };

        private void Awake()
        {
            if (fitter == null)
                fitter = GetComponentInChildren<ContentSizeFitter>();
        }

        private void Start()
        {
            if (titleLabel != null)
                titleLabel.text = title;
            ApplyContent(0);
            if (autoCycle)
                StartCoroutine(AutoCycle());
        }

        private IEnumerator AutoCycle()
        {
            var wait = new WaitForSeconds(cycleInterval);
            while (autoCycle)
            {
                yield return wait;
                if (!autoCycle) yield break;
                SetContent(currentIndex + 1, true);
            }
        }

        public void SetContent(int index)
        {
            SetContent(index, false);
        }

        private void SetContent(int index, bool fromAuto)
        {
            if (!fromAuto)
                autoCycle = false;
            ApplyContent(index);
        }

        private void ApplyContent(int index)
        {
            var source = messages != null && messages.Length > 0 ? messages : DefaultMessages;
            if (source.Length == 0) return;
            currentIndex = ((index % source.Length) + source.Length) % source.Length;
            if (label != null)
                label.text = source[currentIndex];
        }

        private void LateUpdate()
        {
            if (fitter == null)
                return;

            var rect = ((RectTransform)fitter.transform).rect;
            float minW = -1, maxW = -1, minH = -1, maxH = -1;
            var clamped = fitter as ClampedContentSizeFitter;
            if (clamped != null)
            {
                minW = clamped.minWidth;
                maxW = clamped.maxWidth;
                minH = clamped.minHeight;
                maxH = clamped.maxHeight;
            }

            string widthState = AxisState(rect.width, fitter.horizontalFit, minW, maxW, clamped != null);
            string heightState = AxisState(rect.height, fitter.verticalFit, minH, maxH, clamped != null);
            bool hit = widthState.Contains("Hit") || heightState.Contains("Hit");

            if (info != null)
            {
                info.color = hit ? hitInfoColor : normalInfoColor;
                info.text =
                    $"Size: {rect.width:0} x {rect.height:0}\n" +
                    $"H: {fitter.horizontalFit}  Min {FormatLimit(minW)}  Max {FormatLimit(maxW)}  {widthState}\n" +
                    $"V: {fitter.verticalFit}  Min {FormatLimit(minH)}  Max {FormatLimit(maxH)}  {heightState}";
            }

            if (contentOutline != null)
                contentOutline.effectColor = hit ? hitOutlineColor : normalOutlineColor;

            var areaRect = widthDimRoot != null ? widthDimRoot.parent as RectTransform : null;
            float halfW = areaRect != null ? areaRect.rect.width / 2f : 400f;
            float halfH = areaRect != null ? areaRect.rect.height / 2f : 300f;

            if (labelLayoutElement != null)
            {
                if (fitter.horizontalFit != ContentSizeFitter.FitMode.Unconstrained)
                {
                    float areaW = areaRect != null ? areaRect.rect.width : 600f;
                    float wrapW = maxW >= 0 ? maxW - 40f : areaW - 48f;
                    labelLayoutElement.preferredWidth = Mathf.Max(40f, wrapW);
                }
                else
                {
                    labelLayoutElement.preferredWidth = -1f;
                }
            }

            if (widthDimText != null)
                widthDimText.text = $"W {rect.width:0}";
            if (heightDimText != null)
                heightDimText.text = $"H {rect.height:0}";

            if (widthDimRoot != null)
            {
                widthDimRoot.sizeDelta = new Vector2(Mathf.Max(40f, rect.width), widthDimRoot.sizeDelta.y);
                float yBelow = -rect.height / 2f - 12f;
                float yAbove = rect.height / 2f + 12f;
                float y = yBelow;
                if (yBelow - 12f < -halfH + 8f) y = yAbove;
                y = Mathf.Clamp(y, -halfH + 12f, halfH - 12f);
                widthDimRoot.anchoredPosition = new Vector2(0, y);
            }
            if (heightDimRoot != null)
            {
                heightDimRoot.sizeDelta = new Vector2(heightDimRoot.sizeDelta.x, Mathf.Max(30f, rect.height));
                float xLeft = -rect.width / 2f - 14f;
                float xRight = rect.width / 2f + 14f;
                float x = xLeft;
                if (xLeft - 22f < -halfW + 8f) x = xRight;
                x = Mathf.Clamp(x, -halfW + 22f, halfW - 22f);
                heightDimRoot.anchoredPosition = new Vector2(x, 0);
            }

            if (overflowLabel != null && areaRect != null)
            {
                bool overflow = rect.width > areaRect.rect.width - 8f || rect.height > areaRect.rect.height - 8f;
                overflowLabel.gameObject.SetActive(overflow);
                if (overflow)
                    overflowLabel.text = clamped == null ? "Overflows display area (no clamp)" : "Overflows display area";
            }
        }

        private static string AxisState(float size, ContentSizeFitter.FitMode fit, float min, float max, bool isClamped)
        {
            if (fit == ContentSizeFitter.FitMode.Unconstrained)
                return "[Unconstrained]";

            if (!isClamped)
                return "[No Clamp]";

            bool hasMin = min >= 0;
            bool hasMax = max >= 0;
            if (!hasMin && !hasMax)
                return "[No Clamp]";

            bool hitMin = hasMin && size <= min + 0.5f;
            bool hitMax = hasMax && size >= max - 0.5f;
            if (hitMin && hitMax) return "[Hit Min+Max]";
            if (hitMax) return "[Hit Max]";
            if (hitMin) return "[Hit Min]";
            return "[Within Range]";
        }

        private static string FormatLimit(float value)
        {
            return value >= 0 ? value.ToString("0") : "off";
        }
    }
}
