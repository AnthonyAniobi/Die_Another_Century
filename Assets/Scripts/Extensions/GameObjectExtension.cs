using UnityEngine;

public static class GameObjectExtension
{
    public static void RemoveAllChildren(this GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
