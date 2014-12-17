using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace FreePDF.UserControl
{
    public partial class EmailBoxValidate : System.Web.UI.UserControl
    {
        [Category("Custom Settings")]
        [Description("Set Text")]
        public String Text
        {
            get { return input.Text; }
            set { input.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}