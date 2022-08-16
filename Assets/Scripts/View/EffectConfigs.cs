using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Configs/EffectConfigs", order = 1), System.Serializable]
public class EffectConfigs : UnityEngine.ScriptableObject
{
    [SerializeField] List<EffectConfig> _itemConfig;

    Dictionary<string, EffectConfig> _configDictionary = new Dictionary<string, EffectConfig>(1);
    private string _defaultName = "default";

    private void OnEnable()
    {
        foreach (var item in _itemConfig)
        {
            if (_configDictionary.ContainsKey(item.Name) == false)
            {
                _configDictionary.Add(item.Name, item);
            }
        }

    }

    public EffectConfig GetConfig(string name)
    {
        if (_configDictionary.ContainsKey(name))
        {
            return _configDictionary[name];
        }

        return _configDictionary[_defaultName];
    }

}