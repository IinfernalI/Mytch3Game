using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class InitializeALL : MonoBehaviour
    {
        private Model _model = new Model();
        private Viev _viev = new Viev();

        private void Awake()
        {
            _viev.Init(_model);
        }
    }
}