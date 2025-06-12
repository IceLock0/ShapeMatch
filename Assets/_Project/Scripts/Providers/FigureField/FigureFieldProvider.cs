using System;
using System.Collections.Generic;
using _Project.Scripts.Factories.Figure;
using _Project.Scripts.Figure;
using _Project.Scripts.Services.Notifier;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Providers.FigureField
{
    public class FigureFieldProvider : IFigureFieldProvider
    {
        private readonly IFigureFactory _figureFactory;
        private readonly IFigureDestroyNotifierService _figureDestroyNotifierService;

        private readonly RefreshButtonUIView _refreshButtonUIView;

        private readonly int _figureToCreate;
        private readonly int _matchesCount;

        private List<FigureView> _figures;

        public FigureFieldProvider(IFigureFactory figureFactory, IFigureDestroyNotifierService figureDestroyNotifierService, 
            RefreshButtonUIView refreshButtonUIView,
            int figureToCreate, int matchesCount)
        {
            _figureFactory = figureFactory;
            _figureDestroyNotifierService = figureDestroyNotifierService;

            _refreshButtonUIView = refreshButtonUIView;

            _figureToCreate = figureToCreate;
            _matchesCount = matchesCount;
        }

        public void Initialize()
        {
            _figures = _figureFactory.Create(_figureToCreate);

            _refreshButtonUIView.Refreshed += OnRefresh;
        }

        public void Dispose()
        {
            _refreshButtonUIView.Refreshed -= OnRefresh;
        }

        public void DestroyFigure(FigureView figureView)
        {
            if (_figures.Contains(figureView))
            {
                _figures.Remove(figureView);
                GameObject.Destroy(figureView.gameObject);

                _figureDestroyNotifierService.Notify();
            }
        }

        public bool IsFieldEmpty() =>
            _figures.Count == 0;

        private void OnRefresh()
        {
            foreach (var figure in _figures)
                GameObject.Destroy(figure.gameObject);

            _figures = _figureFactory.Create(_figures.Count / _matchesCount);
        }
    }
}