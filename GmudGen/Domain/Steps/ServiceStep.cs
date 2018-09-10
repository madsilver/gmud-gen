﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GmudGen.Domain.Steps
{
    class ServiceStep : ISteps
    {
        private List<string> files;

        public ServiceStep()
        {
            this.files = new List<string>();
        }

        public List<string> GetSteps()
        {
            string file = String.Format("{0}\\resources\\templates\\service\\{1}.xml",
                Environment.CurrentDirectory,
                Prefs.ServiceTemplate);

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
            string undeploy = null, undeployed = null, cache = null, deploy = null, deployed = null, dodeploy = null;

            foreach (string name in this.files)
            {
                undeploy += name + ".undeploy\n";
                undeployed += name + ".undeployed\n";
                cache += name.Replace(".war","") + "\n";
                deploy += name + "\n";
                deployed += name + ".deployed\n";
                dodeploy += name + ".dodeploy\n";
            }

            step = step.Replace("{{undeploy}}", undeploy);
            step = step.Replace("{{undeployed}}", undeployed);
            step = step.Replace("{{cache}}", cache);
            step = step.Replace("{{war}}", deploy);
            step = step.Replace("{{dodeploy}}", dodeploy);
            step = step.Replace("{{deployed}}", deployed);

            step = step.Replace("{{directory}}", Prefs.Info.Directory);

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
