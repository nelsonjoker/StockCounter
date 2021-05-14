using StockCounter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockCounter
{
    public partial class PrinterSettings : Form
    {


        public PrinterSettings()
        {
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            System.Drawing.Printing.PrinterSettings.StringCollection printers = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            
            List<string> src = new List<string>(printers.Count);
            src.Add(Settings.Default.PrinterNull);
            foreach (string p in printers)
                src.Add(p);
            comboBoxPrinterName.DataSource = src;

            int selected = -1;
            if (!string.IsNullOrEmpty(Settings.Default.PrinterName))
            {
                for(int i = 0;i < src.Count; i++)
                {
                    if (src[i].Equals(Settings.Default.PrinterName))
                    {
                        selected = i;
                        break;
                    }
                }
            }
            if(selected < 0 && src.Count > 0){
                selected = 0;
                Settings.Default.PrinterName = src[0];
                Settings.Default.Save();
            }

            if(selected >= 0 && comboBoxPrinterName.Items.Count > 0)
            {
                comboBoxPrinterName.SelectedIndex = selected;
            }

            List<string> formats = new List<string>();
            formats.Add("Code39");
            formats.Add("Code128");
            comboBoxBarcodeFormat.DataSource = formats;
            if (string.IsNullOrEmpty(Settings.Default.Barcode))
                comboBoxBarcodeFormat.SelectedIndex = 0;
            else
                comboBoxBarcodeFormat.SelectedItem = Settings.Default.Barcode;


            numericUpDownWidth.Value = Settings.Default.PageWidth;
            numericUpDownHeight.Value = Settings.Default.PageHeight;


            string[] names = SerialPort.GetPortNames();
            if (names != null && names.Length > 0)
            {
                comboBoxScannerSerialPort.Items.AddRange(names);
                if (!string.IsNullOrEmpty(Settings.Default.ScannerPort))
                {
                    comboBoxScannerSerialPort.SelectedItem = Settings.Default.ScannerPort;
                }
            }
            else
                comboBoxScannerSerialPort.DataSource = null;
            

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(comboBoxPrinterName.SelectedIndex < 0)
            {
                MessageBox.Show("Nenhuma impressora selecionada...", "Impressora", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.Default.PrinterName = comboBoxPrinterName.SelectedValue.ToString();
            Settings.Default.Barcode = comboBoxBarcodeFormat.SelectedValue.ToString();
            Settings.Default.PageWidth = (int)numericUpDownWidth.Value;
            Settings.Default.PageHeight = (int)numericUpDownHeight.Value;
            Settings.Default.ScannerPort = comboBoxScannerSerialPort.SelectedItem == null ? String.Empty : comboBoxScannerSerialPort.SelectedItem.ToString();
            Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
            Close();

        }

        
    }
}
