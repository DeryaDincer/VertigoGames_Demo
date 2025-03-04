using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VertigoGames.Settings
{
    [CreateAssetMenu (fileName = "AnimationSettings", menuName = "VertigoGames/AnimationSettings")]
    public class AnimationSettings : ScriptableObject
    {
        [Title("Animation Item Settings")]
        
        [SerializeField] private int _spawnanimationItemRadius = 40;

        public int SpawnanimationItemRadius => _spawnanimationItemRadius;
    }
}