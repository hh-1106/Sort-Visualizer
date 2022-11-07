<div align="center">

# Sort-Visualizer
这是一个基于 Unity 的<b>排序算法</b>可视化框架

<p>
    <a href="https://www.zhihu.com/people/homeless-honey/posts">
        <img src="https://img.shields.io/badge/-北北-f6f6f6?logo=zhihu">
    </a>
    <a href="https://space.bilibili.com/16054225">
        <img src="https://img.shields.io/badge/-SIH-f77199?logo=bilibili">
    </a>
    <img src="https://img.shields.io/github/license/hh-1106/Sort-Visualizer?color=blue">
</p>

</div>

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
docs

    README.md
```


## ⚡️ 使用

<img src="https://github.com/homeless-honey/Sort-Visualizer/blob/main/docs/imgs/1.png?raw=true"
width=40%
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
<br>


## ⏺️ 自定义录制

<img src="https://github.com/homeless-honey/Sort-Visualizer/blob/main/docs/imgs/2.png?raw=true"
width=40%
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
    // 单例模式，无需创建对象，按如下方调用即可

    // 开始录制
    MovieRecorder.Instance.StartRecording();

    // 停止录制
    MovieRecorder.Instance.StopRecording();
    ```

<br>
<br>


## 🎨 自定义渲染方案
本节我们将要实现一个数组可视化方案，别担心，你要做的只是决定数组的每个 `元素` 长什么样子，以及怎样 `摆放` 它们而已。只需要几十行代码哦。
我知道你已经跃跃欲试了，不过还是先看看我们有什么吧~

### 🐔 ArrayVisual
脱下 `unity魔法外衣`，这只基类已然露出本来样貌。

```csharp
public class ArrayVisual : MonoBehaviour
{
    public int n;                           // 数组长度
    public int[] A;                         // 数组
    
    int[] states;                           // 数组每个元素的（排序）状态
    public Color[] palette;                 // 我们将给每种状态一种颜色以标记它

    public GameObject arrayElementPrefab;   // 数组元素物体

    private void Update()
    {
        UpdateObjs();
    }

    // 初始化数组，逆序
    protected virtual void InitArray()
    {
        A = new int[n];
        states = new int[n];

        for (int i = 0; i < n; i++)
        {
            A[i] = n - i;
            states[i] = 0;
        }
    }

    // 更新数组元素物体物理信息
    protected virtual void UpdateObjs() {}
}

```
不难发现，我们的 `ArrayVisual` 拥有将数组可视化的能力，它会在 `Update` 中不断地根据数组信息同步视觉呈现。至于如何修改数组，那是 `Sort Algorithm` 的事。现在让我们专注到数组最初的样子吧。

#### TriangleArrayVisual
```csharp
public class TriangleArrayVisual : ArrayVisual
{
    public float pannelWidth;
    public float pannelHeight;

    protected override void UpdateObjs()
    {
        // 生成所有待排序的物体
        if (transform.childCount != n)
        {
            for (int i = 0; i < n; i++)
            {
                GameObject e = Instantiate(arrayElementPrefab) as GameObject;
                e.transform.parent = transform;
            }
        }

        // 修改待排序物体的物理状态
        for (int i = 0; i < n; i++)
        {
            var e = transform.GetChild(i).gameObject;

            float x = Mathf.Lerp(-pannelWidth / 2f, pannelWidth / 2f, (i + .5f) / (float)n);
            float h = Mathf.Lerp(0, pannelHeight, A[i] / (float)n);

            e.transform.position = new Vector3(x, 0, 0);
            e.transform.localScale = new Vector3(strokeWidth, h, 0);
            e.GetComponent<SpriteRenderer>().color = palette[States[i]];
        }
    }
}
```

待续...