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

public partial class Ngo_CashReceipt : System.Web.UI.Page
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
        string sql = "SELECT MAX(Id+1) FROM CashReceiptTable";
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
            string strQuery = "insert into CashReceiptTable(ReceiptNo, DonorName, DonationDate, PaymentMode, Amount, Notes, NGOName, NGOType) values (@ReceiptNo,@DonorName,@DonationDate,@PaymentMode,@Amount,@Notes,@NGOName,@NGOType)";
            SqlCommand cmd = new SqlCommand(strQuery);
            cmd.Parameters.Add("@ReceiptNo", SqlDbType.VarChar).Value = txtReceiptId.Text;
            cmd.Parameters.Add("@DonorName", SqlDbType.VarChar).Value = txtDonor.Text;
            cmd.Parameters.Add("@DonationDate", SqlDbType.VarChar).Value = txtDate.Text;
            cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = ddlPaymentMode.Text;
            cmd.Parameters.Add("@Amount", SqlDbType.VarChar).Value = txtAmount.Text;
            cmd.Parameters.Add("@Notes", SqlDbType.VarChar).Value = txtNotes.Text;
            cmd.Parameters.Add("@NGOName", SqlDbType.VarChar).Value = Session["Name"].ToString();
            cmd.Parameters.Add("@NGOType", SqlDbType.VarChar).Value = Session["NGOType"].ToString();
            InsertUpdateData(cmd);
            Clear();
            BindShiftRptr();
            string message = "Cash Receipt Create Successfully!!";
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
        txtNotes.Text = "";
        txtDonor.Text = "";
        txtDate.Text = "";
        txtAmount.Text = "";
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
            using (SqlCommand cmd = new SqlCommand("select * from CashReceiptTable Where NGOName='" + Session["Name"].ToString() + "' order By Id asc", con))
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