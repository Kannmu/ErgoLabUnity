# Unity Experiment Template

## 简介 Introduction

本仓库使用**Unity 2021**开发构建了一个通用的实验程序框架模板，适合进行**心理学、人机工程学、工效学**等需要**人类被试**参加的实验时使用。提供了常用UI功能的预制体Prefab(按钮、滚动文本框、暂停菜单等)、一些常用的人机交互硬件驱动、以及实验数据和被试管理系统。

Unity版本：2021.3.19f1c1。相差不多的版本也可以使用。

## 如何使用 Usage

项目仓库为Unity工程文件，可以按照以下流程使用：

> 1. 在本地新建一个**空Unity项目**，模板选择**2D**。
> 2. **Clone**本仓库文件到项目目录下，覆盖文件。
> 3. 根据需求更改模板内容。

## 结构 Structure

使用场景Scene来管理功能模块，不同的功能切换时加载对应场景Scene。

目前包含以下Scene：

> 1. **IntroAndMenu**: 开始菜单场景
> 2. **Notification**：告知被试实验相关内容场景
> 3. **LoadingScene**：场景间切换 Loading界面


## 场景 Scene 

### 添加场景 Add Scene

为了实现切换场景间的Loading界面，以及解决一些加载产生的卡顿。重写封装了场景切换脚本，场景间的切换调度使用SceneController.cs脚本内方法来进行。不使用原本的BuildIndex系统。

在添加了新的场景后需要在/Asserts/Script/SceneManagement/SceneController.cs文件中手动添加新场景名称到场景列表。并管理场景顺序。如下：

```cs
// 场景列表数组，这里按顺序填写已经存在的scene的场景名。
    public static ArrayList sceneNames = new ArrayList() {
        "Scene_0",
        "Scene_1",
        "IntroAndMenu",
        "Notification",
        "Scene_-1",
    };
```

### 切换场景 Switch Scene

需要切换场景时使用以下方法：

```cs
SceneController.GoToNextScene();//切换下一个场景
SceneController.GoToPrevScene();//切换上一个场景
SceneController.GoToSceneByIndex(int Sceneindex);//通过引索切换场景
SceneController.GoToSceneByName(string Scenename);//通过名称切换场景
```

其中Sceneindex从0开始。

这些方法会先加载LoadingScene，并同时开始异步加载目标场景，当目标场景加载完成90%的时候切换至目标场景。这样便可以流畅的进行场景切换。

## 预制体 Prefabs

目前制作了Button、Scroll Textbox、Fade的预制体。

### 按钮 Button
    

