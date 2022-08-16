using System.Collections.Generic;
using UnityEngine;

namespace View
{
    public class EffectView : MonoBehaviour
    {
        [field:SerializeField]
        public string Name { get; set; }

        [field:SerializeField]
        public float Duration { get; private set; }
        
        [SerializeField] private List<ParticleSystem> _particleSystems;
        private RectTransform _rt;
        
        
        public RectTransform RectTransform => _rt != null ? _rt : (_rt = transform as RectTransform);

        public void SetActiveForParticles(bool isActive)
        {
            _particleSystems.ForEach(i=> i.gameObject.SetActive(isActive));
            //+ other options
        }

    }
}