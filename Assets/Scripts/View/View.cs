using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
using InGameStateMashine.InGameState;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class View : MonoBehaviour
    {
        public event Action<int, Vector2Int> OnEndDrag;
        public event Action OnSave;
        public event Action OnLoad;
        public GridLayoutGroup gridLayoutGroup;

        [SerializeField] private GameObject _blockInput;
        [SerializeField] private Text  _score;
        [SerializeField] private Button  _saveButton;
        [SerializeField] private Button  _loadButton;

        [SerializeField] private  Ease _easeFall = Ease.OutBounce;
        [SerializeField] private  Ease _scoreEase = Ease.OutSine;
        [SerializeField] private  float _scoreDuration = 0.2f;

        [SerializeField,Range(1,8)] private float _speed = 2f;
        [SerializeField,Range(0.001f,1f)] private float _animationDelay = 0.02f;
        [SerializeField,Range(0.05f,1f)]
        private float _swarDuration = 0.1f;

        private Canvas _canvas;

        private Model _model;

        private GameObjectSimplePoolObject<EffectView> _poolEffect = new();

        private readonly List<ElementView> _listViewElements = new();

        private readonly Dictionary<Vector2, RectTransform> _positions = new();

        private Sequence _fallSequence;

        private float _cubeSize;

        private Sequence _swapAnimation;

        private Sequence _destroyAnimation;

        private GameFactory _gameFactory;
        private float _distance = 0;

        public void Init(Model model, GameFactory gameFactory)
        {
            _model = model;
            _gameFactory = gameFactory;
            _canvas = GetComponentInParent<Canvas>();
            _canvas.worldCamera = Camera.main;
            UpdateGrid();
            CreateFieldView();
            UpdateView();
            _model.OnSetData += OnSetData;
            _model.OnSwap += SwapAnimate;
            _model.OnScoreChangedBy += ScoreChange;
            ScoreChange(0,_model._score,_model._score);
           
            Debug.Log($"Done: {+_positions.Count}");
        }

        private void Start()
        {
            _saveButton.onClick.AddListener(SaveClick);
            _loadButton.onClick.AddListener(LoadClick);
        }
        private void OnDestroy()
        {
            _model.OnSetData -= OnSetData;
            _model.OnSwap -= SwapAnimate;
            _saveButton.onClick.RemoveListener(SaveClick);
            _loadButton.onClick.RemoveListener(LoadClick);

        }

        private void CreateFieldView()
        {
            for (int y = 0; y < _model.Height; y++) // 0
            {
                for (int x = 0; x < _model.Weight; x++) //1
                {
                    RectTransform rt =
                        new GameObject($"[{x},{y}]", typeof(RectTransform)).transform as RectTransform;
                    rt.SetParent(gridLayoutGroup.transform);
                    rt.localScale = Vector3.one;
                    _positions.Add(new Vector2(y, x), rt);
                }
            }
        }

        public void PlayerCanUseInput(bool isActive) =>
            _blockInput.SetActive(isActive == false);

        private void OnSetData(List<ElementData> list)
        {
            UpdateView(list);
        }

        void Update()
        {
            
            // Make sure user is on Android platform
           // if (Application.platform == RuntimePlatform.Android) {
        
                // Check if Back was pressed this frame
                if (Input.GetKeyDown(KeyCode.Escape)) {
            
                    // Quit the application
                    Application.Quit();
                    Debug.Log($"Quit");
                }
            //}
        }

        private void UpdateView(List<ElementData> listForUpdate = null)
        {
            listForUpdate = listForUpdate ?? _model.Elements;
            foreach (ElementData data in listForUpdate)
            {
                int id = data.ID;
                ElementView elView = GetViewById(id);
                if (elView == null)
                {
                    elView = InstantiateAndInit(_positions[new Vector2(data.yPos >= 0?data.yPos : data.FallDownPos , data.xPos)].transform);
                }

                Transform elViewTransform = elView.transform;
                elViewTransform.SetParent(_positions[new Vector2(data.yPos >= 0?data.yPos : data.FallDownPos, data.xPos)].transform);
                elViewTransform.localPosition = Vector3.zero;
                elViewTransform.localScale = Vector3.one;

                elView.SetID(id);
                elView.SetStrite(_gameFactory.GetSprite(data.type.ToString()));
                elView.SetUnactive(data.State == ElementState.IsDead);
            }
        }

        private void ScoreChange(int old,int change, int cur) => 
            DOVirtual.Int(old, cur, _scoreDuration, (i) => { _score.text = $"{i}$"; }).SetEase(_scoreEase);

        public ElementView InstantiateAndInit(Transform parent)
        {
            var elView = _gameFactory.GetInstance<ElementView>(parent);
            elView.SetDragContainer(gameObject);
            //elView.OnClick += OnClickHandler;
            elView.OnEndDragAction += OnEndDragHandler;
            _listViewElements.Add(elView);
            return elView;
        }

        private void OnEndDragHandler(int id, Vector2Int direction)
        {
            OnEndDrag?.Invoke(id, direction);
        }

        private ElementView GetViewById(int id)
        {
            return _listViewElements?.FirstOrDefault(i => i.GetID() == id);
        }

        // public void OnClickHandler(int id) =>
        //     OnClick?.Invoke(id);

        private void UpdateGrid()
        {
            int cubsCount = _model.Weight;
            gridLayoutGroup.constraintCount = cubsCount;
            var spacingAll = (cubsCount - 1) * gridLayoutGroup.spacing.x;
            spacingAll += gridLayoutGroup.padding.left + gridLayoutGroup.padding.right;
            _cubeSize = (gridLayoutGroup.GetComponent<RectTransform>().rect.width - spacingAll) / cubsCount;
            gridLayoutGroup.cellSize = new Vector2(_cubeSize, _cubeSize);
        }

        public float FallAnimate(List<AnimateData> animate)
        {
            _fallSequence?.Kill();
            _fallSequence = DOTween.Sequence();
            foreach (AnimateData data in animate.OrderBy(i => i.X).OrderByDescending(i => i.ToY))
            {
                ElementView elView = GetViewById(data.ID);
                if (elView == null)
                {
                    Debug.LogWarning($"cannot found: {data.ID}");
                    continue;
                }

                ;
                elView.SetStrite(_gameFactory.GetSprite(data.type.ToString()));
                elView.transform.position = data.FromY < 0
                    ? _positions[new Vector2(0, data.X)].transform.position + new Vector3(0, Mathf.Abs(data.FromY) * GetDistance)
                    : _positions[new Vector2(data.FromY, data.X)].transform.position;

                Vector2 to = _positions[new Vector2(data.ToY, data.X)].transform.position;
                float duration = (data.ToY - data.FromY) / _speed;
                var fall = elView.transform.DOMove(to, duration).SetEase(_easeFall);
                _fallSequence.Join(fall.SetDelay(_animationDelay));
                elView.SetUnactive(false);
            }

            return _fallSequence.Duration();
        }

        private float SwapAnimate(ElementData a, ElementData b)
        {
            _swapAnimation?.Kill();
            _swapAnimation = DOTween.Sequence();

            var elviewA = _listViewElements.FirstOrDefault(i => i.GetID() == a.ID);
            var elviewB = _listViewElements.FirstOrDefault(i => i.GetID() == b.ID);
            if (elviewA == null || elviewB == null)
            {
                Debug.LogWarning($"cannot find {a.ID} or {b.ID} ");
                return 0;
            }

            var destinationPositionA = elviewB.OrigPos;
            var destinationPositionB = elviewA.OrigPos;
            Debug.Log($"a {a.ID}{destinationPositionA} b {b.ID} {destinationPositionB}");

            var moveA = elviewA.transform.DOMove(destinationPositionA, _swarDuration);
            var moveB = elviewB.transform.DOMove(destinationPositionB, _swarDuration);
            _swapAnimation.Join(moveA);
            _swapAnimation.Join(moveB);
            _swapAnimation.OnComplete(() => { UpdateView(new List<ElementData> { a, b }); });
            return _swapAnimation.Duration();
        }

        private void LoadClick() => 
            OnLoad?.Invoke();

        private void SaveClick() => 
            OnSave?.Invoke();


        public float AnimateDestroyElementFor(List<ElementData> deadElenemtList)
        {
            float duration = 0;
            foreach (ElementData data in deadElenemtList)
            {
                var elview = _listViewElements.FirstOrDefault(i => i.GetID() == data.ID);

                if (elview == null)
                {
                    Debug.LogWarning($"cannot find {data.ID}");
                    continue;
                }

                EffectView effect;
                if (_poolEffect.TryGet(out effect) == false)
                    effect = _gameFactory.GetEffect(data.type.ToString(), elview.transform);

                duration = duration < effect.Duration ? effect.Duration : duration;
                effect.SetActiveForParticles(true);

                DOVirtual.DelayedCall(duration, () =>
                {
                    effect.SetActiveForParticles(false);
                    effect.gameObject.SetActive(false);
                });
            }

            return duration;
        }

        private float GetDistance
        {
            get
            {
                var distance = _distance = _distance > 0
                    ? _distance
                    : (_distance = Vector3.Distance(_positions[new Vector2(0, 0)].position, _positions[new Vector2(0, 1)].position));
                return distance;
            }
        }
        
    }
}