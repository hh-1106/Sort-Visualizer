<div align="center">

# Sort-Visualizer
这是一个基于 Unity 的<b>排序算法</b>可视化框架

<p>
    <a href="https://www.zhihu.com/people/hh-1106/posts">
        <img src="https://img.shields.io/badge/-北北-f6f6f6?logo=zhihu">
    </a>
    <a href="https://space.bilibili.com/16054225">
        <img src="https://img.shields.io/badge/-SIH-f77199?logo=bilibili">
    </a>
    <img src="https://img.shields.io/github/license/hh-1106/Sort-Visualizer?color=blue">
</p>

</div>

<p align="center">
<img width=32% hspace=0.1% src="https://github.com/hh-1106/Sort-Visualizer/blob/main/docs/gifs/merge.gif?raw=true">
<img width=32% hspace=0.1% src="https://github.com/hh-1106/Sort-Visualizer/blob/main/docs/gifs/radix.gif?raw=true">
<img width=32% hspace=0.1% src="https://github.com/hh-1106/Sort-Visualizer/blob/main/docs/gifs/heap.gif?raw=true">
</p>

## ⚙️ 安装
1. `clone` 或 `download` 此 `repo` 到你的电脑
2. 解压 `repo > Sort-Visualizer > Assets > Plugins.zip`
3. 用 `Unity Hub` 打开 `Sort-Visualizer` 文件夹
4. 等待 Unity 启动和导入项目
5. `Unity` 菜单栏点击 `Window > Package Manger`，搜索 `Post Processing` 并安装
   - 搜不到的话请检查 `Package Manager` 窗口左上角是否选中 `Packages: Unity Registry`
6. 安装结束后项目结构应如下

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

<img src="https://github.com/hh-1106/Sort-Visualizer/blob/main/docs/imgs/1.png?raw=true"
width=44%
hspace=3%
align="right"> 

1. 在 `Project` 窗口中打开 `Assets > Scenes > Sort Visualizer` 场景
2. 在 `Hierarchy` 窗口中选中 `Sort Anim` 对象
3. 在 `Inspector` 窗口中修改参数
   - `Array Visual` 可以拖入不同的渲染方案，并修改相应的参数
   - `排序算法` 默认提供了9种算法实现，点击选择
   - `动画` 设置动画参数
   - `录制` 选择是否开启录制
4. 点击▶️运行程序
5. 如果你 `Enable Record`，运行时会自动录制视频

<br>
<br>
<br>
<br>
<br>


## ⏺️ 自定义录制

<img src="https://github.com/hh-1106/Sort-Visualizer/blob/main/docs/imgs/2.png?raw=true"
width=44%
hspace=3%
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

### 🐔 Array Visual
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

#### 🐦 Triangle Array Visual


<img src="https://github.com/hh-1106/Sort-Visualizer/blob/main/docs/gifs/1.gif?raw=true"
width=52%
hspace=3%
align="right"> 

`Triangle Array Visual` 是已提供的范例，它将数组的每个元素按照其数值映射为长方形物体，按照下标顺序横向排列。

- 你可以在示例场景中点击 `Array Visual > TriangleArrayVisual`，便能如右图一般实时调整视觉效果。
- 如果你想在自己的场景中使用，从 `Assets > Prefabs > Array Visual` 中拖入 `Hierarchy` 窗口即可。
- 在你做好自己的视觉方案后，记得反过来将其保存为预制体哦。

<font color=#7e7e7e > 双击打开其上挂载的 `Script`，我们来看看它是如何实现的吧。</font>

```csharp
// 继承 ArrayVisual 类
public class TriangleArrayVisual : ArrayVisual
{
    // 整体画布的宽、高
    public float pannelWidth;
    public float pannelHeight;

    // 重写父类方法
    protected override void UpdateObjs()
    {
        // 为数组每个元素都生成视觉物体，并设为孩子统一管理
        CheckChildCount();

        // 修改所有子物体的物理状态
        for (int i = 0; i < n; i++)
        {
            // 每一个子物体e
            var e = transform.GetChild(i).gameObject;

            // 计算其位置和高度
            float x = Mathf.Lerp(-pannelWidth / 2f, pannelWidth / 2f, (i + .5f) / (float)n);
            float h = Mathf.Lerp(0, pannelHeight, A[i] / (float)n);

            // 设置相应的物理属性
            e.transform.position = new Vector3(x, 0, 0);
            e.transform.localScale = new Vector3(strokeWidth, h, 0);

            // 修改颜色
            e.GetComponent<SpriteRenderer>().color = palette[States[i]];
        }
    }
}
```

