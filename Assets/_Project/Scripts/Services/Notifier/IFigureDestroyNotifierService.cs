using System;

namespace _Project.Scripts.Services.Notifier
{
    public interface IFigureDestroyNotifierService
    {
        public void Notify();
        public event Action FigureDestroyed;
    }
}