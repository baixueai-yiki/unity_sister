using UnityEngine;              //基础 Unity 功能
using UnityEngine.UI;
using TMPro;

// 单个配方的UI脚本，用于显示配方信息
public class RecipeEntryUI : MonoBehaviour
{
    public ItemDatabase itemDatabase;             //引用：物品数据库
    public RecipeDatabase recipeDatabase;         //引用：配方数据库
    private RecipeData recipeData;                //声明一个RecipeData类的变量，用于接收配方数据
    private bool canCraft;                          //当前配方是否可以制作
    //private RecipeData recipe;                  //引用：当前显示的配方数据（仅保存引用，不创建新数据）
    //public Button Button_Craft;                 // 合成按钮
    //public Image Img_CraftArrow;                // 工艺箭头图标
    public string recipeID = "";                // 声明一个变量。用于储存配方ID
    public Image Img_RecipeIcon;                  // 配方图标
    public TMP_Text Text_RecipeName;              // 配方名字
    public TMP_Text Text_Description;               // 配方简介
    [Header("材料图标")]
    public Image Img_RecipeIcon1;
    public Image Img_RecipeIcon2;
    public Image Img_RecipeIcon3;
    public Image Img_RecipeIcon4;
    [Header("结果图标")]
    public Image Img_ResultIcon1;
    public Image Img_ResultIcon2;
    public Image Img_ResultIcon3;
    public Image Img_ResultIcon4;
    //private Image[] recipeIcons;               // 材料图标数组
    //private Image[] resultIcons;               // 结果图标数组

    
    public void SetRecipe(string recipe)//设置配方条目的数据(传入配方id的参数)
    {
        //recipeID = recipeId;//这个id变量其实可以删掉，直接用recipeData.id
        //不过我后面打算改成传入recipe本体，到时候大改一下
        //recipeData = recipeDatabase.GetRecipeData(recipeId);

        Img_RecipeIcon.sprite = recipeData.icon;
        Text_RecipeName.text = recipeData.name;
        Text_Description.text = recipeData.description;

        Img_RecipeIcon1.sprite = itemDatabase.GetItemData(recipe.RecipeItemID[0]).icon;
        Img_RecipeIcon1.sprite = itemDatabase.GetItemData(recipe.RecipeItemID[1]).icon;
        Img_RecipeIcon1.sprite = itemDatabase.GetItemData(recipe.RecipeItemID[2]).icon;
        Img_RecipeIcon1.sprite = itemDatabase.GetItemData(recipe.RecipeItemID[3]).icon;
        Img_CraftArrow.sprite = recipeData.craftArrow;
        Img_RecipeIcon1.sprite = itemDatabase.GetItemData(recipe.ResultItemID[0]).icon;
        Img_RecipeIcon1.sprite = itemDatabase.GetItemData(recipe.ResultItemID[1]).icon;
        Img_RecipeIcon1.sprite = itemDatabase.GetItemData(recipe.ResultItemID[2]).icon;
        Img_RecipeIcon1.sprite = itemDatabase.GetItemData(recipe.ResultItemID[3]).icon;
    }


    //设置当前配方是否可以制作
    public void SetCraftable(bool value)
    {
        canCraft = value;
        //设置按钮是否可点击
        Button_Craft.interactable = canCraft;
    }
    //点击合成按钮
    public void CraftClick()
    {
        if (!canCraft)//材料不足，直接返回
            return;
        //广播合成事件（参数：当前配方）
        //EventBus.RaiseCraft(recipeID);

        Debug.Log("配方：" + recipeID);
    }
}