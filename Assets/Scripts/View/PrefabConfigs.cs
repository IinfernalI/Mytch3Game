using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabConfigs", menuName = "Configs/PrefabConfigs", order = 1), System.Serializable]
public class PrefabConfigs : UnityEngine.ScriptableObject
{
    [SerializeField] List<Component> _prefabConfig;

    Dictionary<Type, GameObject> _configDictionary = new ();

    private void OnEnable()
    {
        foreach (var value in _prefabConfig)
        {
            Type key = value.GetType();
            if (_configDictionary.ContainsKey(key) == false)
            {
                _configDictionary.Add(key, value.gameObject);
            }
        }

    }

    public T GetConfig<T>() where T: Component
    {
        Type key = typeof(T);
        if (_configDictionary.ContainsKey(key))
        {
            return _configDictionary[key].GetComponent<T>();
        }

        Debug.Log($"can not find {nameof(T)}");
        return null;
    }

    public class Prefab
    {
        public Component Conponent;
        public GameObject GameObject;
    }
}