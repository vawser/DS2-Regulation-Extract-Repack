using Microsoft.WindowsAPICodePack.Dialogs;
using System.Configuration;
using SoulsFormats;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.Collections.Generic;
using System.Linq;
using static SoulsFormats.PARAM;
using System.Reflection;

namespace DS2_Param_Extract
{
    public partial class Main : Form
    {
        public CommonOpenFileDialog ModPath_Dialog;
        public string Path_BaseMod;

        private Progress<string> ProgressText;

        public Regulation reg;

        public Main()
        {
            InitializeComponent();

            l_status.Text = "";
            ProgressText = new Progress<string>(status => l_status.Text = status);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ModPath_Dialog = new CommonOpenFileDialog();
            ModPath_Dialog.InitialDirectory = "";
            ModPath_Dialog.IsFolderPicker = true;

            t_ModPath.Text = "";
        }

        private async void b_Extract_Click(object sender, EventArgs e)
        {
            await Task.Run(() => ExtractParams(ProgressText));
        }

        private void b_SelectMod_Click(object sender, EventArgs e)
        {
            if (ModPath_Dialog.ShowDialog() == CommonFileDialogResult.Ok && !string.IsNullOrEmpty(ModPath_Dialog.FileName))
            {
                Path_BaseMod = ModPath_Dialog.FileName;
                t_ModPath.Text = Path_BaseMod;
            }
        }

        private void ExtractParams(IProgress<string> progress)
        {
            progress.Report("Loading regulation.");

            // Return if scrambled path is empty
            if (Path_BaseMod == "" || Path_BaseMod == null)
            {
                progress.Report("Aborted.");
                MessageBox.Show("No base path specified.");
                return;
            }

            // Load regulation params
            reg = new Regulation(Path_BaseMod, c_IncludeEMEVD.Checked, c_IncludeFMG.Checked);
            
            if (reg.LoadParams() && reg.LoadLooseParams())
            {
                progress.Report("Params loaded.");
            }

            if (reg.SaveAllParamsAsLoose())
            {
                SystemSounds.Asterisk.Play();
                progress.Report("Params saved.");
            }
        }
        private async void b_Repack_Click(object sender, EventArgs e)
        {
            await Task.Run(() => RepackRegulation(ProgressText));
        }

        private void RepackRegulation(IProgress<string> progress)
        {
            progress.Report("Loading regulation.");

            // Return if scrambled path is empty
            if (Path_BaseMod == "" || Path_BaseMod == null)
            {
                progress.Report("Aborted.");
                MessageBox.Show("No base path specified.");
                return;
            }

            // Load regulation params
            reg = new Regulation(Path_BaseMod, c_IncludeEMEVD.Checked, c_IncludeFMG.Checked);

            if (reg.RepackFiles())
            {
                SystemSounds.Asterisk.Play();
                progress.Report("EMEVD/FMG files repacked.");
            }
        }

        private void l_status_Click(object sender, EventArgs e)
        {

        }
    }
}