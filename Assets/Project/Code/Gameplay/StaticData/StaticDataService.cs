using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.LevelGenerator.Configs;
using Code.Gameplay.Player.Configs;
using Code.Gameplay.Windows;
using Code.Gameplay.Windows.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<WindowId, GameObject> _windowPrefabsById;
        private LevelDifficultyConfig _levelDifficultyConfig;
        private LevelGeneratorConfig _levelGenerationConfig;
        private PlayerMovementConfig _playerMovementConfig;

        public void LoadAll()
        {
            LoadWindows();
            LoadLevelGenerationConfigs();
            LoadPlayerConfigs();
        }

        public LevelDifficultyConfig GetLevelDifficultyConfig() => _levelDifficultyConfig;

        public LevelGeneratorConfig GetLevelGeneratorConfig() => _levelGenerationConfig;
        public PlayerMovementConfig GetPlayerMovementConfig() => _playerMovementConfig;


        public GameObject GetWindowPrefab(WindowId id) =>
            _windowPrefabsById.TryGetValue(id, out GameObject prefab)
                ? prefab
                : throw new Exception($"Prefab config for window {id} was not found");

        private void LoadWindows()
        {
            _windowPrefabsById = Resources
                .Load<WindowsConfig>("Configs/Windows/WindowConfigs")
                .WindowConfigs
                .ToDictionary(x => x.Id, x => x.Prefab);
        }

        private void LoadPlayerConfigs()
        {
            _playerMovementConfig = Resources.Load<PlayerMovementConfig>("Configs/Player/PlayerMovementConfig");
        }

        private void LoadLevelGenerationConfigs()
        {
            _levelDifficultyConfig = Resources
                .Load<LevelDifficultyConfig>("Configs/LevelGeneration/LevelDifficultyConfig");
            _levelGenerationConfig= Resources
                .Load<LevelGeneratorConfig>("Configs/LevelGeneration/LevelGeneratorConfig");
        }
    }
}