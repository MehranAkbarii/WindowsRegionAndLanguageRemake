using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace RegionAndLanguage {


    public partial class Form1 : Form {

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Wow64EnableWow64FsRedirection(ref IntPtr ptr);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int Wow64RevertWow64FsRedirection(ref IntPtr ptr);

        private string geoID;
        private string languageTag;
        private string selectedLang;
        private string selectedGeoID;
        private List<string> installedLanguages;

        private string[] regionList = new
               string[] {
                   "Antigua and Barbuda (2)",
               "Afghanistan (3)",
"Algeria (4)",
"Azerbaijan (5)",
"Albania (6)",
"Armenia (7)",
"Andorra (8)",
"Angola (9)",
"American Samoa (10)",
"Argentina (11)",
"Australia (12)",
"Austria (14)",
"Bahrain (17)",
"Barbados (18)",
"Botswana (19)",
"Bermuda (20)",
"Belgium (21)",
"Bahamas, The (22)",
"Bangladesh (23)",
"Belize (24)",
"Bosnia and Herzegovina (25)",
"Bolivia (26)",
"Myanmar (27)",
"Benin (28)",
"Belarus (29)",
"Solomon Islands (30)",
"Brazil (32)",
"Bhutan (34)",
"Bulgaria (35)",
"Brunei (37)",
"Burundi (38)",
"Canada (39)",
"Cambodia (40)",
"Chad (41)",
"Sri Lanka (42)",
"Congo (43)",
"Congo (DRC) (44)",
"China (45)",
"Chile (46)",
"Cameroon (49)",
"Comoros (50)",
"Colombia (51)",
"Costa Rica (54)",
"Central African Republic (55)",
"Cuba (56)",
"Cabo Verde (57)",
"Cyprus (59)",
"Denmark (61)",
"Djibouti (62)",
"Dominica (63)",
"Dominican Republic (65)",
"Ecuador (66)",
"Egypt (67)",
"Ireland (68)",
"Equatorial Guinea (69)",
"Estonia (70)",
"Eritrea (71)",
"El Salvador (72)",
"Ethiopia (73)",
"Czech Republic (75)",
"Finland (77)",
"Fiji (78)",
"Micronesia (80)",
"Faroe Islands (81)",
"France (84)",
"Gambia (86)",
"Gabon (87)",
"Georgia (88)",
"Ghana (89)",
"Gibraltar (90)",
"Grenada (91)",
"Greenland (93)",
"Germany (94)",
"Greece (98)",
"Guatemala (99)",
"Guinea (100)",
"Guyana (101)",
"Haiti (103)",
"Hong Kong SAR (104)",
"Honduras (106)",
"Croatia (108)",
"Hungary (109)",
"Iceland (110)",
"Indonesia (111)",
"India (113)",
"British Indian Ocean Territory (114)",
"Iran (116)",
"Israel (117)",
"Italy (118)",
"Côte d'Ivoire (119)",
"Iraq (121)",
"Japan (122)",
"Jamaica (124)",
"Jan Mayen (125)",
"Jordan (126)",
"Johnston Atoll (127)",
"Kenya (129)",
"Kyrgyzstan (130)",
"North Korea (131)",
"Kiribati (133)",
"Korea (134)",
"Kuwait (136)",
"Kazakhstan (137)",
"Laos (138)",
"Lebanon (139)",
"Latvia (140)",
"Lithuania (141)",
"Liberia (142)",
"Slovakia (143)",
"Liechtenstein (145)",
"Lesotho (146)",
"Luxembourg (147)",
"Libya (148)",
"Madagascar (149)",
"Macao SAR (151)",
"Moldova (152)",
"Mongolia (154)",
"Malawi (156)",
"Mali (157)",
"Monaco (158)",
"Morocco (159)",
"Mauritius (160)",
"Mauritania (162)",
"Malta (163)",
"Oman (164)",
"Maldives (165)",
"Mexico (166)",
"Malaysia (167)",
"Mozambique (168)",
"Niger (173)",
"Vanuatu (174)",
"Nigeria (175)",
"Netherlands (176)",
"Norway (177)",
"Nepal (178)",
"Nauru (180)",
"Suriname (181)",
"Nicaragua (182)",
"New Zealand (183)",
"Palestinian Authority (184)",
"Paraguay (185)",
"Peru (187)",
"Pakistan (190)",
"Poland (191)",
"Panama (192)",
"Portugal (193)",
"Papua New Guinea (194)",
"Palau (195)",
"Guinea-Bissau (196)",
"Qatar (197)",
"Réunion (198)",
"Marshall Islands (199)",
"Romania (200)",
"Philippines (201)",
"Puerto Rico (202)",
"Russia (203)",
"Rwanda (204)",
"Saudi Arabia (205)",
"Saint Pierre and Miquelon (206)",
"Saint Kitts and Nevis (207)",
"Seychelles (208)",
"South Africa (209)",
"Senegal (210)",
"Slovenia (212)",
"Sierra Leone (213)",
"San Marino (214)",
"Singapore (215)",
"Somalia (216)",
"Spain (217)",
"Saint Lucia (218)",
"Sudan (219)",
"Svalbard (220)",
"Sweden (221)",
"Syria (222)",
"Switzerland (223)",
"United Arab Emirates (224)",
"Trinidad and Tobago (225)",
"Thailand (227)",
"Tajikistan (228)",
"Tonga (231)",
"Togo (232)",
"São Tomé and Príncipe (233)",
"Tunisia (234)",
"Türkiye (235)",
"Tuvalu (236)",
"Taiwan (237)",
"Turkmenistan (238)",
"Tanzania (239)",
"Uganda (240)",
"Ukraine (241)",
"United Kingdom (242)",
"United States (244)",
"Burkina Faso (245)",
"Uruguay (246)",
"Uzbekistan (247)",
"Saint Vincent and the Grenadines (248)",
"Venezuela (249)",
"Vietnam (251)",
"U.S. Virgin Islands (252)",
"Vatican City (253)",
"Namibia (254)",
"Wake Island (258)",
"Samoa (259)",
"Swaziland (260)",
"Yemen (261)",
"Zambia (263)",
"Zimbabwe (264)",
"Serbia (269)",
"Montenegro (270)",
"Serbia (271)",
"Curaçao (273)",
"Anguilla (300)",
"South Sudan (276)",
"Antarctica (301)",
"Aruba (302)",
"Ascension Island (303)",
"Ashmore and Cartier Islands (304)",
"Baker Island (305)",
"Bouvet Island (306)",
"Cayman Islands (307)",
"Channel Islands (308)",
"Christmas Island (309)",
"Clipperton Island (310)",
"Cocos (Keeling) Islands (311)",
"Cook Islands (312)",
"Coral Sea Islands (313)",
"Diego Garcia (314)",
"Falkland Islands (315)",
"French Guiana (317)",
"French Polynesia (318)",
"French Southern Territories (319)",
"Guadeloupe (321)",
"Guam (322)",
"Guantanamo Bay (323)",
"Guernsey (324)",
"Heard Island and McDonald Islands (325)",
"Howland Island (326)",
"Jarvis Island (327)",
"Jersey (328)",
"Kingman Reef (329)",
"Martinique (330)",
"Mayotte (331)",
"Montserrat (332)",
"Netherlands Antilles (Former) (333)",
"New Caledonia (334)",
"Niue (335)",
"Norfolk Island (336)",
"Northern Mariana Islands (337)",
"Palmyra Atoll (338)",
"Pitcairn Islands (339)",
"Rota Island (340)",
"Saipan (341)",
"South Georgia and the South Sandwich Islands (342)",
"St Helena, Ascension and Tristan da Cunha (343)",
"Tinian Island (346)",
"Tokelau (347)",
"Tristan da Cunha (348)",
"Turks and Caicos Islands (349)",
"British Virgin Islands (351)",
"Wallis and Futuna (352)",
"Africa (742)",
"Asia (2129)",
"Europe (10541)",
"Isle of Man (15126)",
"North Macedonia (19618)",
"Melanesia (20900)",
"Micronesia (21206)",
"Midway Islands (21242)",
"Northern America (23581)",
"Polynesia (26286)",
"Central America (27082)",
"Oceania (27114)",
"Sint Maarten (30967)",
"South America (31396)",
"Saint Martin (31706)",
"World (39070)",
"Western Africa (42483)",
"Middle Africa (42484)",
"Northern Africa (42487)",
"Central Asia (47590)",
"South-Eastern Asia (47599)",
"Eastern Asia (47600)",
"Eastern Africa (47603)",
"Eastern Europe (47609)",
"Southern Europe (47610)",
"Middle East (47611)",
"Southern Asia (47614)",
"Timor-Leste (7299303)",
"Kosovo (9914689)",
"Americas (10026358)",
"Åland Islands (10028789)",
"Caribbean (10039880)",
"Northern Europe (10039882)",
"Southern Africa (10039883)",
"Western Europe (10210824)",
"Australia and New Zealand (10210825)",
"Saint Barthélemy (161832015)",
"U.S. Minor Outlying Islands (161832256)",
"Latin America and the Caribbean (161832257)",
"Bonaire, Sint Eustatius and Saba (161832258)"
,

               };


        public Form1() {

            InitializeComponent();
            this.MaximizeBox = false;
            Icon = Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            locationComboBox.Items.AddRange(regionList);
            locationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            getHomeLocation();
            selectLocation();
            getLangs();
            getCurrentDisplayLang();
            applyButton.Enabled = false;
        }

        private void changeKeyboardsButton_Click(object sender, EventArgs e) {
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "control.exe";
            p.StartInfo.Arguments = "7nput.dll";
            p.Start();
        }

        private void lpksetupButton_Click(object sender, EventArgs e) {
            IntPtr val = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref val);
            Process p = new Process();
            p.StartInfo.FileName = "lpksetup.exe";
            p.Start();
            Wow64EnableWow64FsRedirection(ref val);
        }

        private void applyButton_Click(object sender, EventArgs e) {
            checkHomeLocation();
            selectLocation();
            if (geoID != selectedGeoID) {
                changeLocation();
            }
            selectedLang = comboBoxLanguages.SelectedItem.ToString();
            if (languageTag != selectedLang) {
                changeLanguage(selectedLang);
            }
            applyButton.Enabled = false;
        }
        private void OkButton_Click(object sender, EventArgs e) {
            checkHomeLocation();
            selectLocation();
            checkCurrentDisplayLang();
            selectedLang = comboBoxLanguages.SelectedItem.ToString();
            if ((geoID == selectedGeoID) && (languageTag == selectedLang)) {
                this.Close();
            } else if ((geoID != selectedGeoID) && (languageTag == selectedLang)) {
                changeLocation();
                this.Close();
            } else if ((geoID != selectedGeoID) && (languageTag != selectedLang)) {
                changeLanguage(selectedLang);
                changeLocation();
                this.Close();
            } else if ((geoID == selectedGeoID) && (languageTag != selectedLang)) {
                changeLanguage(selectedLang);
                this.Close();
            } else
                this.Close();
        }
        private void getHomeLocation() {
            checkHomeLocation();
            for (int i = 0; i < regionList.Length; i++) {
                string str = regionList[i].Substring(regionList[i].LastIndexOf('(') + 1, regionList[i].LastIndexOf(')') - regionList[i].LastIndexOf('(') - 1);
                if (str.Equals(geoID)) {
                    locationComboBox.SelectedIndex = i;
                }
            }
        }

        private void selectLocation() {
            string selectedLocation = locationComboBox.SelectedItem.ToString();
            selectedGeoID = selectedLocation.Substring(selectedLocation.LastIndexOf('(') + 1, selectedLocation.LastIndexOf(')') - selectedLocation.LastIndexOf('(') - 1);
        }
        private void changeLocation() {
            // Execute PowerShell command to set home location
            string command = $"Set-WinHomeLocation -GeoId {selectedGeoID}";
            try {
                Process p = new Process();
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.FileName = "powershell.exe";
                p.StartInfo.Arguments = $"-WindowStyle Hidden -Command \"{command}\"";
                p.Start();
                //Process.Start("powershell.exe", $"-WindowStyle Hidden -Command \"{command}\"");
                Console.WriteLine("Home location set successfully.");
                // Optionally, show success message to user
            } catch (Exception ex) {
                Console.WriteLine($"Failed to set home location: {ex.Message}");
                // Optionally, show error message to user
            }
        }

        private void locationComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            checkHomeLocation();
            string text = locationComboBox.SelectedIndex.ToString();
            List<string> list = new List<string>();
            for (int i = 0; i < regionList.Length; i++) {
                string str = regionList[i].Substring(regionList[i].LastIndexOf('(') + 1, regionList[i].LastIndexOf(')') - regionList[i].LastIndexOf('(') - 1);
                list.Add(str);
            }
            int index = list.IndexOf(geoID);
            if (index.ToString() != text)
                applyButton.Enabled = true;
        }
        private void checkHomeLocation() {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\International\\Geo");
            geoID = (string)key.GetValue("Nation");
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void displayLangSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process p = new Process();
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "control.exe";
            p.StartInfo.Arguments = "/name Microsoft.Language";
            p.Start();
            //control /name Microsoft.Language
        }

        private void button1_Click(object sender, EventArgs e) {
        }

        private void checkCurrentDisplayLang() {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop");
            if (key.GetValueNames().Contains("PreferredUILanguagesPending")) {
                string[] arr = (string[])key.GetValue("PreferredUILanguagesPending");
                languageTag = arr[0];
            } else if (key.GetValueNames().Contains("PreferredUILanguages")) {
                string[] arr = (string[])key.GetValue("PreferredUILanguages");
                languageTag = arr[0];
            } else CultureInfo.CurrentUICulture.ToString();

        }

        private void getCurrentDisplayLang() {
            checkCurrentDisplayLang();
            if (IsAdministrator()) {
                for (int i = 0; i < installedLanguages.Count; i++) {
                    string str = installedLanguages[i].ToString();
                    if (str.Equals(languageTag)) {
                        comboBoxLanguages.SelectedIndex = i;
                    }
                }
            }
            //Computer\HKEY_CURRENT_USER\Control Panel\Desktop\PreferredUILanguagesPending
        }
        private void getLangs() {
            if (IsAdministrator()) {
                string output = ExecuteCommand("dism", "/online", "/get-intl");

                installedLanguages = new List<string>();
                string pattern = @"Installed language\(s\): (\S+)";

                MatchCollection matches = Regex.Matches(output, pattern);
                foreach (Match match in matches) {
                    installedLanguages.Add(match.Groups[1].Value);
                }
                comboBoxLanguages.Items.AddRange(installedLanguages.ToArray());
            } else {
                comboBoxLanguages.Items.Add(CultureInfo.CurrentUICulture.ToString());
                comboBoxLanguages.SelectedIndex = 0;
                comboBoxLanguages.Enabled = false;
                labelNonAdminAcc.Enabled = true;
                labelNonAdminAcc.Visible = true;
                lpksetupButton.Enabled = false;
            }
        }

        private void changeLanguage(string selectedLang) {
            // Get the selected language from the ComboBox
            try {
                string selectedLanguage = selectedLang;
                // Construct the PowerShell command to change the display language
                string powerShellCommand = $"Set-WinUILanguageOverride -Language {selectedLanguage}";
                // Run the PowerShell command
                ProcessStartInfo psi = new ProcessStartInfo("powershell.exe") {
                    Arguments = $"-Command \"{powerShellCommand}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = Process.Start(psi)) {
                    process.WaitForExit();
                }
                Form frm = new FormLogOff();
                frm.ShowDialog();
                //MessageBox.Show("You must log off for display language changes to take effect.", "Change Display Language", MessageBoxButtons.OK, MessageBoxIcon.Information);

            } catch (Exception ex) {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        static string ExecuteCommand(string command, params string[] arguments) {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = string.Join(" ", arguments);
            startInfo.RedirectStandardOutput = true;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.Verb = "runas";

            using (Process process = new Process()) {
                process.StartInfo = startInfo;
                process.Start();

                string result = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return result;
            }
        }

        private void comboBoxLanguages_SelectedIndexChanged(object sender, EventArgs e) {
            checkCurrentDisplayLang();
            int langsComboIndex = comboBoxLanguages.SelectedIndex;
            if (IsAdministrator()) {
                int index = installedLanguages.IndexOf(languageTag);
                if (index != langsComboIndex)
                    applyButton.Enabled = true;
            }
        }
        public static bool IsAdministrator() {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        //private void Form1_Load(object sender, EventArgs e) {
        //    //if (!IsAdministrator()) {
        //    //    MessageBox.Show("Please Run as Administrator","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    //    this.Close();
        //    //}
        //}

        private void buttonDownloadLangPacks_Click(object sender, EventArgs e) {
            Process.Start("https://apps.microsoft.com/search?query=local+experience+packs&hl=en-us&gl=US&price=Free");
        }

        private void buttonPrefLangs_Click(object sender, EventArgs e) {
            Form frm = new FormPrefLangs();
            frm.ShowDialog();
        }
    }
}

