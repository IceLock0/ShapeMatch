using System;
using System.Collections.Generic;
using _Project.Scripts.Figure;
using _Project.Scripts.Providers;
using _Project.Scripts.Providers.FigureField;

namespace _Project.Scripts.ActionBar
{
    public class ActionBarModel
    {
        private readonly IFigureFieldProvider _figureFieldProvider;
        
        private readonly List<FigureView> _figures = new();

        private readonly int _matchesCount;
        private readonly int _size;

        public event Action<FigureView> FigureRemoved;
        
        public ActionBarModel(IFigureFieldProvider figureFieldProvider, int matchesCount, int size)
        {
            _figureFieldProvider = figureFieldProvider;
            
            _matchesCount = matchesCount;
            _size = size;
        }

        public void AddFigure(FigureView figure)
        {
            _figures.Add(figure);
            
            CheckSimilar(figure);
        }
        
        public bool IsBarFull() =>
            _figures.Count == _size;
        
        public void RefreshList() =>
            _figures.Clear();

        private void CheckSimilar(FigureView figure)
        {
            var similarFigures = new List<FigureView>();
            
            for (var i = 0; i < _figures.Count ; i++)
            {
                if (_figures[i].Equals(figure))
                    similarFigures.Add(_figures[i]);
                
                if(similarFigures.Count == _matchesCount)
                {
                    foreach (var similar in similarFigures)
                    {
                        _figureFieldProvider.DestroyFigure(similar);
                        
                        _figures.Remove(similar);
                        
                        FigureRemoved?.Invoke(similar);
                    }
                    
                    break;
                }
            }
        }
    }
}