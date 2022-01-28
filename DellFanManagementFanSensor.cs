using DellFanManagement.DellSmbiozBzhLib;
using FanControl.Plugins;

namespace FanControl.DellPlugin
{
    public class DellFanManagementFanSensor : IPluginSensor
    {
        private readonly BzhFanIndex _fanIndex;

        public DellFanManagementFanSensor(BzhFanIndex fanIndex) => _fanIndex = fanIndex;

        public string Identifier => $"Dell/FanSensor/{(int)_fanIndex}";

        public float? Value { get; private set; }

        public string Name => $"Dell Fan {(int)_fanIndex + 1}";

        public string Origin => $"DellSmbiosBzh";

        public string Id => "Fan_" + _fanIndex.ToString();

        public void Update() => Value = DellSmbiosBzh.GetFanRpm(_fanIndex);
    }
}
