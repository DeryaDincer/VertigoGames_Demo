using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VertigoGames.Datas.Reward;

[CreateAssetMenu(fileName = "ZoneData", menuName = "VertigoGames/Zone/ZoneData")]
public class ZoneData : ScriptableObject
{
    public ZoneType ZoneType => zoneType;
    public List<RewardData> GuaranteedRewards => _guaranteedRewards;
    public List<RewardData> RandomRewards=> _randomRewards;
    public List<RewardWeightEntry> WeightDistribution=> _weightDistribution;
    
    [SerializeField] private ZoneType zoneType;
    [SerializeField] private List<RewardData> _guaranteedRewards;
    [SerializeField] private List<RewardData> _randomRewards;
    [SerializeField] private List<RewardWeightEntry> _weightDistribution;
}

[Serializable]
public class RewardWeightEntry
{
    public RewardWeight RewardWeightKey;
    public float Value;
}