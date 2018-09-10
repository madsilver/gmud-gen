using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GmudGen.Domain.Steps
{
    class BackupStep : ISteps
    {
        private DateTime dt;
        private List<string> files;

        public BackupStep()
        {
            this.dt = DateTime.Parse(Prefs.Info.Schedule);
            this.files = new List<string>();
        }

        public List<string> GetSteps()
        {
            string file = String.Format("{0}\\resources\\templates\\backup\\{1}.xml",
               Environment.CurrentDirectory,
               Prefs.BackupTemplate);

            string step;

            List<string> list = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            foreach (XmlElement node1 in doc.GetElementsByTagName("steps"))
            {
                foreach (XmlElement node2 in node1.ChildNodes)
                {
                    step = ParserStep(node2.InnerText, node2.GetAttribute("name"));
                    list.Add(step);
                }
            }

            return list;
        }

        public string ParserStep(string step, string type)
        {
            step = step.Replace("{(schedule}}", this.dt.ToString("yyyy-MM-dd"));

            return step;
        }

        public List<string> GetFiles()
        {
            return this.files;
        }

        public void SetFiles(string file)
        {
            this.files.Add(file);
        }
    }
}
