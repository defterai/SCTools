using System;
using System.Windows.Forms;
using NSW.StarCitizen.Tools.Helpers;
using NSW.StarCitizen.Tools.Properties;

namespace NSW.StarCitizen.Tools.Forms
{
    public partial class PromptForm : Form, ILocalizedForm
    {
        private readonly PromptType _promptType;
        private readonly Func<string, bool> _valueValidator;

        public enum PromptType
        {
            CreateProfile,
            RenameProfile
        }

        public string Value
        {
            get => tbValue.Text;
            set
            {
                tbValue.Text = value;
                UpdateAcceptButton();
            }
        }

        public int MaxValueLength
        {
            get => tbValue.MaxLength;
            set => tbValue.MaxLength = value;
        }

        public PromptForm(PromptType promptType, Func<string, bool> valueValidator)
        {
            _promptType = promptType;
            _valueValidator = valueValidator;
            InitializeComponent();
            UpdateLocalizedControls();
            UpdateAcceptButton();
        }

        public void UpdateLocalizedControls()
        {
            btnOK.Text = Resources.Promtp_OK_Button;
            switch (_promptType)
            {
                case PromptType.CreateProfile:
                    Text = Resources.GameSettings_ProfileNew_Title;
                    lblText.Text = Resources.GameSettings_ProfileNew_Text;
                    break;
                case PromptType.RenameProfile:
                    Text = Resources.GameSettings_ProfileRename_Title;
                    lblText.Text = Resources.GameSettings_ProfileRename_Text;
                    break;
            }
        }

        private void tbValue_TextChanged(object sender, EventArgs e) => UpdateAcceptButton();

        private void UpdateAcceptButton() => btnOK.Enabled = _valueValidator(tbValue.Text);
    }
}
