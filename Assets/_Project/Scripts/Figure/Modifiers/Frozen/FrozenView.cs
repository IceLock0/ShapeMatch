using _Project.Scripts.Services.Notifier;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Figure.Modifiers.Frozen
{
    public class FrozenView : MonoBehaviour
    {
        [SerializeField] private int _minFigureToUnfroze = 6;
        [SerializeField] private int _maxFigureToUnfroze = 24;
        
        private int _figureToUnfroze;

        private IFigureDestroyNotifierService _figureDestroyNotifierService;

        private FigureView _figureView;
        
        public void Initialize(IFigureDestroyNotifierService figureDestroyNotifierService)
        {
            _figureDestroyNotifierService = figureDestroyNotifierService;

            _figureDestroyNotifierService.FigureDestroyed += OnFigureDestroy;

            _figureView = GetComponentInParent<FigureView>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            
            _figureToUnfroze = Random.Range(_minFigureToUnfroze, _maxFigureToUnfroze + 1);

            _figureView.CanRaycast = false;
        }

        private void OnFigureDestroy()
        {
            _figureToUnfroze--;
            
            if(_figureToUnfroze <= 0)
                Hide();
        }
        
        private void Hide()
        {
            gameObject.SetActive(false);
            
            _figureView.CanRaycast = true;
        }
        
        private void OnDisable()
        {
            _figureDestroyNotifierService.FigureDestroyed -= OnFigureDestroy;
        }
    }
}