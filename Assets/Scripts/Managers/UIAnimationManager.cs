using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VertigoGames.Managers
{
    public class UIAnimationManager : MonoBehaviour
    {
        public GameObject rewardIconPrefab; 
        public Transform target; 
        public float animationDuration = 1f; 

        public void CreateRewardAnimation(Vector3 startPosition, int rewardAmount)
        {
            GameObject rewardIcon = Instantiate(rewardIconPrefab, startPosition, Quaternion.identity);
            StartCoroutine(AnimateReward(rewardIcon, rewardAmount));
        }

        private IEnumerator AnimateReward(GameObject rewardIcon, int rewardAmount)
        {
            float elapsedTime = 0;
            Vector3 startPosition = rewardIcon.transform.position;

            while (elapsedTime < animationDuration)
            {
                rewardIcon.transform.position = Vector3.Lerp(startPosition, target.position, elapsedTime / animationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            rewardIcon.transform.position = target.position;
            Destroy(rewardIcon);

            // Reward miktarını işle
            HandleReward(rewardAmount);
        }

        private void HandleReward(int rewardAmount)
        {
            // Reward miktarını işleyecek kod
            Debug.Log("Reward Collected: " + rewardAmount);
        }
    }
}

