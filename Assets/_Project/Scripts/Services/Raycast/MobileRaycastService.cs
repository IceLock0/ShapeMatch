using UnityEngine;

namespace _Project.Scripts.Services.Raycast
{
    public class MobileRaycastService : BaseRaycastService
    {
        public override bool TryGetComponent<T>(out T targetComponent) where T : class
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    var result = base.TryGetComponent<T>(out var component);
                    targetComponent = component;
                    return result;
                }
            }

            targetComponent = null;
            return false;
        }
    }
}