using System;
using UnityEngine;
using UnityEngine.EventSystems;
using View;
using Object = UnityEngine.Object;

namespace Services
{
    public class GameFactory // TODO Create interface
    {
        private readonly AssetProvider _assetProvider;

        public GameFactory(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public void InitScene2(Action callback)
        {
            CreateEventSystem();
            var camera = CreateCamera("IT_IS_MY_SUPERPUPER_CAMERA");
            InstantiateAndInitCanvas(camera, callback);

            void CreateEventSystem()
            {
                var go = new GameObject("EventSystem");
                go.AddComponent<EventSystem>();
                go.AddComponent<StandaloneInputModule>();
            }

            void InstantiateAndInitCanvas(Camera camera, Action callback)
            {
                //ButtonStart obj = Resources.Load<ButtonStart>("CanvasForLevel_01");
                //var buttonStart = Object.Instantiate(obj); //+ instantiate pref & getCom from him
                ButtonStart buttonStart = GetInstance<ButtonStart>();
                Canvas canvas = buttonStart.GetComponent<Canvas>();
                canvas.worldCamera = camera;
                buttonStart.OnClick += OnClickHandler;

                void OnClickHandler()
                {
                    buttonStart.OnClick -= OnClickHandler;
                    callback?.Invoke();
                }
            }
        }

        public Camera CreateCamera(string cameraName)
        {
            var camObj = new GameObject(cameraName);
            var camera = camObj.AddComponent<Camera>();
            camObj.AddComponent<AudioListener>();
            return camera;
        }

        public T GetInstance<T>(Transform parent = null) where T : Component
        {
            T prefab = _assetProvider.GetPrefab<T>();
            T instans = parent == null ? Object.Instantiate(prefab) : Object.Instantiate(prefab, parent);
            return instans;
        }

        public EffectView GetEffect(string name, Transform parent)
        {
            var prefab = _assetProvider.GetEffect(name);
            return UnityEngine.Object.Instantiate(prefab, parent);
        }

        public Sprite GetSprite(string name) => 
            _assetProvider.GetItemSprite(name);
    }
}