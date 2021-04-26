using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yubay_Drone_team
{
    public partial class ChangePages : System.Web.UI.UserControl
    {
        public static int PageSize { get; set; } = 10;

        public static int PageIndex { get; set; } = 1;

        public static int TotalSize { get; set; } = 10;

        public string Url { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            int currentPageIndex = Convert.ToInt32(Request.QueryString["Page"]);
            if (currentPageIndex == 0)
            {
                currentPageIndex = 1;
            }

            int pages = TotalSize / PageSize + 1;

            if (TotalSize % PageSize == 0)
            {
                pages = TotalSize / PageSize;
            }

            this.aLinkFristPage.HRef = this.BuildPagingUrl(1);
            this.aLinkLastPage.HRef = this.BuildPagingUrl(pages);
            for (int i = currentPageIndex - 3; i <= currentPageIndex + 3; i++)
            {
                //不讓頁數為負數
                if (i <= 0)
                {
                    continue;
                }

                //防止末頁按鈕多跑頁數
                if (i == pages + 1)
                {
                    break;
                }

                if (i == pages + 1 && TotalSize % PageSize == 0)
                {
                    break;
                }


                //當前頁數為黑色
                if (i == currentPageIndex)
                {
                    this.PlaceHolder1.Controls.Add(
                    new HyperLink()
                    {
                        ID = $"btn{i}",
                        Text = $"{i}",
                        NavigateUrl = $"{Url}?Page={i}",
                        CssClass = "LinkStyle",
                        ForeColor = System.Drawing.Color.Black
                    });
                    continue;
                }


                //動態增加連結
                this.PlaceHolder1.Controls.Add(
                    new HyperLink()
                    {
                        ID = $"btn{i}",
                        Text = $"{i}",
                        NavigateUrl = $"{Url}?Page={i}",
                        CssClass = "LinkStyle"
                    });


            }
        }

        string BuildPagingUrl(int pageIndex)
        {
            return $"{Url}?Page={pageIndex}";
        }
    }
}