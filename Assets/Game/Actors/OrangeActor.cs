using UnityEngine;

public class OrangeActor : MonoBehaviour , IPickItemData
{
    public string itemId = "core_test_orange";
    public int amount = 1;


    public (string itemId, int amount) PickItem()
    {
        //Destroy(gameObject);
        return (itemId, amount);
    }
}
