using UnityEngine;
using UnityEngine.UI;

namespace MUD.Modules.ClampedContentSizeFitter
{
    [AddComponentMenu("Layout/Clamped Content Size Fitter", 142)]
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class ClampedContentSizeFitter : ContentSizeFitter
    {
        RectTransform m_RectTransform;

        [SerializeField] private float m_MinWidth = -1;
        [SerializeField] private float m_MaxWidth = -1;
        [SerializeField] private float m_MinHeight = -1;
        [SerializeField] private float m_MaxHeight = -1;

        public float minWidth
        {
            get => m_MinWidth;
            set { if (m_MinWidth == value) return; m_MinWidth = value; SetDirty(); }
        }

        public float maxWidth
        {
            get => m_MaxWidth;
            set { if (m_MaxWidth == value) return; m_MaxWidth = value; SetDirty(); }
        }

        public float minHeight
        {
            get => m_MinHeight;
            set { if (m_MinHeight == value) return; m_MinHeight = value; SetDirty(); }
        }

        public float maxHeight
        {
            get => m_MaxHeight;
            set { if (m_MaxHeight == value) return; m_MaxHeight = value; SetDirty(); }
        }

        public override void SetLayoutHorizontal()
        {
            base.SetLayoutHorizontal();
            ClampAxis(0);
        }

        public override void SetLayoutVertical()
        {
            base.SetLayoutVertical();
            ClampAxis(1);
        }

        void ClampAxis(int axis)
        {
            var fitting = axis == 0 ? horizontalFit : verticalFit;
            if (fitting == FitMode.Unconstrained)
                return;

            float min = axis == 0 ? m_MinWidth : m_MinHeight;
            float max = axis == 0 ? m_MaxWidth : m_MaxHeight;
            if (min >= 0 && max >= 0 && min > max)
                max = min;

            if (m_RectTransform == null)
                m_RectTransform = (RectTransform)transform;

            float size = m_RectTransform.sizeDelta[axis];
            if (min >= 0) size = Mathf.Max(size, min);
            if (max >= 0) size = Mathf.Min(size, max);
            m_RectTransform.SetSizeWithCurrentAnchors((RectTransform.Axis)axis, size);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            if (m_MinWidth >= 0 && m_MaxWidth >= 0 && m_MinWidth > m_MaxWidth) m_MaxWidth = m_MinWidth;
            if (m_MinHeight >= 0 && m_MaxHeight >= 0 && m_MinHeight > m_MaxHeight) m_MaxHeight = m_MinHeight;
            base.OnValidate();
        }
#endif
    }
}
