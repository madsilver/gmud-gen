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
        ExcelHandler excel;

        int ROW_STEP = 22;
        int ROW_TESTS_1 = 26;
        int ROW_TESTS_2 = 30;
        int ROW_ROLLBACK = 34;

        int COL_COUNT = 1;
        int COL_DATE = 2;
        int COL_DESC = 4;
        int COL_AREA = 5;
        int COL_EXECUTOR = 6;

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

                    excel.Write(ROW_STEP, COL_AREA, Prefs.Info.Area, false);
                    excel.Write(ROW_STEP, COL_EXECUTOR, Prefs.Info.Executor, false);
                    excel.Write(ROW_STEP, COL_DESC, step, true);

                    SetDate();

                    UpdateCountLine();
                }
            }
        }

        private void GeneralInfo()
        {
            Console("Writing general information");
            excel.Write(6, 6, Prefs.Info.Crq, false);
            excel.Write(7, 3, Prefs.Info.Owner, false);
            excel.Write(7, 6, Prefs.Info.Phone, false);
            excel.Write(8, 3, Prefs.Info.BlockEnv, false);
            excel.Write(9, 3, Prefs.Info.UpdateCMDB, false);
            excel.Write(10, 3, Prefs.Info.ReplicationDR, false);
            excel.Write(11, 3, Prefs.Info.DoubleCustody, false);
            excel.Write(12, 3, Prefs.Info.AccessDataCenter, false);
            excel.Write(13, 3, Prefs.Info.Homologated, false);
            excel.Write(13, 5, Prefs.Info.HomologatedDesc, false);
            excel.Write(14, 3, Prefs.Info.UserImpacted, false);
            excel.Write(14, 5, Prefs.Info.UserImpactedDesc, false);
            excel.Write(15, 3, Prefs.Info.CallCCT, false);

            // servers
            excel.Write(19, 4, "Servidores:\n" + Prefs.Info.Servers, false);

            string schedule = String.Format("{0} {1}", Prefs.Info.Schedule, Prefs.Info.ScheduleTime);
            CURRENT_DATE = DateTime.Parse(schedule);

            excel.Write(19, 2, CURRENT_DATE.ToString(dtFmt), false);
            excel.Write(19, 3, CURRENT_DATE.ToString(dtFmt), false);
        }

        private void SetCountStep()
        {
            string v = excel.Read(ROW_STEP - 1, COL_COUNT);
            int val = 0, i = 1;

            if (Int32.TryParse(v, out val))
            {
                i = Int32.Parse(v) + 1;
            }

            excel.Write(ROW_STEP, COL_COUNT, i.ToString(), false);
        }

        private void SetDate()
        {
            excel.Write(ROW_STEP, COL_DATE, CURRENT_DATE.ToString(dtFmt), false);

            CURRENT_DATE = CURRENT_DATE.AddMinutes(5);
            excel.Write(ROW_STEP, COL_DATE+1, CURRENT_DATE.ToString(dtFmt), false);
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
            string m = String.Format("[{0}] - {1}\n", dt.ToString("yyyy-MM-dd HH:mm:ss"), s);
            tbConsole.Inlines.Add(m);
        }
    }
}
