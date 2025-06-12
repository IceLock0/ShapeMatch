using System;
using _Project.Scripts.Figure;
using _Project.Scripts.Utils;

namespace _Project.Scripts.Providers.FigureField
{
    public interface IFigureFieldProvider : IInitializable, IDisposable
    {
        public bool IsFieldEmpty();
        public void DestroyFigure(FigureView figureView);
    }
}