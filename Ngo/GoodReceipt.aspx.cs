using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
public partial class Ngo_GoodReceipt : System.Web.UI.Page
{
    SqlCommand com;
    string str;
    SqlCommand cmd;
    SqlConnection con;
    String CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
    SqlDataReader rdr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            auto();
            BindShiftRptr();
        }
    }

    private void auto()
    {
        int Num = 0;
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        con.Open();
        string sql = "SELECT MAX(Id+1) FROM GoodReceiptTable";
        cmd = new SqlCommand(sql);
        cmd.Connection = con;
        if (Convert.IsDBNull(cmd.ExecuteScalar()))
        {
            Num = 01;
            lblReceipt.Text = Convert.ToString(Num);
            txtReceiptId.Text = Convert.ToString("B" + Num);
        }
        else
        {
            Num = (int)(cmd.ExecuteScalar());
            lblReceipt.Text = Convert.ToString(Num);
            txtReceiptId.Text = Convert.ToString("B" + Num);
        }
        cmd.Dispose();
        con.Close();
        con.Dispose();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        String CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            string strQuery = "insert into GoodReceiptTable(ReceiptNo, DonorName, DonationDate, DonationType, Quantity, Describe, NGOName, NGOType) values (@ReceiptNo,@DonorName,@DonationDate,@DonationType,@Quantity,@Describe,@NGOName,@NGOType)";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.Add("@ReceiptNo", SqlDbType.VarChar).Value = txtReceiptId.Text;
            cmd.Parameters.Add("@DonorName", SqlDbType.VarChar).Value = txtDonor.Text;
            cmd.Parameters.Add("@DonationDate", SqlDbType.VarChar).Value = txtDate.Text; 
            cmd.Parameters.Add("@DonationType", SqlDbType.VarChar).Value = ddlDonationType.Text;
            cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = txtQuantityDonation.Text;
            cmd.Parameters.Add("@Describe", SqlDbType.VarChar).Value = txtDescribe.Text;
            cmd.Parameters.Add("@NGOName", SqlDbType.VarChar).Value = Session["Name"].ToString();
            cmd.Parameters.Add("@NGOType", SqlDbType.VarChar).Value = Session["NGOType"].ToString();
            InsertUpdateData(cmd);
            Clear();
            BindShiftRptr();
            string message = "Good Receipt Create Successfully!!";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }
    }
    private void Clear()
    {
        txtDescribe.Text = "";
        txtDonor.Text = ""; 
        txtDate.Text = "";
        auto();
    }
    private Boolean InsertUpdateData(SqlCommand cmd)
    {
        String strConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(strConnString);
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            return false;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }

    private void BindShiftRptr()
    {
        String CS = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(CS))
        {
            using (SqlCommand cmd = new SqlCommand("select * from GoodReceiptTable Where NGOName='" + Session["Name"].ToString() + "' order By Id asc", con))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {

                    DataTable dtBrands = new DataTable();
                    sda.Fill(dtBrands);
                    RepeaterUser.DataSource = dtBrands;
                    RepeaterUser.DataBind();
                }

            }
        }
    }

}