using System.Collections.Generic;
using UnityEngine;


//合成界面UI，负责生成和排列所有配方条目
public class RecipeUI : MonoBehaviour
{
    public RecipeDatabase recipeDatabase;     //引用：配方数据库
    public RecipeEntryUI recipeEntryUI;       //引用：配方条目UI（预制体）
    public Transform content;                 //引用：Scroll View的Content
    public Inventory inventory;               //引用：制作容器


    void Start()
    {
        RefreshRecipe();
    }


    //刷新配方列表
    public void RefreshRecipe()
    {
        //遍历 Content 下面所有的子物体，命名为child后删除
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        //创建两个临时列表，用于保存可制作和不可制作的配方
        List<RecipeData> canCraftRecipes = new List<RecipeData>();
        List<RecipeData> cannotCraftRecipes = new List<RecipeData>();

        //遍历数据库中的所有配方，命名为recipe
        foreach (RecipeData recipe in recipeDatabase.recipes)
        {
            //判断当前配方是否可以制作
            if (CanCraft(recipe))
            {
                canCraftRecipes.Add(recipe);//加入可制作列表
            }
            else
            {
                cannotCraftRecipes.Add(recipe);//加入不可制作列表
            }
        }
        //遍历可制作列表中的所有配方，调用生成配方条目的函数（配方、是否可制作）
        foreach (RecipeData recipe in canCraftRecipes)
        {
            CreateRecipeEntry(recipe, true);
        }
        foreach (RecipeData recipe in cannotCraftRecipes)
        {
            CreateRecipeEntry(recipe, false);
        }
    }

    //判断配方是否可以制作
    private bool CanCraft(RecipeData recipe)
    {
        InventorySlot[] slots = inventory.GetSlots();
        //遍历配方中的材料数组
        foreach (string recipeID in recipe.RecipeID)
        {
            //空材料直接跳过
            if (string.IsNullOrEmpty(recipeID))
                continue;

            //记录当前材料是否找到
            bool found = false;

            //遍历桌子容器数组
            foreach (InventorySlot slot in slots)
            {
                //找到需要的材料
                if (slot.itemId == recipeID)
                {
                    found = true;
                    break;
                }
            }

            //有一种材料没找到说明无法制作
            if (!found)
                return false;
        }

        //所有材料都找到了
        return true;
    }

    //生成一个配方条目（配方、是否可制作）这个函数是被循环调用的所以不用担心entry变量重复使用
    private void CreateRecipeEntry(RecipeData recipe, bool canCraft)
    {
        //声明RecipeEntryUI类的变量命名为entry，赋值为 实例化一个recipeEntryUI放到content下面
        // Instantiate实例化是unity的功能
        RecipeEntryUI entry = Instantiate(recipeEntryUI, content);
        //设置配方数据
        entry.SetRecipe(recipe.id);
        //设置是否可以制作
        entry.SetCraftable(canCraft);
    }
}