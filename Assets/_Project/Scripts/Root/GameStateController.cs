using System;
using _Project.Scripts.ActionBar;
using _Project.Scripts.Providers;
using _Project.Scripts.Providers.FigureField;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Root
{
    public class GameStateController : IInitializable, IDisposable
    {
        private readonly IFigureFieldProvider _figureFieldProvider;

        private readonly ActionBarView _actionBarView;
        private readonly Canvas _canvas;
        private readonly GameObject _winScreenPrefab;
        private readonly GameObject _defeatScreenPrefab;
        
        public GameStateController(IFigureFieldProvider figureFieldProvider, ActionBarView actionBarView,
            Canvas canvas, GameObject winScreenPrefab, GameObject defeatScreenPrefab)
        {
            _figureFieldProvider = figureFieldProvider;
            _actionBarView = actionBarView;
            _canvas = canvas;

            _winScreenPrefab = winScreenPrefab;
            _defeatScreenPrefab = defeatScreenPrefab;
        }

        public void Initialize()
        {
            _actionBarView.Defeated += OnDefeated;
            _actionBarView.FigureCountChanged += OnFigureCountChanged;
        }

        public void Dispose()
        {
            _actionBarView.Defeated -= OnDefeated;
            _actionBarView.FigureCountChanged -= OnFigureCountChanged;
        }

        private void OnDefeated()
        {
            GameObject.Instantiate(_defeatScreenPrefab, _canvas.transform);
        }

        private void OnFigureCountChanged()
        {
            if (_figureFieldProvider.IsFieldEmpty())
                GameObject.Instantiate(_winScreenPrefab, _canvas.transform);
        }
    }
}