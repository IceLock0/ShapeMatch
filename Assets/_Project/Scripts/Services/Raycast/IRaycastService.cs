namespace _Project.Scripts.Services.Raycast
{
    public interface IRaycastService
    {
        public bool TryGetComponent<T>(out T targetComponent) where T : class;
    }
}