using System.Collections.Generic;
using UnityEngine;


//可创建的资源模板（路径：Game/Recipe Database）
[CreateAssetMenu(menuName = "Game/Recipe Database")]
//声明一个名为ItemDatabase的类：ScriptableObject资源型数据类
public class RecipeDatabase : ScriptableObject
{
    public List<RecipeData> recipes;//声明一个存放RecipeData类的列表，名为recipes
    private Dictionary<string, RecipeData> map;//声明一个名为map的字典<字符串键，ItemData类值>

    public void Init()
    {
        //为map赋值：一个新的字典<字符串键，ItemData类值>
        map = new Dictionary<string, RecipeData>();
        //foreach是C# 里的一个循环语法（读出的值item = 变量items）
        //foreach循环进行（var读取集合：item = items）
        foreach (var recipe in recipes)
        {
            //将map[recipe的id值] 赋值为 recipe
            map[recipe.id] = recipe;
        }
    }

    //传入配方ID读取对应的配方数据
    public RecipeData GetRecipeData(string id)
    {
        if (map == null)//Lazy Loading懒加载（用到才初始化的写法，用于优化性能）
            Init();
        //TryGetValue是C#的查询写法
        //尝试根据Id查找item，然后返回item（如果查不到返回的是null）
        map.TryGetValue(id, out var recipe);
        return recipe;
    }

    //获取所有配方
    public List<RecipeData> GetAllRecipes()
    {
        return recipes;
    }
}