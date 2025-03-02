using System;
using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VertigoGames.Datas.Reward;

[CreateAssetMenu(fileName = "ZoneData", menuName = "VertigoGames/Zone/ZoneData")]
public class ZoneData : ScriptableObject
{
    public ZoneType ZoneType => zoneType;
    public int ZoneActivationInterval => _zoneActivationInterval;
    public List<RewardData> GuaranteedRewards => _guaranteedRewards;
    public List<RewardData> RandomRewards=> _randomRewards;
    public RewardWeight RewardWeight=> _rewardWeight;
    
    [Title("Zone Settings")]
    [SerializeField] private ZoneType zoneType;
    [SerializeField] private int _zoneActivationInterval;
    
    [Title("Rewards")]
    [SerializeField] private List<RewardData> _guaranteedRewards;
    [SerializeField] private List<RewardData> _randomRewards;
    [SerializeField] private RewardWeight _rewardWeight;
}
