using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using DG.Tweening;
using Enum;
using Newtonsoft.Json;
using UnityEngine;
using Random = System.Random;

namespace Controller
{
    public class Controller
    {
        public event Action<int, List<ElementData>> OnLoad;
        
        private const string Savedata = "SaveData";
        private Model _model;
        private Random _random;
        private FindDublicateAlgoritm _findDublicateAlgoritm = new FindDublicateAlgoritm();


        public Controller(Model model, int score, List<ElementData> list)
        {
            _model = model;
            InitModel(score,list);
        }

        public Controller(Model model)
        {
            _model = model;
            InitModel();
        }

        public bool TryFoundMutches(out List<List<ElementData>> list)
        {
            list = _findDublicateAlgoritm.GetMatches(_model.Elements, _model.Height, _model.Weight);
            return list.Any();
        }

        private void InitModel(int score, List<ElementData> list)
        {
            _model.ResetScore();
            _model.ScoreAdd(score);
            _model.Elements = list;
            SetAllToFall();
            _model.SetData(list);
        }
        private void InitModel()
        {
            _model.ResetScore();
            int id = 0;
            _random = new Random();
            var enumCount = System.Enum.GetNames(typeof(ElementType)).Length;

            for (int y = 0; y < _model.Height; y++)
            {
                for (int x = 0; x < _model.Weight; x++)
                {
                    ElementType type = (ElementType)_random.Next(0, enumCount);
                    var element = new ElementData(type, ++id);

                    element.yPos = y;
                    element.xPos = x;
                    element.State = ElementState.Idle; // TODO to Newbie
                    _model.Elements.Add(element); //! y = 0 , x = 1 
                }
            }
            SetAllToFall();
            
        }

        public void SetDeadElements(List<ElementData> elementDatas)
        {
            foreach (var elementData in elementDatas)
            {
                elementData.State = ElementState.IsDead;
            }

            _model.SetData(elementDatas);
        }

        private void SetAllToFall()
        {
            foreach (ElementData element in _model.Elements)
            {
                element.State = ElementState.Newbie;
                element.FallDownPos = element.yPos;
                element.yPos -= _model.Height;
            }
        }

        public bool IsNeighbours(int idA, int idB)
        {
            ElementData elDataA = GetById(idA);
            ElementData elDataB = GetById(idB);

            return (elDataA.yPos == elDataB.yPos &&
                    (elDataA.xPos + 1 == elDataB.xPos || elDataA.xPos - 1 == elDataB.xPos))
                   || (elDataA.xPos == elDataB.xPos &&
                       (elDataA.yPos + 1 == elDataB.yPos || elDataA.yPos - 1 == elDataB.yPos));
        }


        public void Swap(int idA, int idB, Action callback)
        {
            ElementData elDataA = GetById(idA);
            ElementData elDataB = GetById(idB);

            (elDataA.xPos, elDataB.xPos) = (elDataB.xPos, elDataA.xPos);
            (elDataA.yPos, elDataB.yPos) = (elDataB.yPos, elDataA.yPos);

            float duration = _model.SwapData(elDataA, elDataB);
            DOVirtual.DelayedCall(duration, () => callback?.Invoke());
        }

        public ElementData GetById(int id)
        {
            foreach (var elData in _model.Elements)
            {
                if (elData.ID == id)
                    return elData;
            }

            return null;
        }

        public void Save(int score, List<ElementData> elements)
        {
            SaveData data = new SaveData();
            data.Score = score;
            data.Elements = elements;
            string json = JsonConvert.SerializeObject(data);
            Debug.Log($"{json}");
            PlayerPrefs.SetString(Savedata, json);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(Savedata) == false)
            {
                Debug.Log($"no saved data");
                return;
            }
            string json = PlayerPrefs.GetString(Savedata);
            var data = JsonConvert.DeserializeObject<SaveData>(json);
            Debug.Log($"response: {data.Score}: {data.Elements.Count}");
            OnLoad?.Invoke(data.Score,data.Elements);
        }
    }
}

[Serializable]
public class SaveData
{
    public int Score { get; set; }
    public List<ElementData> Elements { get; set; }
}