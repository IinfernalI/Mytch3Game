using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    [Serializable]
    public class Model
    {
        public event Action<List<ElementData>> OnSetData;
        public event Func<ElementData, ElementData, float> OnSwap;
        public event Action<int,int,int> OnScoreChangedBy;

        public List<ElementData> Elements = new();
        public int _score;

        public int Score => _score;

        public void ScoreAdd(int value)
        {
            var oldValue = _score;
            _score += value;
            OnScoreChangedBy?.Invoke(oldValue,value,_score);
        }

        public void SetData(List<ElementData> elementDatas) =>
            OnSetData?.Invoke(elementDatas);

        public float SwapData(ElementData a, ElementData b) =>
            OnSwap?.Invoke(a, b) ?? 0;

        public int Height { get; set; } = 12;
        public int Weight { get; set; } = 8;

        public bool AnyDeadElements() =>
            Elements.Any(i => i.State == ElementState.IsDead);

        public void ResetScore() => 
            _score = 0;
    }
}