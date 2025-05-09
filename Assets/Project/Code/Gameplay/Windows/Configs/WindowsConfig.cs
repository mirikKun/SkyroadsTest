using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Windows.Configs
{
    [CreateAssetMenu(fileName = "windowConfig", menuName = "Configs/Window Config")]
    public class WindowsConfig : ScriptableObject
    {
        public List<WindowConfig> WindowConfigs;
    }
}