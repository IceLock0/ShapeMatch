using UnityEngine;

namespace _Project.Scripts.Figure.Modifiers
{
    public class HeavyFigureModifier : IFigureModifier
    {
        private readonly Rigidbody2D _rb;

        private const float GRAVITY_FACTOR = 0.5f;
        
        public HeavyFigureModifier(Rigidbody2D rb)
        {
            _rb = rb;
        }
        
        public void Apply()
        {
            var targetGravityScale = _rb.gravityScale * (1 + GRAVITY_FACTOR);
            
            _rb.gravityScale = targetGravityScale;
        }
    }
}