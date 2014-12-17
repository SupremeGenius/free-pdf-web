using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace FreePDF.UserControl
{
    public partial class RequiredTextBox : System.Web.UI.UserControl
    {
        [Category("Custom Settings")]
        [Description("Set Text")]
        public String Text
        {
            get { return input.Text; }
            set { input.Text = value; }
        }

        [Category("Custom Settings")]
        [Description("Set Error Message")]
        public String ErrorMessage
        {
            get { return validator.ErrorMessage; }
            set { validator.ErrorMessage = value; }
        }

        [Category("Custom Settings")]
        [Description("Set Text Mode")]
        public TextBoxMode TextMode
        {
            get { return input.TextMode; }
            set { input.TextMode = value; }
        }

        [Category("Custom Settings")]
        [Description("Set Validate Group")]
        public String ValidationGroup
        {
            get { return validator.ValidationGroup; }
            set { validator.ValidationGroup = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}