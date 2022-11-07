<h1 align='center'> Sort-Visualizer </h1>

<p align="center">
  这是一个基于 Unity 的<b>排序算法</b>可视化框架
</p>


## ⚙️ 安装
1. `clone` 或 `download` 此 `repo` 到你的电脑
2. 用 `Unity Hub` 打开 `repo` 中的 `Sort-Visualizer`文件夹
3. 等待 Unity 启动和导入项目
4. `Unity` 菜单栏点击 `Window > Package Manger`，搜索 `Post Processing` 并安装
5. 安装结束后项目结构应如下

```
Sort-Visualizer
    Assets                                              资源文件夹
        Plugins                                         插件
            com.unity.recorder@3.0.3                    
            Sirenix
            unitypackage-custom-hierarchy-1.2.0
        Prefabs                                         预制体
        Scenes                                          场景
        Scripts                                         代码文件夹
            Array Visual                                
                ArrayVisual.cs                          视觉方案基类
                ...
            Sort Algorithm                              
                ISortAlgorithm.cs                       排序算法接口
                ...
            utils                                       工具包
                MovieRecorder.cs
                Singleton.cs
                TaskManager.cs
            SortAnim.cs                                 排序动画类
        Settings
    Packages                                            官方插件包
    Recordings                                          录制存放文件夹（自定义）
    ...
README.md
```

## ⚡️ 使用

<img src="https://github.com/homeless-honey/Sort-Visualizer/blob/main/docs/imgs/1.png?raw=true"
width=35%
hspace=5%
align="right"> 

1. 在 `Project` 窗口中打开 `Assets > Scenes > Sort Visualizer` 场景
2. 在 `Hierarchy` 窗口中选中 `Sort Anim` 对象
3. 在 `Inspector` 窗口中修改参数
   - `Array Visual` 可以拖入不同的渲染方案，并修改相应的参数
   - `排序算法` 默认提供了9种算法实现，点击选择
   - `动画` 设置动画参数
   - `录制` 选择是否开启录制
4. 点击▶️运行程序
5. 程序运行中亦可修改参数（但不会保存）

<br>
<br>
<br>

## ⏺️ 自定义录制

<img src="https://github.com/homeless-honey/Sort-Visualizer/blob/main/docs/imgs/2.png?raw=true"
width=35%
hspace=5%
align="right"> 

1. 在 `Project` 窗口中打开 `Assets > Prefabs > Movie Recorder` 预制体
2. 在 `Inspector` 窗口中修改录制参数
   - `settings` 资源文件可以在 `Assets > Settings` 内 `右键 > Create > Recorder > ...` 创建
3. 别忘了将预制体拖入 `Hierarchy` 窗口（已有则不用拖）
4. `SortAnim` 会按计划接管录制工作
   - 每次排序都会自动录制保存到设定的文件夹内
   - 如果你只想录制默认流程，这条就到此为止了
5. 🧐若想自行控制录制周期
    ```csharp
    // MovieRecorder 使用了单例模式，无需创建对象，按如下方法调用即可

    // 开始录制
    MovieRecorder.Instance.StartRecording();

    // 停止录制
    MovieRecorder.Instance.StopRecording();
    ```
