# unity_sister
身为姐姐的玩家被自己的病娇妹妹囚禁在了家里，妹妹会保护姐姐并提供食物。可是妹妹显然不知道怎样维持身体健康，你能在被囚禁在家里的情况下照顾好自己并偷偷与外界取得联系吗？


## 项目目标
# 1.基本思路
制作一个完整的小游戏，游戏内容简单一些就好
关键是包含完整的游戏流程：外部菜单、关卡过程、游戏结算
现在的思路是制作一个极简的室内戏生存小游戏
玩家被自己的病娇恋人囚禁，基本玩法是在家里生存以保证身体健康
游戏目标是在不被恋人发现的情况下报警

再之后，据说需要做一些商业项目，来展示技能深度
我不太清楚这包括什么

# 2.游戏内容
1.设计玩家单位
先放一个长方形充当美术素材
创建脚本写控制代码，a向左移动 d向右移动 w与物体交互 s躺地上休息
2.设计玩家体力条
玩家移动会消耗体力，躺床上/躺地上休息可以恢复体力
（玩家始终都戴着脚链和铁球移动）
3.设计游戏地图和可交互物品
床：躺床上可以休息（躺地上休息会减体温）
冰箱：拿食材
灶台：拿食材过来做饭
4.设计游戏时间和恋人的对话
女主每天早上都会在固定去上班
女主每天晚上回家时间半固定，拜托她买东西和加班都会晚回家
先设计成固定对话，之后再补充逻辑




# 开发过程
1.创建新场景MainMenu.unity，放在Scenes/MainMenu/
    1.1.创建UI类的对象Canvas画布
    1.2.创建空对象，命名为MainMenuUI。作为Canvas画布的子对象
        1.2.1.创建UI脚本MainMenuUI.cs，放在UI/MenuScripts/，挂载在MainMenuUI下面
    1.3.创建UI类的子对象Panel面板，命名为Panel_Menu。然后在里面存放按钮，作为游戏主菜单
        1.3.1.启动游戏Btn_Start
        1.3.2.读取存档Btn_Load
        1.3.3.致谢名单Btn_Credits
        1.3.4.设置界面Btn_Setting
        1.3.5.退出游戏Btn_Quit
        1.3.6.创建UI类的子对象TextMeshPro文本，命名为Text_Title。然后给项目定个名称
        1.3.7.创建UI类的子对象Image图像，命名为Img_Background。然后放个背景图上去
    1.4.创建UI类的子对象Panel面板，命名为Panel_Start。开始游戏时会有对话框暗示剧情
        1.4.1.创建UI类的子对象TextMeshPro文本，命名为Text_Text_Dialogue。然后给项目定个名称
        1.4.2.创建UI类的子对象Image图像，命名为Img_Dialogue。然后放个背景图上去
    1.5.创建UI类的子对象Panel面板，命名为Panel_Load。只有两个存档位
        1.5.1.存档一Btn_one
        1.5.2.存档二Btn_two
        1.5.3.创建UI脚本LoadUI.cs，放在UI/MenuScripts/，挂载在Panel_Load下面
    1.6.创建UI类的子对象Panel面板，命名为Panel_Credits。致谢名单
    1.7.创建UI类的子对象Panel面板，命名为Panel_Setting。设计一个游戏的设置界面
