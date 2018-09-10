using GmudGen.Domain;
using GmudGen.Domain.Steps;
using GmudGen.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GmudGen.Pages
{
    /// <summary>
    /// Interaction logic for Build.xaml
    /// </summary>
    public partial class Build : UserControl
    {
        private ExcelHandler excel;

        private enum Columm
        {
            Count = 1,
            Date = 2,
            Info = 3,
            Desc = 4,
            Area = 5,
            Executor = 6
        };

        private int ROW_STEP = 22;
        private int ROW_TESTS_1 = 26;
        private int ROW_TESTS_2 = 30;
        private int ROW_ROLLBACK = 34;

        DateTime CURRENT_DATE;

        string dtFmt = "yyyy-MM-dd HH:mm";

        public Build()
        {
            InitializeComponent();

            Console("Ready");
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsRequired())
            {
                Start();
            }
        }

        private bool FieldsRequired()
        {
            List<string> fields = new List<string>();

            if(Prefs.Info.Schedule == "")
            {
                fields.Add("- Data");
            }

            if (Prefs.Info.Owner == "")
            {
                fields.Add("- Responsável");
            }

            if (Prefs.Info.Directory == "x.x.x - Release")
            {
                fields.Add("- Pasta");
            }

            if (fields.Count > 0)
            {
                string m = String.Format("Os campos abaixo são obrigatórios e precisam ser preenchidos:\n\n{0}", String.Join("\n", fields));
                MessageBox.Show(m, "GMUD Maker For Dummies");
            }

            return fields.Count < 1;
        }

        private void Start()
        {
            Console("Instantiating excel handler");
            excel = new ExcelHandler(Environment.CurrentDirectory + "\\resources\\gmud.xlsx");
            excel.SetSheet("Plan");

            Thread.Sleep(2000);

            GeneralInfo();
            
            foreach(ISteps stp in Prefs.ListSteps)
            {
                foreach (string step in stp.GetSteps())
                {
                    SetCountStep();
                    excel.Write(ROW_STEP, (int)Columm.Area, Prefs.Info.Area);
                    excel.Write(ROW_STEP, (int)Columm.Executor, Prefs.Info.Executor);
                    excel.Write(ROW_STEP, (int)Columm.Desc, step);
                    excel.InsertLine(ROW_STEP + 1);
                    SetDate();
                    UpdateCountLine();
                }
            }
        }

        private void GeneralInfo()
        {
            Console("Writing general information");
            excel.Write(6, 6, Prefs.Info.Crq);
            excel.Write(7, 3, Prefs.Info.Owner);
            excel.Write(7, 6, Prefs.Info.Phone);
            excel.Write(8, 3, Prefs.Info.BlockEnv);
            excel.Write(9, 3, Prefs.Info.UpdateCMDB);
            excel.Write(10, 3, Prefs.Info.ReplicationDR);
            excel.Write(11, 3, Prefs.Info.DoubleCustody);
            excel.Write(12, 3, Prefs.Info.AccessDataCenter);
            excel.Write(13, 3, Prefs.Info.Homologated);
            excel.Write(13, 5, Prefs.Info.HomologatedDesc);
            excel.Write(14, 3, Prefs.Info.UserImpacted);
            excel.Write(14, 5, Prefs.Info.UserImpactedDesc);
            excel.Write(15, 3, Prefs.Info.CallCCT);

            // servers
            excel.Write(19, 4, "Servidores:\n" + Prefs.Info.Servers);

            string schedule = String.Format("{0} {1}", Prefs.Info.Schedule, Prefs.Info.ScheduleTime);
            CURRENT_DATE = DateTime.Parse(schedule);

            excel.Write(19, 2, CURRENT_DATE.ToString(dtFmt));
            excel.Write(19, 3, CURRENT_DATE.ToString(dtFmt));
        }

        private void SetCountStep()
        {
            string v = excel.Read(ROW_STEP - 1, (int)Columm.Count);
            int i = 1;

            if (Int32.TryParse(v, out int val))
            {
                i = Int32.Parse(v) + 1;
            }

            excel.Write(ROW_STEP, (int)Columm.Count, i.ToString());
        }

        private void SetDate()
        {
            excel.Write(ROW_STEP, (int)Columm.Date, CURRENT_DATE.ToString(dtFmt));

            CURRENT_DATE = CURRENT_DATE.AddMinutes(5);
            excel.Write(ROW_STEP, (int)Columm.Date + 1, CURRENT_DATE.ToString(dtFmt));
        }

        private void UpdateCountLine()
        {
            ROW_STEP++;
            ROW_TESTS_1++;
            ROW_TESTS_2++;
            ROW_ROLLBACK++;
        }

        private void Console(string s)
        {
            DateTime dt = DateTime.Now;
            string m = String.Format("{0} - {1}\n", dt.ToString("yyyy-MM-dd HH:mm:ss"), s);
            tbConsole.Inlines.Add(m);
        }
    }
}
