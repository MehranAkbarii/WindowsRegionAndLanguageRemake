using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace RegionAndLanguage {
    public partial class FormPrefLangs : Form {

        private string[] languages;
        public FormPrefLangs() {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e) {
            // Load languages from registry
            languages = GetLanguagesFromRegistry();

            // Display languages in the listbox
            foreach (var language in languages) {
                listBoxLanguages.Items.Add(language);
            }
        }
        private string[] GetLanguagesFromRegistry() {
            // Read Languages value from the registry
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International\User Profile");
            if (key.GetValueNames().Contains("Languages")) {
                string[] languagesValue = (string[])key.GetValue("Languages");
                return languagesValue;
            }
            return new string[] { CultureInfo.CurrentUICulture.ToString() };

        }
        private void buttonMoveUp_Click(object sender, EventArgs e) {
            MoveSelectedLanguage(-1);
        }

        private void buttonMoveDown_Click(object sender, EventArgs e) {
            MoveSelectedLanguage(1);
        }

        private void MoveSelectedLanguage(int direction) {
            int newIndex = listBoxLanguages.SelectedIndex + direction;
            if (newIndex >= 0 && newIndex < listBoxLanguages.Items.Count) {
                string selectedLanguage = listBoxLanguages.SelectedItem.ToString();
                listBoxLanguages.Items.Remove(selectedLanguage);
                listBoxLanguages.Items.Insert(newIndex, selectedLanguage);
                listBoxLanguages.SelectedIndex = newIndex;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            UpdateLanguagesRegistry();
            this.Close();
        }
        private void UpdateLanguagesRegistry() {
            // Update Languages value in the registry
            string[] newValue = listBoxLanguages.Items.Cast<string>().ToArray();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\International\User Profile", true);
            if ((key.GetValueNames().Contains("Languages")) && (newValue != GetLanguagesFromRegistry())) {
                key.SetValue("Languages", newValue);
            }
        }

        private void listBoxLanguages_SelectedIndexChanged(object sender, EventArgs e) {
            if ((listBoxLanguages.SelectedIndex > 0) && (listBoxLanguages.SelectedIndex == listBoxLanguages.Items.Count - 1)) {
                buttonMoveUp.Enabled = true;
                buttonMoveDown.Enabled = false;
            } else if (listBoxLanguages.SelectedIndex == 0) {
                buttonMoveUp.Enabled = false;
                buttonMoveDown.Enabled = true;
            } else if (listBoxLanguages.SelectedIndex > 0) {
                buttonMoveDown.Enabled = true;
                buttonMoveUp.Enabled = true;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
