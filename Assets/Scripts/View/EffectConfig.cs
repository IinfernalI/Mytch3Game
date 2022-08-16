using UnityEngine;
using View;

[System.Serializable]
public class EffectConfig
{
    [SerializeField] string _name;
    [SerializeField] EffectView _effect;
   public string Name
    {
        get => _name;
    }

    public EffectView Effect
    {
        get => _effect;
        private set => _effect = value;
    }
}