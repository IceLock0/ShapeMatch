using _Project.Scripts.Figure.Modifiers;
using _Project.Scripts.Services.Raycast;
using UnityEngine;

namespace _Project.Scripts.Figure
{
    public class FigureView : MonoBehaviour, IRaycastable
    {
        [field: SerializeField] public SpriteRenderer ColorSprite { get; private set; }
        [field: SerializeField] public SpriteRenderer AnimalSprite { get; private set; }
        
        private Rigidbody2D _rb;
        private Collider2D _collider;

        private IFigureModifier _figureModifier;
        
        public SpriteRenderer ShapeSprite { get; private set; }

        public bool CanRaycast { get; set; } = true;
        
        public void Initialize(Color color, Sprite animalSprite, IFigureModifier figureModifier)
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();

            ShapeSprite = GetComponent<SpriteRenderer>();
            ColorSprite.color = color;
            AnimalSprite.sprite = animalSprite;
            
            _figureModifier = figureModifier;
            _figureModifier?.Apply();
        }

        public void DisablePhysics()
        {
            _collider.enabled = false;
            _rb.simulated = false;
        }

        public override bool Equals(object other)
        {
            if (other is not FigureView figureView)
                return false;

            return ColorSprite.color.Equals(figureView.ColorSprite.color)
                   && AnimalSprite.sprite.name == figureView.AnimalSprite.sprite.name
                   && ShapeSprite.sprite.name == figureView.ShapeSprite.sprite.name;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 31 + ColorSprite.color.GetHashCode();
            hash = hash * 31 + (AnimalSprite.sprite != null ? AnimalSprite.sprite.name.GetHashCode() : 0);
            hash = hash * 31 + (ShapeSprite.sprite != null ? ShapeSprite.sprite.name.GetHashCode() : 0);
            return hash;
        }
    }
}