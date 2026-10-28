<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs"
    Inherits="Inventory_Management.Default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Inventory Management</title>

    <style>
        body {
            font-family: Arial;
            margin: 30px;
            background: #f2f2f2;
        }

        .container {
            max-width: 900px;
            margin: auto;
            padding: 20px;
            background: white;
        }

        .input {
            width: 100%;
            padding: 8px;
            margin: 8px 0 15px;
            box-sizing: border-box;
        }

        .button {
            padding: 9px 15px;
            margin-right: 8px;
            cursor: pointer;
        }

        .grid {
            width: 100%;
            margin-top: 20px;
        }

        .grid th, .grid td {
            padding: 10px;
            border: 1px solid #ccc;
        }

        .message {
            display: block;
            margin-top: 12px;
        }
    </style>
</head>

<body>
<form id="form1" runat="server">

    <div class="container">

        <h1>Inventory Management System</h1>

        <asp:HiddenField ID="hfProductId" runat="server" />

        <label>Product Name</label>

        <asp:TextBox ID="txtProductName"
            runat="server"
            CssClass="input"
            MaxLength="100" />

        <label>Quantity</label>

        <asp:TextBox ID="txtQuantity"
            runat="server"
            CssClass="input"
            TextMode="Number" />

        <label>Price</label>

        <asp:TextBox ID="txtPrice"
            runat="server"
            CssClass="input" />

        <asp:Button ID="btnSave"
            runat="server"
            Text="Add Product"
            CssClass="button"
            OnClick="btnSave_Click" />

        <asp:Button ID="btnClear"
            runat="server"
            Text="Clear"
            CssClass="button"
            CausesValidation="false"
            OnClick="btnClear_Click" />

        <asp:Label ID="lblMessage"
            runat="server"
            CssClass="message" />

        <h2>Product List</h2>

        <asp:GridView ID="gvProducts"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="grid"
            OnRowCommand="gvProducts_RowCommand"
            EmptyDataText="No products available.">

            <Columns>

                <asp:BoundField
                    DataField="ProductId"
                    HeaderText="Product ID" />

                <asp:BoundField
                    DataField="ProductName"
                    HeaderText="Product Name" />

                <asp:BoundField
                    DataField="Quantity"
                    HeaderText="Quantity" />

                <asp:BoundField
                    DataField="Price"
                    HeaderText="Price"
                    DataFormatString="{0:N2}" />

                <asp:TemplateField HeaderText="Actions">

                    <ItemTemplate>

                        <asp:LinkButton
                            ID="btnEdit"
                            runat="server"
                            Text="Edit"
                            CommandName="EditProduct"
                            CommandArgument='<%# Eval("ProductId") %>'
                            CausesValidation="false" />

                        &nbsp;

                        <asp:LinkButton
                            ID="btnDelete"
                            runat="server"
                            Text="Delete"
                            CommandName="DeleteProduct"
                            CommandArgument='<%# Eval("ProductId") %>'
                            CausesValidation="false"
                            OnClientClick="return confirm('Delete this product?');" />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>

</form>
</body>
</html>
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Inventory_Management
{
    public partial class Default : Page
    {
        string cs = ConfigurationManager
            .ConnectionStrings["InventoryConnection"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProducts();
            }
        }

        // READ
        private void LoadProducts()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query =
                    "SELECT * FROM Products ORDER BY ProductId";

                using (SqlDataAdapter da =
                    new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    gvProducts.DataSource = dt;
                    gvProducts.DataBind();
                }
            }
        }

        // CREATE AND UPDATE
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text.Trim();
            int quantity;
            decimal price;

            if (name == "")
            {
                ShowMessage("Enter product name.");
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out quantity)
                || quantity < 0)
            {
                ShowMessage("Enter a valid quantity.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out price)
                || price < 0
                || price > 99999999.99m)
            {
                ShowMessage("Enter a valid price.");
                return;
            }

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string query;

                if (hfProductId.Value == "")
                {
                    query = "INSERT INTO Products " +
                        "(ProductName, Quantity, Price) " +
                        "VALUES (@Name, @Quantity, @Price)";
                }
                else
                {
                    query = "UPDATE Products SET " +
                        "ProductName=@Name, Quantity=@Quantity, " +
                        "Price=@Price WHERE ProductId=@Id";
                }

                using (SqlCommand cmd =
                    new SqlCommand(query, con))
                {
                    cmd.Parameters.Add(
                        "@Name", SqlDbType.VarChar, 100)
                        .Value = name;

                    cmd.Parameters.Add(
                        "@Quantity", SqlDbType.Int)
                        .Value = quantity;

                    SqlParameter p = cmd.Parameters.Add(
                        "@Price", SqlDbType.Decimal);

                    p.Precision = 10;
                    p.Scale = 2;
                    p.Value = price;

                    if (hfProductId.Value != "")
                    {
                        int id;

                        if (!int.TryParse(
                            hfProductId.Value, out id))
                        {
                            ShowMessage("Invalid product ID.");
                            return;
                        }

                        cmd.Parameters.Add(
                            "@Id", SqlDbType.Int).Value = id;
                    }

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        ShowMessage(
                            hfProductId.Value == ""
                            ? "Product added successfully."
                            : "Product updated successfully.");
                    }
                    else
                    {
                        ShowMessage("Product not found.");
                    }
                }
            }

            ClearForm();
            LoadProducts();
        }

        // EDIT AND DELETE
        protected void gvProducts_RowCommand(
            object sender, GridViewCommandEventArgs e)
        {
            int id;

            if (!int.TryParse(
                Convert.ToString(e.CommandArgument), out id))
            {
                ShowMessage("Invalid product ID.");
                return;
            }

            if (e.CommandName == "EditProduct")
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        "SELECT * FROM Products WHERE ProductId=@Id";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add(
                            "@Id", SqlDbType.Int).Value = id;

                        con.Open();

                        using (SqlDataReader reader =
                            cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                hfProductId.Value =
                                    reader["ProductId"].ToString();

                                txtProductName.Text =
                                    reader["ProductName"].ToString();

                                txtQuantity.Text =
                                    reader["Quantity"].ToString();

                                txtPrice.Text =
                                    reader["Price"].ToString();

                                btnSave.Text = "Update Product";

                                ShowMessage(
                                    "Edit the details and click Update Product.");
                            }
                            else
                            {
                                ShowMessage("Product not found.");
                            }
                        }
                    }
                }
            }
            else if (e.CommandName == "DeleteProduct")
            {
                using (SqlConnection con =
                    new SqlConnection(cs))
                {
                    string query =
                        "DELETE FROM Products WHERE ProductId=@Id";

                    using (SqlCommand cmd =
                        new SqlCommand(query, con))
                    {
                        cmd.Parameters.Add(
                            "@Id", SqlDbType.Int).Value = id;

                        con.Open();

                        int rows = cmd.ExecuteNonQuery();

                        ShowMessage(rows > 0
                            ? "Product deleted successfully."
                            : "Product not found.");
                    }
                }

                ClearForm();
                LoadProducts();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            lblMessage.Text = "";
            LoadProducts();
        }

        private void ClearForm()
        {
            hfProductId.Value = "";
            txtProductName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";

            btnSave.Text = "Add Product";
        }

        private void ShowMessage(string message)
        {
            lblMessage.Text = Server.HtmlEncode(message);
        }
    }
}