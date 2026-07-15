using UnityEngine;

//指令：这个类可以被显示或保存
[System.Serializable]
public class RecipeData
{
    //public Sprite craftArrow;       // 工艺箭头
    public string id = "";          // 配方ID（clean_wheat）
    public string name = "";        // 配方名字（如：挑拣干净麦粒）
    public string description = ""; // 配方简介
    public Sprite icon = null;      // 配方图标
    public string stationId = "";   // 工作台ID（Player、Table）

    // public string recipeItemID1;     // 配方物品1（ID）
    // public string recipeItemID2;     // 配方物品2（ID）
    // public string recipeItemID3;     // 配方物品3（ID）
    // public string recipeItemID4;     // 配方物品4（ID）
    // public string resultItemID1;     // 结果物品1（ID）
    // public string resultItemID2;     // 结果物品2（ID）
    // public string resultItemID3;     // 结果物品3（ID）
    // public string resultItemID4;     // 结果物品4（ID）

    // 声明配方材料数组和结果材料数组，命名为RecipeID和ResultID 。
    // 创建一个新的字符串数组（固定4格，不考虑数量）
    // 每种合成配方的最大材料数量是四个物品（不考虑堆叠）
    public string[] RecipeID = new string[4];
    public string[] ResultID = new string[4];
}