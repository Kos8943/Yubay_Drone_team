using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yubay_Drone_team
{
    public partial class Battery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //string WantSearch = this.DropDownListSearch.SelectedValue;
            //string KeyWord = this.textKeyWord.Text;

            //DataTable dt = ConnectionDB.KeyWordSearchDroneDestination(WantSearch, KeyWord);
            //this.repInvoice.DataSource = dt;
            //this.repInvoice.DataBind();
        }

        protected void repInvoice_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }
    }
}