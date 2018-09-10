using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmudGen.Entity
{
    class Info
    {
        private string schedule;
        private string scheduleTime;
        private string owner;
        private string phone;
        private string environment;
        private string area;
        private string executor;
        private string directory;
        private string servers;
        private string crq;
        private string blockEnv;
        private string updateCMDB;
        private string replicationDR;
        private string doubleCustody;
        private string accessDataCenter;
        private string homologated;
        private string homologatedDesc;
        private string userImpacted;
        private string userImpactedDesc;
        private string callCCT;

        public string Schedule { get => schedule; set => schedule = value; }
        public string ScheduleTime { get => scheduleTime; set => scheduleTime = value; }
        public string Owner { get => owner; set => owner = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Environment { get => environment; set => environment = value; }
        public string Area { get => area; set => area = value; }
        public string Executor { get => executor; set => executor = value; }
        public string Servers { get => servers; set => servers = value; }
        public string Crq { get => crq; set => crq = value; }
        public string BlockEnv { get => blockEnv; set => blockEnv = value; }
        public string UpdateCMDB { get => updateCMDB; set => updateCMDB = value; }
        public string ReplicationDR { get => replicationDR; set => replicationDR = value; }
        public string DoubleCustody { get => doubleCustody; set => doubleCustody = value; }
        public string AccessDataCenter { get => accessDataCenter; set => accessDataCenter = value; }
        public string Homologated { get => homologated; set => homologated = value; }
        public string UserImpacted { get => userImpacted; set => userImpacted = value; }
        public string CallCCT { get => callCCT; set => callCCT = value; }
        public string HomologatedDesc { get => homologatedDesc; set => homologatedDesc = value; }
        public string UserImpactedDesc { get => userImpactedDesc; set => userImpactedDesc = value; }
        public string Directory { get => directory; set => directory = value; }
    }
}
