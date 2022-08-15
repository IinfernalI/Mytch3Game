using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneView : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    public event Action OnClickStart;
    
    public StartSceneView Instance { get; private set; }
    public void Start()
    {
        Instance = this;
        _startButton.onClick.AddListener(StartClick);
    }
    
    public void StartClick()
    {
        OnClickStart?.Invoke();
    }
}