
using System;
using System.CodeDom;
using System.Windows.Forms;
using System.Configuration;
using System.Diagnostics;

namespace FileLinesCompare
{
    public partial class Compare : Form
    {
        private string _ignoreLinesFilePath;
        private readonly FileCompareService _fileCompareService;
        private string _checkoutLocation;
        
        public Compare()
        {
            InitializeComponent();
            _fileCompareService = new FileCompareService();
            _fileCompareService.ReportProgress += new EventHandler(ReportProgress);
        }
        
        private void Compare_Load(object sender, System.EventArgs e)
        {
            try
            {
                _ignoreLinesFilePath = ConfigurationManager.AppSettings["IgnoreLinesFile"];
                _checkoutLocation = ConfigurationManager.AppSettings["CheckoutFolder"];
                txtCheckoutFolder.Text = _checkoutLocation;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCompare_Click(object sender, System.EventArgs e)
        {
            try
            {
                _checkoutLocation = txtCheckoutFolder.Text;

                if (!_checkoutLocation.EndsWith(@"\"))
                {
                    _checkoutLocation += @"\";
                }

                NotifyStartProcessing();

                var patchFileName = _fileCompareService.GetPatchForChanges(_checkoutLocation);

                NotifyStartProcessing();
                
                string resultData =
                    _fileCompareService.CompareFiles(_ignoreLinesFilePath, _checkoutLocation, patchFileName);

                var results = new Result();
                results.PopulateResults(resultData);

                NotifyEndProcessing();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        
        private void ReportProgress(object sender, EventArgs e)
        {
            var currentFile = _fileCompareService.CurrentFile;
            this.txtProgress.AppendText(currentFile + Environment.NewLine);
        }

        private void btnMaintainIgnore_Click(object sender, System.EventArgs e)
        {
            try
            {
                Process.Start(_ignoreLinesFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void NotifyStartProcessing()
        {
            txtProgress.Clear();
            btnCompare.Enabled = false;
            btnMaintainIgnore.Enabled = false;
        }

        private void NotifyEndProcessing()
        {
            btnCompare.Enabled = true;
            btnMaintainIgnore.Enabled = true;
        }
    }
}
