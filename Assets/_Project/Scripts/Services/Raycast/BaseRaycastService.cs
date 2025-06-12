using UnityEngine;

namespace _Project.Scripts.Services.Raycast
{
    public class BaseRaycastService : IRaycastService
    {
        private readonly Camera _camera = Camera.main;

        public virtual bool TryGetComponent<T>(out T component) where T : class
        {
            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            var hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider 
                && hit.collider.TryGetComponent<IRaycastable>(out var raycastable) 
                && raycastable.CanRaycast 
                && hit.collider.TryGetComponent<T>(out var TComponent))
            {
                component = TComponent;
                return true;
            }

            component = null;
            return false;
        }
    }
}