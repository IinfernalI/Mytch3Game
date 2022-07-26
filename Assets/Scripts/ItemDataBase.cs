using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemDataBase
{
    public static Item[] ItemsProp { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]private static void Initialize() => ItemsProp = Resources.LoadAll<Item>("Items/");
    
    
}
