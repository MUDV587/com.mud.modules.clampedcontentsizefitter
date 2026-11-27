using UnityEngine;
using UnityEngine.UI;

namespace MUD.Modules.ClampedContentSizeFitter.Samples
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(ClampedContentSizeFitter))]
    public class ClampedContentSizeFitterDemo : MonoBehaviour
    {
        [SerializeField] private ClampedContentSizeFitter fitter;
        [SerializeField] private RectTransform target;

        private void Reset()
        {
            fitter = GetComponent<ClampedContentSizeFitter>();
            target = (RectTransform)transform;
        }

        private void Awake()
        {
            if (fitter == null)
                fitter = GetComponent<ClampedContentSizeFitter>();
            if (target == null)
                target = (RectTransform)transform;

            // Fit to preferred size, but clamp the final width/height.
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            fitter.minWidth = 100f;
            fitter.maxWidth = 400f;
            fitter.minHeight = 40f;
            fitter.maxHeight = 200f;
        }

        [ContextMenu("Apply Clamp")]
        private void ApplyClamp()
        {
            if (fitter == null)
                return;

            fitter.SetLayoutHorizontal();
            fitter.SetLayoutVertical();
            Debug.Log($"Clamped size: {target.rect.width} x {target.rect.height}", this);
        }
    }
}
