<%@ Page Title="Placement Dashboard" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Prac_9._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Placement Dashboard</h2>
    <hr />

    <h3>Placement Overview</h3>
    <table border="1" cellpadding="8" style="border-collapse: collapse; margin-bottom: 15px;">
        <tr>
            <td><strong>Total Students Placed:</strong></td>
            <td><asp:Label ID="lblTotalPlaced" runat="server" Text="0"></asp:Label></td>
        </tr>
        <tr>
            <td><strong>Highest Package (LPA):</strong></td>
            <td><asp:Label ID="lblHighestPackage" runat="server" Text="0"></asp:Label></td>
        </tr>
        <tr>
            <td><strong>Average Package (LPA):</strong></td>
            <td><asp:Label ID="lblAvgPackage" runat="server" Text="0"></asp:Label></td>
        </tr>
    </table>

    <p style="margin-bottom: 25px;">
        <asp:HyperLink ID="lnkCompanyDetails" runat="server" NavigateUrl="~/CompanyDetails.aspx"
        Font-Bold="true">
            &rarr; View Placed Students & Company Details
        </asp:HyperLink>
    </p>

    <hr />

    <h3>All Students Placement Status</h3>
    <asp:GridView ID="gvAllStudents" runat="server" AutoGenerateColumns="true" CellPadding="6"
    GridLines="Both">
    </asp:GridView>
</asp:Content>
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Prac_9
{
    public partial class _Default : Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["PlacementDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOverviewMetrics();
                LoadAllStudentsTable();
            }
        }

        private void LoadOverviewMetrics()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmdPlaced = new SqlCommand("SELECT COUNT(*) FROM Students WHERE IsPlaced = 1", con);
                lblTotalPlaced.Text = cmdPlaced.ExecuteScalar().ToString();

                SqlCommand cmdStats = new SqlCommand(
                    "SELECT ISNULL(MAX(PackageLPA), 0), ISNULL(AVG(PackageLPA), 0) FROM Placements", con);

                using (SqlDataReader reader = cmdStats.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lblHighestPackage.Text = string.Format("{0:0.00} LPA", reader[0]);
                        lblAvgPackage.Text = string.Format("{0:0.00} LPA", reader[1]);
                    }
                }
            }
        }

        private void LoadAllStudentsTable()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT
                    s.EnrollmentNo,
                    s.StudentName,
                    s.Department,
                    CASE WHEN s.IsPlaced = 1 THEN 'Placed' ELSE 'Not Placed' END AS [Status],
                    ISNULL(p.CompanyName, '-') AS [Company Name],
                    ISNULL(p.JobRole, '-') AS [Job Role],
                    ISNULL(CAST(p.PackageLPA AS VARCHAR), '-') AS [Package (LPA)]
                    FROM Students s
                    LEFT JOIN Placements p ON s.StudentId = p.StudentId
                    ORDER BY s.StudentId";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvAllStudents.DataSource = dt;
                    gvAllStudents.DataBind();
                }
            }
        }
    }
}
<%@ Page Title="Company Details" Language="C#" MasterPageFile="~/Site.Master"
AutoEventWireup="true" CodeBehind="CompanyDetails.aspx.cs" Inherits="Prac_9.CompanyDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Company Placement Statistics</h2>
    <hr />

    <p>
        <asp:HyperLink ID="lnkBack" runat="server" NavigateUrl="~/Default.aspx">&larr; Back to
        Dashboard</asp:HyperLink>
    </p>

    <h3>Hiring Summary by Company</h3>
    <asp:GridView ID="gvCompanySummary" runat="server" AutoGenerateColumns="true"
    CellPadding="6" GridLines="Both">
    </asp:GridView>

    <br />

    <h3>All Placed Students List</h3>
    <asp:GridView ID="gvPlacedStudents" runat="server" AutoGenerateColumns="true" CellPadding="6"
    GridLines="Both">
    </asp:GridView>
</asp:Content>
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Prac_9
{
    public partial class CompanyDetails : Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["PlacementDBConn"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCompanySummary();
                LoadPlacedStudents();
            }
        }

        private void LoadCompanySummary()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT
                    CompanyName,
                    COUNT(StudentId) AS [Total Hired],
                    MAX(PackageLPA) AS [Max Package (LPA)],
                    AVG(PackageLPA) AS [Avg Package (LPA)]
                    FROM Placements
                    GROUP BY CompanyName";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvCompanySummary.DataSource = dt;
                    gvCompanySummary.DataBind();
                }
            }
        }

        private void LoadPlacedStudents()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT
                    s.EnrollmentNo,
                    s.StudentName,
                    s.Department,
                    p.CompanyName,
                    p.JobRole,
                    p.PackageLPA
                    FROM Placements p
                    INNER JOIN Students s ON p.StudentId = s.StudentId
                    ORDER BY p.CompanyName, s.StudentName";

                using (SqlDataAdapter da = new SqlDataAdapter(query, con))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvPlacedStudents.DataSource = dt;
                    gvPlacedStudents.DataBind();
                }
            }
        }
    }
}
<%@ Master Language="C#" AutoEventWireup="true" CodeBehind="Site.master.cs"
Inherits="Prac_9.SiteMaster" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title><%: Page.Title %></title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
</head>
<body>
    <form runat="server">
        <asp:ScriptManager runat="server">
            <Scripts>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web"
                Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web"
                Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web"
                Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web"
                Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web"
                Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web"
                Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web"
                Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web"
                Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
            </Scripts>
        </asp:ScriptManager>

        <div class="container body-content" style="padding-top: 20px;">
            <asp:ContentPlaceHolder ID="MainContent" runat="server">
            </asp:ContentPlaceHolder>
            <hr />
            <footer>
                <p>&copy; <%: DateTime.Now.Year %> - Placement Portal</p>
            </footer>
        </div>
    </form>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/Scripts/bootstrap.js") %>
    </asp:PlaceHolder>
</body>
</html>
<configuration>
    <connectionStrings>
        <add name="PlacementDBConn"
             connectionString="Data Source=localhost\SQLEXPRESS;Initial Catalog=PlacementDB;Integrated Security=True;"
             providerName="System.Data.SqlClient" />
    </connectionStrings>
    ...
</configuration>