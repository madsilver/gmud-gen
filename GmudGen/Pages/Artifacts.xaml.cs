using System;
using System.Collections.Generic;
using System.IO;
using Path = System.IO.Path;
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
using Forms = System.Windows.Forms;
using GmudGen.Domain;
using GmudGen.Entity;
using GmudGen.Domain.Steps;

namespace GmudGen.Pages
{
    /// <summary>
    /// Interaction logic for Artifacts.xaml
    /// </summary>
    public partial class Artifacts : UserControl
    {
        bool unload = true;
        List<Artifatc> list;

        public Artifacts()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            Forms.FolderBrowserDialog fbd = new Forms.FolderBrowserDialog();
            if (fbd.ShowDialog() == Forms.DialogResult.OK)
            {
                string filename;
                list = new List<Artifatc>();
                foreach (string file in Directory.GetFiles(fbd.SelectedPath))
                {
                    filename = Path.GetFileName(file);

                    if (filename == "pom.xml") continue;

                    list.Add(GetArtifact(filename));
                }
                dgPortlets.ItemsSource = list;
            }
        }

        /// <summary>
        /// Define as propriedades da classe ReleaseFile
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private Artifatc GetArtifact(string filename)
        {
            Artifatc artifact = new Artifatc(filename);
            string ext = Path.GetExtension(filename);

            if (ext == ".snx")
            {
                artifact.IsSnx = true;
            }
            else if (filename.ToLower().Contains("service"))
            {
                artifact.IsService = true;
            }
            else if (ext == ".war")
            {
                artifact.IsPortlet = true;
            }

            return artifact;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            ISteps 
                backupStep = null,
                portletStep = null, 
                serviceStep = null, 
                snxStep = null;

            unload = !unload;
            if (unload) return;

            Prefs.ReloadArtifacts();

            if (list == null) return;

            foreach (Artifatc artifact in list)
            {
                if (artifact.IsPortlet)
                {
                    SingleBackup(backupStep);

                    if (portletStep == null)
                    {
                        portletStep = new PortletStep();
                        Prefs.ListSteps.Add(portletStep);
                    }

                    portletStep.SetFiles(artifact.Name);
                }
                else if (artifact.IsService)
                {
                    SingleBackup(backupStep);

                    if (serviceStep == null)
                    {
                        serviceStep = new ServiceStep();
                        Prefs.ListSteps.Add(serviceStep);
                    }

                    serviceStep.SetFiles(artifact.Name);
                }
                else if (artifact.IsSnx)
                {
                    if(snxStep == null)
                    {
                        snxStep = new SnxStep();
                        //Prefs.ListSteps.Add(snxStep);
                    }

                    snxStep.SetFiles(artifact.Name);
                }
            }
        }

        private void SingleBackup(ISteps bs)
        {
            if(bs == null)
            {
                Prefs.ListSteps.Add(new BackupStep());
            }
        }
    }
}
