# MUD Clamped Content Size Fitter

`ClampedContentSizeFitter` extends Unity UI's `ContentSizeFitter` with optional min/max width and height clamping.

It keeps the familiar ContentSizeFitter workflow: choose a fit mode per axis, then optionally clamp the final size.

![Horizontal Max](images/demo-horizontal-max.png)

## Highlights

- Inherits from `UnityEngine.UI.ContentSizeFitter`
- Optional min/max clamp per axis
- Clamps only fitted axes (`Unconstrained` axes are untouched)
- Custom inspector with enable toggles
- Multi-page side-by-side comparison sample scene with min/max range frames and hit-status feedback

## Package

- **Name**: `com.mud.modules.clampedcontentsizefitter`
- **Unity**: 2022.3 LTS+
- **Dependency**: `com.unity.ugui`
