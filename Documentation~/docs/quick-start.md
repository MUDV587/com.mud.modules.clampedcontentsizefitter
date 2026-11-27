# Quick Start

1. Add **Clamped Content Size Fitter** to a `RectTransform`.
2. Set **Horizontal Fit** / **Vertical Fit** like a normal `ContentSizeFitter`.
3. Enable **Min/Max** fields for the fitted axis.
4. Enter Play Mode or change content - the fitted size will be clamped.

## Example

- Horizontal Fit: `Preferred Size`
- Min Width: `100`
- Max Width: `400`

If preferred content width is smaller than 100, the final width becomes 100. If larger than 400, it becomes 400.

## Disable a Limit

Set the limit to disabled (`-1`). The inspector toggle controls this automatically.
