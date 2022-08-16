using UnityEngine;

[System.Serializable]
public class IconConfig
{
    [SerializeField] string _name;
    [SerializeField] Sprite _sprite;

    public string Name
    {
        get => _name;
    }

    public Sprite Sprite
    {
        get => _sprite;
        private set => _sprite = value;
    }
}