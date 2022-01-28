using FanControl.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DellFanManagement.DellSmbiozBzhLib;

namespace FanControl.DellPlugin
{
    public class DellPlugin : IPlugin, IDisposable
    {
        private const string SYS_FILE = "bzh_dell_smm_io_x64.sys";
        private bool _dellInitialized;
        private FileInfo _copiedSysFile;
        private Boolean m_DisposedValue;

        public string Name => "Dell";

        public void Close()
        {
            if (_dellInitialized)
            {
                DellSmbiosBzh.EnableAutomaticFanControl(true);
                DellSmbiosBzh.EnableAutomaticFanControl(false);
                DellSmbiosBzh.Shutdown();

                _copiedSysFile.Delete();
                _copiedSysFile = null;
                _dellInitialized = false;
            }
        }

        public void Initialize()
        {
            var copyLocation = Path.Combine(Directory.GetCurrentDirectory(), SYS_FILE);
            if (File.Exists(copyLocation))
                File.Delete(copyLocation);

            FileInfo sysFile = new FileInfo(typeof(DellSmbiosBzh).Assembly.Location).Directory.GetFiles(SYS_FILE).FirstOrDefault();
            _copiedSysFile = sysFile.CopyTo(copyLocation, true);

            _dellInitialized = DellSmbiosBzh.Initialize();

        }

        public void Load(IPluginSensorsContainer _container)
        {
            if (_dellInitialized)
            {
                IEnumerable<DellFanManagementControlSensor> fanControls = new[] {
                            BzhFanIndex.Fan1,
                            BzhFanIndex.Fan2
                        }.Select(i => new DellFanManagementControlSensor(i)).ToArray();

                IEnumerable<DellFanManagementFanSensor> fanSensors = new[] {
                            BzhFanIndex.Fan1,
                            BzhFanIndex.Fan2
                        }.Select(i => new DellFanManagementFanSensor(i)).ToArray();

                _container.ControlSensors.AddRange(fanControls);
                _container.FanSensors.AddRange(fanSensors);
            }
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!m_DisposedValue)
            {
                if (disposing)
                {
                  // TODO: dispose managed state (managed objects)
                }

              // TODO: free unmanaged resources (unmanaged objects) and override finalizer
              // TODO: set large fields to null
              Close();
              m_DisposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
         ~DellPlugin()
         {
             // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
             Dispose(disposing: false);
         }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
