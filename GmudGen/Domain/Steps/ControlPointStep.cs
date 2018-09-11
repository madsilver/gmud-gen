using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GmudGen.Domain.Steps
{
    class ControlPointStep : ISteps
    {
        public ControlPointStep()
        {
            GetSteps();
        }

         public List<string> GetSteps()
        {
            string file = String.Format("{0}\\resources\\templates\\control-point\\{1}.xml",
               Environment.CurrentDirectory,
               Prefs.ControlPointTemplate);

            string step;

            List<string> list = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.Load(file);

            foreach (XmlElement node1 in doc.GetElementsByTagName("steps"))
            {
                foreach (XmlElement node2 in node1.ChildNodes)
                {
                    step = ParserStep(node2.InnerText, node2.GetAttribute("id"));
                    list.Add(step);
                }
            }

            return list;
        }

        public string ParserStep(string step, string id)
        {
            return step.Replace("{{contacts}}", Prefs.Info.Contacts);
        }

        public void SetFiles(string file)
        {
            throw new NotImplementedException();
        }

        public List<string> GetFiles()
        {
            throw new NotImplementedException();
        }
    }
}
