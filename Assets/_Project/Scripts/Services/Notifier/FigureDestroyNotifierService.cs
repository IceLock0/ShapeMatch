using System;

namespace _Project.Scripts.Services.Notifier
{
    public class FigureDestroyNotifierService : IFigureDestroyNotifierService
    {
        public void Notify()
        {
            FigureDestroyed?.Invoke();
        }
        
        public event Action FigureDestroyed;
    }
}