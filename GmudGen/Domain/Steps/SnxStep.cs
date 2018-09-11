using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmudGen.Domain.Steps
{
    class SnxStep : ISteps
    {
        private List<string> files;

        public SnxStep()
        {
            this.files = new List<string>();
        }

        public List<string> GetSteps()
        {
            throw new NotImplementedException();
        }

        public string ParserStep(string step, string id)
        {
            throw new NotImplementedException();
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
