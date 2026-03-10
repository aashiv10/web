using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUsers();
            }
        }

        private void BindUsers()
        {
            string connStr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT Id, Username, Name, Address, Contact FROM Users";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            lblStatus.ForeColor = System.Drawing.Color.Red;
            lblStatus.Text = "";

            string username = txtNewUsername.Text.Trim();
            string pass = txtNewPassword.Text;
            string retype = txtNewRetype.Text;
            string name = txtNewName.Text.Trim();
            string address = txtNewAddress.Text.Trim();
            string contact = txtNewContact.Text.Trim();

            if (pass != retype)
            {
                lblStatus.Text = "Passwords do not match.";
                return;
            }

            if (!IsValidPassword(pass))
            {
                lblStatus.Text = "Password must contain uppercase, lowercase, number and special character.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand chk = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Username=@u", con);
                chk.Parameters.AddWithValue("@u", username);

                int exists = (int)chk.ExecuteScalar();

                if (exists > 0)
                {
                    lblStatus.Text = "Username already exists.";
                    return;
                }

                SqlCommand ins = new SqlCommand(
                    "INSERT INTO Users(Username, Password, Name, Address, Contact) VALUES(@u, @p, @n, @a, @c)", con);

                ins.Parameters.AddWithValue("@u", username);
                ins.Parameters.AddWithValue("@p", pass);
                ins.Parameters.AddWithValue("@n", name);
                ins.Parameters.AddWithValue("@a", address);
                ins.Parameters.AddWithValue("@c", contact);

                ins.ExecuteNonQuery();
            }

            lblStatus.ForeColor = System.Drawing.Color.Green;
            lblStatus.Text = "User added successfully.";

            ClearNewUserForm();
            BindUsers();
        }

        private void ClearNewUserForm()
        {
            txtNewUsername.Text = "";
            txtNewPassword.Text = "";
            txtNewRetype.Text = "";
            txtNewName.Text = "";
            txtNewAddress.Text = "";
            txtNewContact.Text = "";
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUsers.EditIndex = e.NewEditIndex;
            BindUsers();
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            BindUsers();
        }

        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            string name = Convert.ToString(e.NewValues["Name"]).Trim();
            string username = Convert.ToString(e.NewValues["Username"]).Trim();
            string address = Convert.ToString(e.NewValues["Address"]).Trim();
            string contact = Convert.ToString(e.NewValues["Contact"]).Trim();

            string connStr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "UPDATE Users SET Name=@n, Username=@u, Address=@a, Contact=@c WHERE Id=@id", con);

                cmd.Parameters.AddWithValue("@n", name);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@a", address);
                cmd.Parameters.AddWithValue("@c", contact);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
            }

            gvUsers.EditIndex = -1;
            BindUsers();

            lblStatus.ForeColor = System.Drawing.Color.Green;
            lblStatus.Text = "User updated successfully.";
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvUsers.DataKeys[e.RowIndex].Value);

            string connStr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            BindUsers();

            lblStatus.ForeColor = System.Drawing.Color.Green;
            lblStatus.Text = "User deleted successfully.";
        }

        private bool IsValidPassword(string password)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(
                password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$"
            );
        }
    }
}