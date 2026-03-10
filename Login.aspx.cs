using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string retype = txtRetype.Text;

            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "";
            txtPassword.BackColor = System.Drawing.Color.White;

            if (password != retype)
            {
                lblMessage.Text = "Passwords do not match!";
                return;
            }

            if (!IsValidPassword(password))
            {
                lblMessage.Text = "Password must contain uppercase, lowercase, number and special character.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM Users WHERE Username=@u AND Password=@p";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password);

                int count = (int)cmd.ExecuteScalar();

                if (count == 1)
                {
                    txtPassword.BackColor = System.Drawing.Color.White;
                    Response.Redirect("Employee.aspx");
                }
                else
                {
                    txtPassword.BackColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Invalid login credentials";
                }
            }
        }

        protected void lnkShowReset_Click(object sender, EventArgs e)
        {
            pnlReset.Visible = true;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            string username = txtResetUsername.Text.Trim();
            string newPass = txtNewPassword.Text;
            string newRetype = txtNewRetype.Text;

            lblMessage.ForeColor = System.Drawing.Color.Red;
            lblMessage.Text = "";

            if (newPass != newRetype)
            {
                lblMessage.Text = "New passwords do not match.";
                return;
            }

            if (!IsValidPassword(newPass))
            {
                lblMessage.Text = "New password does not meet complexity requirements.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string check = "SELECT COUNT(*) FROM Users WHERE Username=@u";
                SqlCommand cmd = new SqlCommand(check, con);
                cmd.Parameters.AddWithValue("@u", username);

                int exists = (int)cmd.ExecuteScalar();

                if (exists == 0)
                {
                    lblMessage.Text = "Username not found.";
                    return;
                }

                string update = "UPDATE Users SET Password=@p WHERE Username=@u";
                SqlCommand upd = new SqlCommand(update, con);
                upd.Parameters.AddWithValue("@p", newPass);
                upd.Parameters.AddWithValue("@u", username);
                upd.ExecuteNonQuery();

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Password has been reset successfully.";
                pnlReset.Visible = false;
            }
        }

        private bool IsValidPassword(string password)
        {
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$");
        }
    }
}