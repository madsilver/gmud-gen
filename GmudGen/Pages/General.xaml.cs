using GmudGen.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using WinForms = System.Windows.Forms;

namespace GmudGen.Pages
{
    /// <summary>
    /// Interaction logic for General.xaml
    /// </summary>
    public partial class General : UserControl
    {
        bool unload = true;
        public General()
        {
            InitializeComponent();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(tbArea.Text != "")
            {
                tbExecutor.Text = tbArea.Text;
            }
        }

        /// <summary>
        /// Retorna o nome dos arquivos separados por vírgula
        /// </summary>
        /// <returns>string</returns>
        private string getServers()
        {
            List<string> list = new List<string>();

            foreach (ListBoxItem s in lbServer.SelectedItems)
            {
                list.Add(s.Content.ToString());
            }

            return string.Join("\n", list);
        }

        private void cbEnvironmnet_DropDownClosed(object sender, System.EventArgs e)
        {
            bool qa = cbEnvironmnet.SelectionBoxItem.ToString() == "Homologação";
            tbHomologated.Text = qa ? "Implantação no ambiente QA" : "";
            tbUserImpacted.Text = qa ? "Implantação no ambiente QA" : "";
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            unload = !unload;

            if (unload) return;

            Prefs.Info.Schedule = dpSchedule.Text;
            Prefs.Info.ScheduleTime = tbScheduleTIme.Text;
            Prefs.Info.Owner = tbOwner.Text;
            Prefs.Info.Phone = tbPhone.Text;
            Prefs.Info.Environment = cbEnvironmnet.SelectionBoxItem.ToString();
            Prefs.Info.Area = tbArea.Text;
            Prefs.Info.Executor = tbExecutor.Text;
            Prefs.Info.Directory = tbDirectory.Text;
            Prefs.Info.Servers = getServers();
            Prefs.Info.Crq = tbCrq.Text;
            Prefs.Info.BlockEnv = cbBlockEnv.IsChecked.Value ? "SIM" : "NÃO";
            Prefs.Info.UpdateCMDB = cbUpdateCMDB.IsChecked.Value ? "SIM" : "NÃO";
            Prefs.Info.ReplicationDR = cbReplicationDR.IsChecked.Value ? "SIM" : "NÃO";
            Prefs.Info.DoubleCustody = cbDoubleCustody.IsChecked.Value ? "SIM" : "NÃO";
            Prefs.Info.AccessDataCenter = cbAccessDataCenter.IsChecked.Value ? "SIM" : "NÃO";
            Prefs.Info.Homologated = cbHomologated.IsChecked.Value ? "SIM" : "NÃO";
            Prefs.Info.HomologatedDesc = tbHomologated.Text;
            Prefs.Info.UserImpacted = cbUserImpacted.IsChecked.Value ? "SIM" : "NÃO";
            Prefs.Info.UserImpactedDesc = tbUserImpacted.Text;
            Prefs.Info.CallCCT = cbCallCCT.IsChecked.Value ? "SIM" : "NÃO";
        }

        private void sdHour_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan time = TimeSpan.FromHours(sdHour.Value);
            tbScheduleTIme.Text = time.ToString("hh\\:mm");
        }
    }
}
