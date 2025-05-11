using UnityEngine;

namespace Code.Gameplay.LevelGenerator.Configs
{
    [CreateAssetMenu(fileName = "LevelDifficultyConfig", menuName = "Configs/Level Difficulty Config")]
    public class LevelDifficultyConfig : ScriptableObject
    {
        [field: Header("Min")]
        [field: SerializeField] public float MinObstacleRadiusMultiplayer { get; private set; }


        [field: Header("Max")]
        [field: SerializeField] public float MaxObstacleRadiusMultiplayer { get; private set; }

        [field: Space]
        [field: SerializeField] public AnimationCurve DifficultyCurve { get; private set; }
        [field: SerializeField] public float  PointsToMaxDifficulty { get; private set; }
        
        
        public float GetObstacleRadiusMultiplier(float score)
        {
            return Mathf.Lerp(MinObstacleRadiusMultiplayer, MaxObstacleRadiusMultiplayer, score/PointsToMaxDifficulty);
        }

    }
}