using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public sealed class Tile : MonoBehaviour
{
    public int x;
    public int y;

    private Item _itemProp;

    public Item ItemProp
    {
        get => _itemProp;
        set
        {
            if (_itemProp == value)
            {
                return;
            }

            _itemProp = value;
            icon.sprite = _itemProp.sprite;
        }
    }

    public Image icon;

    public Button button;
    
    
}
