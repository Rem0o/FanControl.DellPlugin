using DellFanManagement.Interop;
using FanControl.Plugins;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FanControl.DellPlugin
{
    public class DellPlugin : IPlugin
    {
        private const string SYS_FILE = "bzh_dell_smm_io_x64.sys";
        private bool _dellInitialized;
        private FileInfo _copiedSysFile;

        public string Name => "Dell";

        public void Close()
        {
            if (_dellInitialized)
            {
                DellFanLib.EnableEcFanControl(false);
                DellFanLib.EnableEcFanControl(true);
                DellFanLib.Shutdown();

                _copiedSysFile.Delete();
            }
        }

        public void Initialize()
        {
            FileInfo sysFile = new FileInfo(typeof(DellFanLib).Assembly.Location).Directory.GetFiles(SYS_FILE).FirstOrDefault();
            _copiedSysFile = sysFile.CopyTo(Path.Combine(Directory.GetCurrentDirectory(), SYS_FILE), true);

            _dellInitialized = DellFanLib.Initialize();

        }

        public void Load(IPluginSensorsContainer _container)
        {
            if (_dellInitialized)
            {
                IEnumerable<DellFanManagementControlSensor> fanControls = new[] {
                            FanIndex.Fan1,
                            FanIndex.Fan2
                        }.Select(i => new DellFanManagementControlSensor(i)).ToArray();

                IEnumerable<DellFanManagementFanSensor> fanSensors = new[] {
                            FanIndex.Fan1,
                            FanIndex.Fan2
                        }.Select(i => new DellFanManagementFanSensor(i)).ToArray();

                _container.ControlSensors.AddRange(fanControls);
                _container.FanSensors.AddRange(fanSensors);
            }
        }
    }
}
