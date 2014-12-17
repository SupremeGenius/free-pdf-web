using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer;

namespace FreePDF.AdminCP
{
    public partial class settings : System.Web.UI.Page
    {
        private GroupModel groupLogic;
        private PreferencesModel preferencesLogic;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadPreferences();                
            }
        }

        private void LoadPreferences()
        {
            preferencesLogic = new PreferencesModel();

            //Group Settings
            LoadGroup();

            //System Settings
            txtFileServer.Text = preferencesLogic.GetPreferencesByName("FileServer").Value;
            txtThumbPath.Text = preferencesLogic.GetPreferencesByName("ThumbnailPath").Value;
            txtWebmasterEmail.Text = preferencesLogic.GetPreferencesByName("WebmasterEmail").Value;

            //Module Settings
            cbSearchBoxModule.Checked = Boolean.Parse(preferencesLogic.GetPreferencesByName("SearchModule").Value);
            cbLoginBoxModule.Checked = Boolean.Parse(preferencesLogic.GetPreferencesByName("LoginModule").Value);
            cbMostViewRateBoxModule.Checked = Boolean.Parse(preferencesLogic.GetPreferencesByName("MostViewMostRateModule").Value);
            cbRandomDownloadBoxModule.Checked = Boolean.Parse(preferencesLogic.GetPreferencesByName("RandomMostDownloadModule").Value);
            cbCategoryModule.Checked = Boolean.Parse(preferencesLogic.GetPreferencesByName("CategoryModule").Value);

            txtMostViewRateBoxModuleAmount.Text = preferencesLogic.GetPreferencesByName("MostViewMostRate_Amount").Value;
            txtRandomDownloadBoxModuleAmount.Text = preferencesLogic.GetPreferencesByName("RandomMostDownload_Amount").Value;
        }

        private void LoadGroup()
        {
            groupLogic = new GroupModel();

            ddlDefaultGroup.DataSource = groupLogic.GetGroupList();
            ddlDefaultGroup.DataTextField = "Name";            
            ddlDefaultGroup.DataValueField = "GroupID";            
            ddlDefaultGroup.DataBind();

            ddlAdminGroup.DataSource = groupLogic.GetGroupList();
            ddlAdminGroup.DataTextField = "Name";
            ddlAdminGroup.DataValueField = "GroupID";            
            ddlAdminGroup.DataBind();

            int tmpGroupID;

            tmpGroupID = groupLogic.GetDefaultGroupID();

            for (int i = 0; i < ddlDefaultGroup.Items.Count; i++)
            {
                if (Convert.ToInt32(ddlDefaultGroup.Items[i].Value) == tmpGroupID)
                {
                    ddlDefaultGroup.SelectedIndex = i;
                    break;
                }
            }

            tmpGroupID = groupLogic.GetGroupList().Single(g => g.IsAdmin == true).GroupID;

            for (int i = 0; i < ddlAdminGroup.Items.Count; i++)
            {
                if (Convert.ToInt32(ddlAdminGroup.Items[i].Value) == tmpGroupID)
                {
                    ddlAdminGroup.SelectedIndex = i;
                    break;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            groupLogic = new GroupModel();
            preferencesLogic = new PreferencesModel();
                       
            int oldDefaultGroupID = groupLogic.GetDefaultGroupID();
            int oldAdminGroupID = groupLogic.GetGroupList().Single(g => g.IsAdmin == true).GroupID;

            int newDefaultGroupID = Convert.ToInt32(ddlDefaultGroup.SelectedValue);
            int newAdminGroupID = Convert.ToInt32(ddlAdminGroup.SelectedValue);

            if (oldDefaultGroupID != newDefaultGroupID)
            {
                //Update Default Group
                groupLogic.UpdateGroup(null, null, newDefaultGroupID, true, false);
                // >> Update Old Default Group
                groupLogic.UpdateGroup(null, null, oldDefaultGroupID, false, false);
            }

            if (oldAdminGroupID != newAdminGroupID)
            {
                //Update Admin Group
                groupLogic.UpdateGroup(null, null, newAdminGroupID, false, true);
                // >> Update Old Admin Group
                groupLogic.UpdateGroup(null, null, oldAdminGroupID, false, false);
            }

            //Update File Server - System Settings
            preferencesLogic.UpdatePreferences("FileServer", txtFileServer.Text);

            //Update Thumbnail Path - System Settings
            preferencesLogic.UpdatePreferences("ThumbnailPath", txtThumbPath.Text);

            //Update Webmaster Email - System Settings
            preferencesLogic.UpdatePreferences("WebmasterEmail", txtWebmasterEmail.Text);

            //Update Search Module - Module Settings
            preferencesLogic.UpdatePreferences("SearchModule", cbSearchBoxModule.Checked.ToString());
            
            //Update Login Module - Module Settings
            preferencesLogic.UpdatePreferences("LoginModule", cbLoginBoxModule.Checked.ToString());

            //Update Most View & Most Rate Module - Module Settings
            preferencesLogic.UpdatePreferences("MostViewMostRateModule", cbMostViewRateBoxModule.Checked.ToString());
            preferencesLogic.UpdatePreferences("MostViewMostRate_Amount", txtMostViewRateBoxModuleAmount.Text);

            //Update Random & Most Download Module -Module Settings
            preferencesLogic.UpdatePreferences("RandomMostDownloadModule", cbRandomDownloadBoxModule.Checked.ToString());
            preferencesLogic.UpdatePreferences("RandomMostDownload_Amount", txtRandomDownloadBoxModuleAmount.Text);

            //Update Category Module - Module Settings
            preferencesLogic.UpdatePreferences("CategoryModule", cbCategoryModule.Checked.ToString());           
        }
    }
}