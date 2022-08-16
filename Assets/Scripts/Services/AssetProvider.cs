using UnityEngine;
using View;

namespace Services
{
    public class AssetProvider
    {
        private const string Canvasboard = "CanvasBoard";
        private const string PrefabConfigs = "PrefabConfigs";
        private const string EffectConfigs = "Effect";
        private const string ItemsConfigs = "Items";

        private PrefabConfigs _elementPrefab;
        private EffectConfigs _effects;
        private ItemsConfigs _items;
        private View.View _getViewPrefab;

        public AssetProvider()
        {
            _elementPrefab = Resources.Load<PrefabConfigs>(PrefabConfigs);
            _effects = Resources.Load<EffectConfigs>(EffectConfigs);
            _items = Resources.Load<ItemsConfigs>(ItemsConfigs);
            _getViewPrefab = Resources.Load<View.View>(Canvasboard);
        }

        public EffectView GetEffect(string name) => 
            _effects.GetConfig(name).Effect;

        public Sprite GetItemSprite(string name) => 
            _items.GetConfig(name).Sprite;

        public T GetPrefab<T>() where T : Component => 
            _elementPrefab.GetConfig<T>();
    }
}