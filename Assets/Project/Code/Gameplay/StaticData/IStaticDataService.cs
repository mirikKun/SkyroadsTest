using Code.Gameplay.LevelGenerator.Configs;
using Code.Gameplay.Player.Configs;
using Code.Gameplay.Windows;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        GameObject GetWindowPrefab(WindowId id);
        LevelDifficultyConfig GetLevelDifficultyConfig();
        LevelGeneratorConfig GetLevelGeneratorConfig();
        PlayerMovementConfig GetPlayerMovementConfig();
    }
}