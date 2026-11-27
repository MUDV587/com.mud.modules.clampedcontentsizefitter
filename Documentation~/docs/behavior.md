# Behavior

## Layout Order

`ClampedContentSizeFitter` overrides layout callbacks:

1. Call base `ContentSizeFitter` layout calculation.
2. Clamp the fitted axis size with configured min/max.

This keeps the base fit semantics intact.

## Axis Rules

- **Unconstrained**: axis is not touched.
- **Min Size / Preferred Size**: base calculates size, then clamp is applied if enabled.

## Range Normalization

If both min and max are enabled and `min > max`, the range is normalized to `max = min`. This rule is applied consistently in layout and `OnValidate`.

## Edit Mode

The component uses `[ExecuteAlways]`, so it behaves like ContentSizeFitter in Edit Mode.

## Value Semantics

- `-1`: limit disabled
- `>= 0`: limit enabled
