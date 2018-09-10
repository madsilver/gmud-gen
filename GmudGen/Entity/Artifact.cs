using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmudGen.Entity
{
    class Artifatc
    {
        private string name;
        private bool isPortlet;
        private bool isService;
        private bool isSnx;

        public Artifatc(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public bool IsPortlet { get => isPortlet; set => isPortlet = value; }
        public bool IsService { get => isService; set => isService = value; }
        public bool IsSnx { get => isSnx; set => isSnx = value; }
    }
}
