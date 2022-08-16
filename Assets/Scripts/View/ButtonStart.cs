using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class ButtonStart : MonoBehaviour
    {
        public  Action OnClick;
 
        [SerializeField] private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(Call);
        }
    
        private void OnDisable()
        {
            button.onClick.RemoveListener(Call);
        }

        private void Call()
        {
            OnClick?.Invoke();
        }
    
    }
}
