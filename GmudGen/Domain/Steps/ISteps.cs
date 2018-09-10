using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmudGen.Domain.Steps
{
    interface ISteps
    {
        List<string> GetFiles();
        void SetFiles(string file);
        List<string> GetSteps();
        string ParserStep(string step, string type);
    }
}
