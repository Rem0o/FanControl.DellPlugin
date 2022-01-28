using DellFanManagement.DellSmbiozBzhLib;
using FanControl.Plugins;

namespace FanControl.DellPlugin
{
    public class DellFanManagementControlSensor: IPluginControlSensor
    {
        private readonly BzhFanIndex _fanIndex;
        private bool _isSet = false;
        private float? _val;

        public DellFanManagementControlSensor(BzhFanIndex fanIndex) => _fanIndex = fanIndex;

        public float? Value { get; private set; }

        public string Name => $"Dell Control {(int)_fanIndex + 1}";

        public string Origin => $"DellSmbiosBzh";

        public string Id => "Control_" + _fanIndex.ToString();

        public void Reset()
        {
            DellSmbiosBzh.EnableAutomaticFanControl(_fanIndex == BzhFanIndex.Fan1 ? false : true);
            _isSet = false;
        }

        public void Set(float val)
        {
            if (!_isSet)
            {
                DellSmbiosBzh.DisableAutomaticFanControl(_fanIndex == BzhFanIndex.Fan1 ? false : true);
                _isSet = true;
            }

            _val = val;
            BzhFanLevel fanLevel = GetFanLevel(val);
            DellSmbiosBzh.SetFanLevel(_fanIndex, fanLevel);
        }

        public void Update() => Value = _val;

        private BzhFanLevel GetFanLevel(float val)
        {
            if (val < 33.33)
                return BzhFanLevel.Level0;
            else if (val < 66.66)
                return BzhFanLevel.Level1;
            else
                return BzhFanLevel.Level2;
        }
    }
}
