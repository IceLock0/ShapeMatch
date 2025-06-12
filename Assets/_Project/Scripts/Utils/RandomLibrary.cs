using UnityEngine;

namespace _Project.Scripts.Utils
{
    public static class RandomLibrary
    {
        public static bool TryChance(float chance)
        {
            var rnd = Random.Range(0.0f, 100.0f);

            return rnd <= chance;
        }
    }
}