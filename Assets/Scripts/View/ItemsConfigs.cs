using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "Configs/ItemsConfigs", order = 2), System.Serializable]
public class ItemsConfigs : UnityEngine.ScriptableObject
{
    [SerializeField] List<IconConfig> _itemConfig;

    Dictionary<string, IconConfig> _configDictionary = new ();
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

    public IconConfig GetConfig(string name)
    {
        if (_configDictionary.ContainsKey(name))
        {
            return _configDictionary[name];
        }

        Debug.Log($"cannot find {name} - return default {_defaultName}");
        return _configDictionary[_defaultName];
    }

}