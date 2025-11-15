using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "ScriptableObjects/LevelsData", order = 1)]
    public class LevelsData : ScriptableObject
    {
        public List<LevelData> Levels;
        public int CurrentLevelIndex = 0;
        public LevelData CurrentLevel => Levels[CurrentLevelIndex];
        public void AdvanceToNextLevel()
        {
            if (CurrentLevelIndex < Levels.Count - 1)
            {
                CurrentLevelIndex++;
            }
        }
        public void ResetLevels()
        {
            foreach (var level in Levels)
            {
                level.ResetLevel();
            }
            CurrentLevelIndex = 0;
        }

        public bool IsCompleted() => Levels.TrueForAll(level => level.IsCompleted());
    }
}
