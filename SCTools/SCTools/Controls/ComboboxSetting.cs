using System.Drawing;
using System.Windows.Forms;
using Defter.StarCitizen.ConfigDB.Model;

namespace NSW.StarCitizen.Tools.Controls
{
    public partial class ComboboxSetting : UserControl, ISettingControl
    {
        public Control Control => this;
        public BaseSetting Model => Setting;
        public string Value
        {
            get => SelectedValue.ToString();
            set => SelectedValue = int.Parse(value);
        }
        public bool HasValue
        {
            get
            {
                if (Setting.DefaultValue.HasValue)
                {
                    return SelectedValue != Setting.DefaultValue.Value;
                }
                return cbValue.SelectedValue != null;
            }
        }
        public IntegerSetting Setting { get; }

        public override string Text
        {
            get => lblCaption.Text;
            set => lblCaption.Text = value;
        }

        public int SelectedValue
        {
            get => (int)cbValue.SelectedValue;
            set => cbValue.SelectedValue = value;
        }

        public ComboboxSetting(ToolTip toolTip, IntegerSetting setting)
        {
            Setting = setting;
            InitializeComponent();
            lblCaption.Text = setting.Name;
            cbValue.BindingContext = BindingContext;
            cbValue.DataSource = new BindingSource(setting.Values, null);
            cbValue.DisplayMember = "Value";
            cbValue.ValueMember = "Key";
            ClearValue();
            if (setting.Description != null)
            {
                toolTip.SetToolTip(lblCaption, setting.Description);
            }
        }

        public void ClearValue()
        {
            if (Setting.DefaultValue.HasValue)
            {
                cbValue.SelectedValue = Setting.DefaultValue.Value;
            }
            else
            {
                cbValue.SelectedIndex = -1;
                cbValue.SelectedItem = null;
            }
        }

        private void Combobox_SelectedIndexChanged(object sender, System.EventArgs e) =>
            BackColor = HasValue ? SystemColors.ControlDark : SystemColors.Control;

        private void ComboboxSetting_DoubleClick(object sender, System.EventArgs e) => ClearValue();
    }
}