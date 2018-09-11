using GmudGen.Domain.Steps;
using GmudGen.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GmudGen.Domain
{
    class Prefs
    {
        public static Info Info;
        public static string PortletTemplate;
        public static string ServiceTemplate;
        public static string SnxTemplate;
        public static string BackupTemplate;
        public static string ControlPointTemplate;

        public static List<ISteps> ListSteps;

        public static void Init()
        {
            Info = new Info();

            ListSteps = new List<ISteps>();

            PortletTemplate = "portlet";
            ServiceTemplate = "service";
            SnxTemplate = "snx";
            BackupTemplate = "backup";
            ControlPointTemplate = "control-point";
        }

        public static void ReloadArtifacts()
        {
            ListSteps = new List<ISteps>();
        }
    }
}
