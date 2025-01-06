using System;

namespace FWB.PowerTool.Model
{
    public static class GuidList
    {
        // GUID for the package
        public const string guidFWBPowerToolPackageString = "90fa0204-58a6-4b51-9e71-e3d30347f8ae";
        public static readonly Guid guidFWBPowerToolPackage = new Guid(guidFWBPowerToolPackageString);

        // GUID for the command set
        public const string guidFWBPowerToolPackageCmdSetString = "6e1d6bd1-e284-4fe9-9f0e-57dae96d296d";
        public static readonly Guid guidFWBPowerToolPackageCmdSet = new Guid(guidFWBPowerToolPackageCmdSetString);

    }
}
