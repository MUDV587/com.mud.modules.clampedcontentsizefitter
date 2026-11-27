# 行为说明

## 布局顺序

`ClampedContentSizeFitter` 重写布局回调：

1. 调用基类 `ContentSizeFitter` 的布局计算。
2. 对受控轴应用 min/max clamp。

这样可以保持基类 Fit 语义不变。

## 轴规则

- **Unconstrained**：不修改该轴。
- **Min Size / Preferred Size**：基类计算尺寸后，如果启用了 clamp，则应用限制。

## 范围归一化

当 min/max 同时启用且 `min > max` 时，范围会归一化为 `max = min`。该规则在布局和 `OnValidate` 中一致生效。

## Edit Mode

组件使用 `[ExecuteAlways]`，因此在 Edit Mode 下行为与 ContentSizeFitter 一致。

## 值语义

- `-1`：限制禁用
- `>= 0`：限制启用
