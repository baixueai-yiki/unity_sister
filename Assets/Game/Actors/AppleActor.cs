using UnityEngine;

public class AppleActor : MonoBehaviour
{
    public string itemId = "test_apple";
    public int amount = 1;


    public (string itemId, int amount) PickItem()
    {
        Debug.Log($"[AppleActor] itemId = {itemId}");
        Debug.Log($"[AppleActor] amount = {amount}");
        //Destroy(gameObject);
        return (itemId, amount);
    }
}