using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace View
{
    public class ElementView : MonoBehaviour, IPointerClickHandler, IBeginDragHandler,IEndDragHandler, IDragHandler
    {
        public event Action<int,Vector2Int> OnEndDragAction;
        [SerializeField] private Image _image;

        private int _id;
        private RectTransform _recTransform;
        private Vector3 _startDragPos;


        private Vector2 _eventPosition = Vector2.zero;
        private Camera _camera;
        public float _cellSize = 2;
        private GameObject _dragContainer;
        private Transform _myParent;
        private Vector3 _direction;
        private bool _isDraging;
        public RectTransform RecTransform => _recTransform == null ? (_recTransform = transform as RectTransform) : _recTransform;

        public Vector3 OrigPos => _isDraging ? _startDragPos : RecTransform.position;

        public void SetStrite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void SetID(int id) => _id = id;
    
        public int GetID() => _id;

        public void OnPointerClick(PointerEventData eventData)
        {
           // OnClick?.Invoke(_id);
        }

        public void SetUnactive(bool isUnActive)
        {
            _image.gameObject.SetActive(!isUnActive);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDraging = true;
            _startDragPos = RecTransform.position;
            _myParent = transform.parent;
            transform.SetParent(_dragContainer.transform);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDraging = false;
            transform.SetParent(_myParent);
            RecTransform.position = _startDragPos;
            _eventPosition = Vector2.zero;
            OnEndDragAction?.Invoke(_id,new Vector2Int((int)_direction.x,-(int)_direction.y));
        }

        public void OnDrag(PointerEventData eventData)
        {
            _eventPosition = _camera.ScreenToWorldPoint(eventData.position);
            Vector3 b = new Vector3(_eventPosition.x, _eventPosition.y, RecTransform.position.z);
            var min = _startDragPos - b;
            var plust = _startDragPos + b;
            var up =   new Vector3(1, -1, 0); 
            var left = new Vector3(-1, -1, 0); 
            
            Debug.DrawLine(_startDragPos,_startDragPos+ b,Color.yellow);
            Debug.DrawLine(_startDragPos,_startDragPos+up,Color.green);
            Debug.DrawLine(_startDragPos, _startDragPos+left,Color.red);
            Debug.DrawLine(_startDragPos, _startDragPos + min,Color.blue);
            
            var dot = Vector3.Dot(up, min);
            var dot2 = Vector3.Dot(left, min);
            var upleft = Mathf.Sign(dot);
            float dowmright = Mathf.Sign(dot2);

            float sum = upleft + dowmright;
            
            int y = sum < 0 ? -1 : (sum > 0 ?  1 : 0);
            int x = sum !=0 ? 0 : (upleft > 0 ? -1 : 1);
            _direction = new Vector3Int(x, y, 0);

            float inputdistance = Vector3.Distance(_startDragPos, b);
            var distance = Math.Min(inputdistance, _cellSize);
            
            RecTransform.position = _startDragPos + (_direction * distance);
        }

        public void SetDragContainer(GameObject DragContainer)
        {
            _dragContainer = DragContainer;
        }
        

        private void Start()
        {
            _camera =  Camera.main;
            _cellSize = _camera.ScreenToWorldPoint(Vector3.one * RecTransform.rect.width / 3f).x - _camera.ScreenToWorldPoint(Vector3.zero).x;
        }
#if UNITY_EDITOR

        public void OnDrawGizmos()
        {
            GUIStyle guiStyle = new GUIStyle();
            guiStyle.normal.textColor = Color.green;
            guiStyle.fontSize = 18;
        
           
            
            if (_eventPosition != Vector2.zero)
            {
                UnityEditor.Handles.color = Color.yellow;
                UnityEditor.Handles.DrawLine(_startDragPos,_eventPosition,2);
                
                Vector3 up = _startDragPos + Vector3.up;
                UnityEditor.Handles.color = Color.red;
                UnityEditor.Handles.DrawLine(_startDragPos,up,2);
        
                // var dpt = Vector2.Dot(up,)
                UnityEditor.Handles.Label(transform.position, $"<b>{_id}</b>" ,guiStyle);
            }
            else
            {
                UnityEditor.Handles.Label(transform.position, $"<b>{_id}</b>" ,guiStyle);
            }
        }
#endif
    }
}