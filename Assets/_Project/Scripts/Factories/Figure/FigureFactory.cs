using System;
using System.Collections.Generic;
using _Project.Scripts.Extensions;
using _Project.Scripts.Figure;
using _Project.Scripts.Figure.Modifiers;
using _Project.Scripts.Figure.Modifiers.Frozen;
using _Project.Scripts.Services.Notifier;
using _Project.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Factories.Figure
{
    public class FigureFactory : IFigureFactory
    {
        private readonly IFigureDestroyNotifierService _figureDestroyNotifierService;
        
        private readonly Sprite[] _animalSprites;
        private readonly Transform[] _spawnPoints;
        private readonly Transform _container;
        
        private readonly int _matchesCount;
        
        private FigureView[] _figureViews;

        private const float FIGURE_WITH_MODIFIER_CHANCE = 30.0f;
        
        public FigureFactory(IFigureDestroyNotifierService figureDestroyNotifierService, Sprite[] animalSprites, Transform[] spawnPoints,int matchesCount)
        {
            _figureDestroyNotifierService = figureDestroyNotifierService;
            
            _animalSprites = animalSprites;
            _spawnPoints = spawnPoints;

            _container = new GameObject("FigureContainer").transform;

            _matchesCount = matchesCount;
            
            Load();
        }

        public List<FigureView> Create(int count)
        {
            var figures = new List<FigureView>();
            
            var similarFigurePositions = new Vector2[_matchesCount];
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < similarFigurePositions.Length; j++)
                {
                    var rndPosition = _spawnPoints.GetRandom();
                    similarFigurePositions[j] = rndPosition.position;
                }
                CreateFigure(similarFigurePositions, figures);
            }

            return figures;
        }
        
        private void CreateFigure(Vector2[] positions, List<FigureView> figures)
        {
            var prefab = _figureViews.GetRandom();

            var color = GetRandomColor();
            var animal = _animalSprites.GetRandom();
            
            for(var i = 0; i < positions.Length; i++)
            {
                var rotation = GetRandomRotation();
                
                var created = GameObject.Instantiate(prefab, positions[i], rotation, _container);

                IFigureModifier figureModifier = null;
                if(RandomLibrary.TryChance(FIGURE_WITH_MODIFIER_CHANCE))
                    figureModifier = GetRandomModifier(created);
                
                created.Initialize(color, animal, figureModifier);
                
                figures.Add(created);
            }
        }
        
        private void Load()
        {
            _figureViews = Resources.LoadAll<FigureView>(AssetLabels.FIGURES);
        }
        
        private Color GetRandomColor()
        {
            var r = Random.Range(0f, 1f);
            var g = Random.Range(0f, 1f);
            var b = Random.Range(0f, 1f);
            
            return new Color(r, g, b);
        }

        private Quaternion GetRandomRotation()
        {
            var rndZ = Random.Range(0f, 360f);
            
            return Quaternion.Euler(0f, 0f, rndZ);
        }

        private IFigureModifier GetRandomModifier(FigureView figureView)
        {
            var modifiersCount = Enum.GetValues(typeof(ModifierType)).Length;
            
            var rnd = Random.Range(0, modifiersCount);

            var modifierType = (ModifierType)rnd;

            IFigureModifier modifier = null;
            
            switch (modifierType)
            {
                case ModifierType.Heavy:
                    var rb = figureView.GetComponent<Rigidbody2D>();
                    modifier = new HeavyFigureModifier(rb);
                    break;
                case ModifierType.Frozen:
                    var frozenView = figureView.GetComponentInChildren<FrozenView>(true);
                    frozenView.Initialize(_figureDestroyNotifierService);
                    modifier = new FrozenFigureModifier(frozenView);
                    break;
                default:
                    throw new ArgumentException("Unexpected modifier type");
            }
            
            return modifier;
        }
    }
}