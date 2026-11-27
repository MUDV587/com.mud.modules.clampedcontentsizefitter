# 安装

## Package Manager（Git URL）

1. 打开 **Window > Package Manager**。
2. 点击 **+**。
3. 选择 **Add package from git URL...**。
4. 输入：

```text
https://github.com/MUDV587/com.mud.modules.clampedcontentsizefitter.git#v0.1.0
```

## manifest.json

在 `Packages/manifest.json` 中添加：

```json
"com.mud.modules.clampedcontentsizefitter": "https://github.com/MUDV587/com.mud.modules.clampedcontentsizefitter.git#v0.1.0"
```

## 环境要求

- Unity 2022.3 LTS 或更高版本
- `com.unity.ugui` 1.0.0+