#### 🤯 Other Array Visual


<img src="https://github.com/hh-1106/Sort-Visualizer/blob/main/docs/gifs/2.gif?raw=true"
width=52%
hspace=3%
align="right"> 

同理，你可以发挥自己的创意制作各式各样的视觉方案。再来回顾一下流程吧~
- 新建空物体
- 添加 `MyArrayVisual` 脚本
- 继承 `ArrayVisual` 类
- 重写 `UpdateObjs` 方法进行数组造型
- 重写 `InitArray` 方法定义数组初态
- `Inspector` 内微调参数
- 替换自己的 `arrayElementPrefab`
- 最终保存为 `My Array Visual Prefab`

<br>
<br>

## 🌈 自定义排序算法

庆幸的是，本框架已经内置了九种经典排序算法。由于算法实现源于我本科时的项目<a href="https://zhuanlan.zhihu.com/p/163725242"> <img src="https://img.shields.io/badge/processing-排序可视化-f6f6f6?logo=zhihu"> </a>，~~所以说不定有不少bug~~，当时 <kbd>C V</kbd> 了许多代码，年代久远，已经无从溯源，总之感谢🙏前辈们的开源精神！

>不过要是你🥵想扩展更多算法的话，就继续前进吧。

为了放慢排序的过程，我使用了协程来暂停时间。因此你大概需要亿点点<a href="https://www.youtube.com/watch?v=Eq6rCCO2EU0"> <img src="https://img.shields.io/badge/-coroutines-fa0008?logo=youtube"> </a>的知识。以 `插入排序` 为例，我们来看看具体实现步骤。

<img width=100% hspace=0% src="https://github.com/hh-1106/Sort-Visualizer/blob/main/docs/gifs/insertion.gif?raw=true">

1. 在 `Assets > Scripts > Sort Algorithm` 中新建 `InsertionSort` 脚本 ~~（东西要分类好🍻）~~
    ```csharp
    // 继承 ISortAlgorithm 接口
    public class InsertionSort : ISortAlgorithm
    {
        // 实现排序方法
        public IEnumerator Sort(ArrayVisual A, float delay)
        {
            // 我将以摸牌时的插入策略来说明本算法实现
            // 手牌已有一张，对于接下来摸到的每一张牌
            for (int j = 1; j < A.n; j++)
            {
                // 切换状态，表示摸到了这张牌
                A.States[j] = 2;
                int k = A[j];

                // [0..i]表示手牌，手牌总是排好序的
                int i = j - 1;

                // 我们要在手牌中找一个位置，从右(大)往左(小)找
                while (i >= 0 && A[i] > k)
                {
                    // 切换状态，以标记我们正在比较哪张手牌
                    // 使用 delay，把这个瞬间暂停下来
                    A.States[i] = 1;
                    yield return new WaitForSeconds(delay);
                    A.States[i] = 0;

                    A[i + 1] = A[i];
                    i--;
                }

                // 插入手牌
                A[i + 1] = k;

                // 切换状态，表示这张牌已经插入手牌
                A.States[j] = 0;
            }
        }
    }
    ```
2. 为了在 `SortAnim` 面板上使用我们新写的算法，还需要在 `SortAnim.cs` 脚本中做一些布置
    ```csharp
    // 添加枚举类型，将会显示在面板上
    public enum SortAlogorithmEnum
    {
        Insertion,
    }

    public class SortAnim : MonoBehaviour
    {
        // ...
        void StartNewSort()
        {
            // ...
            sa = SAEnum switch
            {
                // 当枚举值为 Insertion 时，创建相应的排序算法
                SortAlogorithmEnum.Insertion => new InsertionSort(),
            };
        }
    }
    ```