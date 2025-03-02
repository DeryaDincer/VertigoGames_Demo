using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VertigoGames.Controllers
{
    public class ZoneStateController
    {
        public int CurrentZoneIndex => _currentZoneIndex;
        
        private int _currentZoneIndex;
        private List<ZoneData> _zoneDatas = new();

        public ZoneStateController(List<ZoneData> zoneDatas)
        {
            _zoneDatas = zoneDatas;
        }

        public void UpdateCurrentZoneIndex()
        {
            _currentZoneIndex++;
        }
        
        public ZoneData FindCurrentZone()
        {
            // ZoneData listesini ZoneActivationInterval'a göre büyükten küçüğe sırala
            var sortedZones = _zoneDatas.OrderByDescending(z => z.ZoneActivationInterval).ToList();

            foreach (var zone in sortedZones)
            {
                if (_currentZoneIndex % zone.ZoneActivationInterval == 0)
                {
                    return zone; 
                }
            }

            Debug.LogError($"No matching zone found for currentZoneIndex: {_currentZoneIndex}");
            return null; 
        }
  
    }
}

