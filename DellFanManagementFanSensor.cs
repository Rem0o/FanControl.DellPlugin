using DellFanManagement.Interop;
using FanControl.Plugins;

namespace FanControl.DellPlugin
{
    public class DellFanManagementFanSensor : IPluginSensor
    {
        private readonly FanIndex _fanIndex;

        public DellFanManagementFanSensor(FanIndex fanIndex)
        {
            _fanIndex = fanIndex;
        }

        public string Identifier => $"Dell/FanSensor/{(int)_fanIndex}";

        public float? Value { get; private set; }

        public string Name => $"Dell Fan {(int)_fanIndex + 1}";

        public string Origin => $"DellFanLib {DellFanLib.Version}";

        public void Update()
        {
            Value = DellFanLib.GetFanRpm(_fanIndex);
        }
    }
}
