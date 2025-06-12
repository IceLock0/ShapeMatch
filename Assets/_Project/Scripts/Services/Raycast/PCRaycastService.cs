using Input = UnityEngine.Input;

namespace _Project.Scripts.Services.Raycast
{
    public class PCRaycastService : BaseRaycastService
    {
        public override bool TryGetComponent<T>(out T targetComponent) where T : class
        {
            if (Input.GetMouseButtonDown(0))
            {
                var result = base.TryGetComponent<T>(out var component);
                targetComponent = component;
                return result;
            }
            
            targetComponent = null;
            return false;
        }
    }
}