using _Project.Scripts.ActionBar;
using _Project.Scripts.Factories.Figure;
using _Project.Scripts.Providers;
using _Project.Scripts.Providers.FigureField;
using _Project.Scripts.Services.Notifier;
using _Project.Scripts.Services.Raycast;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Root
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private GameObject _winScreenPrefab;
        [SerializeField] private GameObject _defeatScreenPrefab;

        [SerializeField] private RefreshButtonUIView _refreshButton;

        [SerializeField] private Sprite[] _animalSprites;
        [SerializeField] private Transform[] _spawnPoints;

        [SerializeField] private ActionBarView _actionBarView;

        [SerializeField] private int _matchesCount;
        [SerializeField] private int _figureToCreate;

        private IFigureFieldProvider _figureFieldProvider;

        private GameStateController _gameStateController;

        private void Awake()
        {
            IFigureDestroyNotifierService figureDestroyNotifierService = new FigureDestroyNotifierService();

            IFigureFactory figureFactory = new FigureFactory(figureDestroyNotifierService, _animalSprites, _spawnPoints,
                _matchesCount);

            _figureFieldProvider = new FigureFieldProvider(figureFactory, figureDestroyNotifierService, _refreshButton,
                _figureToCreate, _matchesCount);
            _figureFieldProvider.Initialize();

            IRaycastService raycastService = GetRaycastService();

            _actionBarView.Initialize(_figureFieldProvider, raycastService, _refreshButton, _matchesCount);

            _gameStateController = new GameStateController(_figureFieldProvider, _actionBarView, _canvas,
                _winScreenPrefab, _defeatScreenPrefab);
            _gameStateController.Initialize();
        }

        private void OnDisable()
        {
            _figureFieldProvider.Dispose();
            _gameStateController.Dispose();
        }

        private IRaycastService GetRaycastService()
        {
#if UNITY_EDITOR
            return new PCRaycastService();
#elif UNITY_ANDROID
            return new MobileRaycastService();
#endif
        }
    }
}