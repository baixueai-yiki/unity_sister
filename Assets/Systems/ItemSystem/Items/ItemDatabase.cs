using System.Collections.Generic;
using UnityEngine;


//可创建的资源模板（路径名字 = 游戏/Item Database ）
[CreateAssetMenu(menuName = "Game/Item Database")]
//声明一个名为ItemDatabase的类：ScriptableObject资源型数据类
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> items;    //声明一个存放ItemData类的列表，名为items
    private Dictionary<string, ItemData> map;//声明一个名为map的字典<字符串键，ItemData类值>

    public void Init()
    {
        //为map赋值：一个新的字典<字符串键，ItemData类值>
        map = new Dictionary<string, ItemData>();

        //foreach是C# 里的一个循环语法（读出的值item = 变量items）
        //foreach循环进行（var读取集合：item = items）
        foreach (var item in items)
        {
            //将map[item的id值] 赋值为 item
            map[item.Id] = item;
        }
    }

    //函数GetItemData，返回值是一个ItemData类，传入参数是(string Id)
    public ItemData GetItemData(string Id)
    {
        if (map == null)//Lazy Loading懒加载（用到才初始化的写法，用于优化性能）
            Init();
        //TryGetValue是C#的查询写法
        //尝试根据Id查找item，然后返回item（如果查不到返回的是null）
        map.TryGetValue(Id, out var item);
        return item;
    }
}