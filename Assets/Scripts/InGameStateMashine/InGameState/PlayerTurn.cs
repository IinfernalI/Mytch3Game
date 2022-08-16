using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using GameStateMashine.StatesGame;
using UnityEngine;

namespace InGameStateMashine.InGameState
{
    public class PlayerTurn : IInGameState
    {
        private StartState _startState;
        private int? lastClickId;
        public PlayerTurn(StartState startState, Action<Type> callback)
        {
            _startState = startState;
            IsDone = callback;
        }

        public event Action<Type> IsDone;

        public void Init()
        {
            lastClickId = null;
            Debug.Log($"init: this.GetType() ={this.GetType()}");
            _startState.View.PlayerCanUseInput(true);
            //_startState.View.OnClick += OnClickHandler;
            _startState.View.OnEndDrag += OnEndDragHandler;
            _startState.View.OnSave += OnSaveHandler;
            _startState.View.OnLoad += OnLoadHandler;
        }

        private void OnSaveHandler()
        {
            _startState.Controller.Save(_startState.Model._score,_startState.Model.Elements);
            
            Debug.Log($"Save done");
        }

        private void OnLoadHandler()
        {
            Debug.Log($"Loading");
            _startState.Controller.Load();
        }

        private ElementData GetNeighbour(ElementData data, Vector2Int direction)
        {
            Vector2Int neighborPos = direction + new Vector2Int(data.xPos, data.yPos);
            return _startState.Model.Elements.FirstOrDefault(i => i.xPos == neighborPos.x && i.yPos == neighborPos.y);
        }

        private void OnEndDragHandler(int id, Vector2Int direction)
        {
            ElementData data = _startState.Controller.GetById(id);
            ElementData neighbour = GetNeighbour(data, direction);
            if (neighbour != null)
            {
                lastClickId = neighbour.ID;
                OnClickHandler(id);
            }
            else
            {
                Debug.Log($"you cannot do it");
            }
        }

        private void OnClickHandler(int id)
        {
            _startState.View.OnEndDrag -= OnEndDragHandler;
            if (lastClickId.HasValue)
            {
                if (_startState.Controller.IsNeighbours(lastClickId.Value, id))
                {
                   
                   _startState.Controller.Swap(lastClickId.Value, id, TryFoundItMatches);
                }
                else
                {
                    _startState.View.OnEndDrag += OnEndDragHandler;
                    Debug.Log($"you cannot do it");
                    lastClickId = null;
                }
            }
            else
            {
                lastClickId = id;
                _startState.View.OnEndDrag += OnEndDragHandler;
            }

            void TryFoundItMatches()
            {
                if (_startState.Controller.TryFoundMutches(out List<List<ElementData>> deadCandidates))
                {
                    Debug.Log($"found matches :{deadCandidates.Count}");
                    IsDone?.Invoke(GetType());
                }
                else
                {
                    _startState.Controller.Swap(lastClickId.Value, id, NoMatchFound);
                }
            }
        }

        private void NoMatchFound()
        {
            lastClickId = null;
            _startState.View.OnEndDrag += OnEndDragHandler;
            Debug.Log($"No matches");
        }


        public void Exit()
        {
            lastClickId = null;
            _startState.View.PlayerCanUseInput(false);
            _startState.View.OnEndDrag -= OnEndDragHandler;
            _startState.View.OnSave -= OnSaveHandler;
            _startState.View.OnLoad -= OnLoadHandler;
        }
    }
}