# 快速开始

1. 在 `RectTransform` 上添加 **Clamped Content Size Fitter**。
2. 像普通 `ContentSizeFitter` 一样设置 **Horizontal Fit** / **Vertical Fit**。
3. 为受控轴启用 **Min/Max** 字段。
4. 进入 Play Mode 或修改内容，受控轴尺寸会被 clamp。

## 示例

- Horizontal Fit：`Preferred Size`
- Min Width：`100`
- Max Width：`400`

当内容首选宽度小于 100 时，最终宽度为 100；大于 400 时，最终宽度为 400。

## 禁用限制

将限制设为禁用（`-1`）。Inspector 开关会自动处理该值。
