using Random = UnityEngine.Random;

namespace _Project.Scripts.Extensions
{
    public static class ArrayExtensions
    {
        public static T GetRandom<T>(this T[] source)
        {
            var rndIndex = Random.Range(0, source.Length);
            
            return source[rndIndex];
        }
    }
}