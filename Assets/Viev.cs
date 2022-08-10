using UnityEngine;

namespace DefaultNamespace
{
    public class Viev : MonoBehaviour
    {
        private Model _model;

        public void Init(Model model)
        {
            _model = model;
        }
    }
}