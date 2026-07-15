using UnityEngine;

public class AppleActor : MonoBehaviour , IPickItemData
{
    public string itemId = "core_test_apple";
    public int amount = 1;


    public (string itemId, int amount) PickItem()
    {
        //Destroy(gameObject);
        return (itemId, amount);
    }
}