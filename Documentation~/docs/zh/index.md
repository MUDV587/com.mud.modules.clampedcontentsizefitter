# MUD Clamped Content Size Fitter

`ClampedContentSizeFitter` 在 Unity UI 的 `ContentSizeFitter` 基础上增加了可选的最小/最大宽高限制。

它保持熟悉的 ContentSizeFitter 工作流：为每个轴选择 Fit 模式，然后按需对最终尺寸做 clamp。

![Horizontal Max](../images/demo-horizontal-max.png)

## 特性

- 继承自 `UnityEngine.UI.ContentSizeFitter`
- 每个轴可选 min/max clamp
- 只对受控轴生效（`Unconstrained` 轴不修改）
- 自定义 Inspector，带启用开关
- 多页左右对比示例场景，带 min/max 范围框和命中状态反馈

## 包信息

- **包名**：`com.mud.modules.clampedcontentsizefitter`
- **Unity**：2022.3 LTS+
- **依赖**：`com.unity.ugui`
