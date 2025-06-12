using System.Collections.Generic;
using _Project.Scripts.Figure;

namespace _Project.Scripts.Factories.Figure
{
    public interface IFigureFactory
    {
        public List<FigureView> Create(int count);
    }
}