2.在另一个场景，创建UI类的对象Canvas画布
可以使用默认的SampleScene.unity游戏场景，放在Scenes/Gameplay/
    2.1.创建空子对象，命名为PlayUI。用来挂载所有 游戏运行相关的UI脚本
        2.1.1.创建UI类的子对象Slider滑动条，命名为Bar_Stamina（不需要用户滑动，删掉Handle Slide Area）
        2.1.2.创建脚本PlayerStatusUI.cs，放在Game\UI\GameScripts。控制玩家属性UI
                （我觉得这里需要一个枚举，另外不知道隐藏数据怎么整）
        2.1.3.创建UI类的子对象TextMeshPro文本，命名为Text_Time。用来显示时间
        2.1.4.创建脚本TimeUI.cs，放在Game\UI\GameScripts。控制游戏时间UI
                直接引用时间系统和Text_Time组件，为Text_Time的文本赋值
    2.2.创建空子对象，命名为DialogueUI。用来挂载所有 对话相关的UI脚本
        2.2.1.创建UI类的子对象Panel面板，命名为Panel_Dialogue。一天结束后的对话框
        2.2.2.创建脚本DialogueUI.cs，放在Game\UI\GameScripts。控制对话框UI
            2.2.2.1.初始化时，将Panel_Dialogue隐藏 SetActive(false)
            2.2.2.2.由StoryManager调用了一个函数，根据传入的字符串列表参数为变量赋值
            2.2.2.3.在每帧调用的函数当中显示这些对话
            2.2.2.4.对话播放完毕以后，隐藏Panel_Dialogue并调用EventBus广播对话结束的事件
3.设计时间系统、剧情控制器、全局游戏状态机。都放最后面，这些没有父对象
    3.1.创建空对象，命名为EventBus.cs。作为游戏的事件总线
        3.1.1.创建同名脚本，放在Core/Manager/并挂载
        3.1.2.整个系统用到的事件，统一在这里定义
        3.1.3.整个系统广播事件时，统一调用这里的对应函数
    3.2.创建空对象，命名为StoryManager。作为游戏的剧情控制器
        3.2.1.创建同名脚本，放在Core/Managers/并挂载
        3.2.2.在初始化时 注册今天结束的事件，监听到以后为变量赋值StoryData的文本（根据接收到的天数）
        3.2.3.为变量赋值文本后，调用DialogueUI对话UI并传入参数（当天的剧情 字符串列表）
    3.3.创建空对象，命名为TimeSystem。作为游戏内的时间系统
        3.3.1.创建同名脚本，放在Core/Systems/并挂载
        3.3.2.设计女主回家的时间，在基础值上计算浮动值
        3.3.3.当前时间大于等于女主回家时间，调用EventBus广播今天结束的事件（并传输当前天数）
    3.4.创建空对象，命名为GameStateMachine。作为全局游戏状态机
        3.4.1.创建同名脚本，放在Core/Systems/并挂载
        3.4.2.声明一个枚举，里面存放几种全局游戏状态，为不同的游戏状态开关不同的操作权限
        3.4.3.在加载场景时注册涉及UI切换的事件，监听到以后调用状态切换函数
4.创建空对象，命名为Player。创建空子对象，命名为Sprite，添加组件SpriteRenderer用于存放贴图
    4.1.添加组件Rigidbody2D刚体2D赋予物理规则
    4.2.创建脚本PlayerController.cs，放在Game/Player/Scripts/并挂载。控制Player的基本功能：移动和速度
        4.2.1.在每帧都调用的函数里调用 移动输入、休息输入、移动执行、体力消耗 的函数（输入执行分开是为了让游戏控制角色）
        4.2.2.在输入函数第一行判断GSM当中的操作权限
        4.2.3.移动：在输入函数里获取x轴的输入，在执行函数里为Rigidbody2D赋予一个移动速度
            （让他移动的本质就是在按住a和d的期间，给他一个向量）
        4.2.4.休息：按下s时调用执行函数并调用StartRest的休息函数
        4.2.5.体力消耗：若Rigidbody2D正在移动，则调用PlayerStats的体力消耗函数，并传输消耗的参数
    4.3.创建脚本PlayerStats.cs，放在Game/Player/Scripts/并挂载。控制Player的属性变化
        4.3.1.声明属性变量（目前只有体力相关）并标记，方便在编辑器中显示
        4.3.2.设置多个函数当中用于其他脚本调用来调整相关属性，传入的参数是float
        （传入之前用int保证操作丝滑，传入之后用float保证UI数据易读）
    4.4.创建脚本PlayerRest.cs，放在Game/Player/Scripts/并挂载。控制Player趴地上休息
        4.4.1.目前只有回复体力和调用TimeSystem推进时间的功能，感觉需要加入Player的动画演出
    4.5.创建脚本PlayerControlState.cs，放在Game/Player/Scripts/并挂载。作为Player本身的状态机
        4.5.1.现在还没做，感觉得先做动画演出


