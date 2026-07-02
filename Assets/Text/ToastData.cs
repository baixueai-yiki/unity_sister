using System.Collections.Generic;   //现代List<类型>工具箱
using UnityEngine;                  //导入Unity常用库


public static class ToastData
{
    // 根据名字返回文本
    public static List<string> GetToast(string name)
    {
        switch (name)
        {
            case "table":
                return new List<string>
                {
                    "123456"
                };

            default:
                return new List<string>
                {
                    "数据丢失"
                };
        }
    }
}
