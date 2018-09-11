using GmudGen.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {
        bool unload = true;
        public Notification()
        {
            InitializeComponent();

            Prefs.Info.Contacts = "ti.gmud@grupolibra.com.br\ntisistemas.t1rio@grupolibra.com.br\nsuporte_tos";

            tbContacts.Text = Prefs.Info.Contacts;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            unload = !unload;

            if (unload) return;

            Prefs.Info.Contacts = tbContacts.Text;
        }
    }
}