# 目录结构
Assets/
├── Core/                  # 全局系统
│   ├── Systems/           # 系统
│   │   ├── TimeSystem.cs                  # 时间系统
│   │   └── GameStateMachine.cs            # 全局游戏状态机
│   ├── Managers/          # 控制器
│   │   ├── EventBus.cs                    # 事件总线
│   │   └── StoryManager.cs                # 剧情控制器（放在core好像不太合适）
│   └── SaveSystem/        # 存档系统
├── Game/                  # 核心游戏模块
│   ├── Interaction/       # 可交互对象系统（床、冰箱、灶台）
│   │   ├── Scripts/       # 交互逻辑、物品状态
│   │   │   └── InteractableBase.cs        # 交互物体基类（床/门/道具）
│   │   └── Prefabs/       # 交互对象预制体
│   │
│   ├── Player/            # 玩家系统
│   │   ├── Scripts/       # 玩家控制、移动、交互逻辑
│   │   │   ├── PlayerController.cs        # 核心移动逻辑：WASD移动、速度、方向
│   │   │   ├── PlayerStats.cs             # 玩家属性（体力、体温、饥饿等数据）
│   │   │   ├── PlayerRest.cs              # 躺下/休息逻辑（体力/体温恢复）
│   │   │   ├── PlayerControlState.cs      # 玩家控制状态机
│   │   │   ├── PlayerInteraction.cs       # 交互逻辑：床、冰箱、灶台等触发事件
│   │   │   └── PlayerAnimation.cs         # 动画控制：走路、躺下、交互动作
│   │   ├── Prefabs/       # 玩家预制体
│   │   └── Animations/    # 玩家动画（走路、躺下等）
│   │
│   └── UI/
│       ├── MenuScripts/       # 菜单UI控制脚本（面板切换、提示）
│       │   ├── MainMenuUI.cs              # 主菜单脚本
│       │   ├── StartUI.cs                 # 启动游戏脚本
│       │   ├── LoadUI.cs                  # 读取游戏脚本
│       │   ├── CreditsUI.cs               # 致谢名单脚本
│       │   └── SettingUI.cs               # 设置界面脚本
│       ├── GameScripts/       # 游戏UI控制脚本
│       │   ├── PlayerStatusUI.cs          # 玩家属性UI
│       │   └── TimeUI.cs                  # 时间ui
│       ├── Panels/            # UI面板的Prefab预制体
│       ├── Sprites/           # 按钮/图标/背景等图片
│       └── Fonts/             # 字体
│
├── Art/                   # 美术资源
│   ├── Models/                            # 3D/2D模型
│   ├── Textures/                          # 贴图
│   └── Materials/                         # 材质球
├── Audio/                 # 音频资源
│   ├── BGM/                               # 背景音乐
│   └── SFX/                               # 音效（交互、脚步、警报）
│
├── Data/                  # 配置和静态数据
│   ├── ScriptableObjects/ # 可配置的数据（如物品属性）
│   ├── Stories/           # 剧情文本
│   │   └── DailyStory.cs                  # 日常剧情
│   └── JSON/              # 剧情文本或简单配置表（但现在不用json系统，以后再考虑）
├── Scenes/                # 场景文件夹
│   ├── MainMenu/          # 主菜单场景
│   │   └── MainMenu.unity                 # 主菜单场景
│   ├── GamePlay/          # 游戏场景（房间）
│   │   └── SampleScene.unity              # 游戏场景
│   └── Test/              # 调试场景
└── Settings/              # Unity项目设置（图形、输入、标签等）

# 命名规范
UI对象统一使用下划线命名法
目录、脚本与类名统一使用驼峰命名法
