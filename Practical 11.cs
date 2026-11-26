using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartCampusPortal.Models
{
    public class ServiceRequest
    {
        public int RequestId { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string ServiceType { get; set; }

        [Required]
        public string Description { get; set; }

        public string Priority { get; set; }

        public string Status { get; set; }

        public DateTime RequestDate { get; set; }
    }
}
using SmartCampusPortal.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace SmartCampusPortal.DAL
{
    public class ServiceRequestDAL
    {
        private readonly string cs =
            ConfigurationManager.ConnectionStrings["CampusConnection"].ConnectionString;

        public List<ServiceRequest> GetAll()
        {
            List<ServiceRequest> list = new List<ServiceRequest>();

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM ServiceRequests";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new ServiceRequest
                    {
                        RequestId = Convert.ToInt32(dr["RequestId"]),
                        StudentName = dr["StudentName"].ToString(),
                        Department = dr["Department"].ToString(),
                        ServiceType = dr["ServiceType"].ToString(),
                        Description = dr["Description"].ToString(),
                        Priority = dr["Priority"].ToString(),
                        Status = dr["Status"].ToString(),
                        RequestDate = Convert.ToDateTime(dr["RequestDate"])
                    });
                }
            }

            return list;
        }

        public void Insert(ServiceRequest request)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    INSERT INTO ServiceRequests
                    (StudentName, Department, ServiceType,
                     Description, Priority, Status)
                    VALUES
                    (@StudentName, @Department, @ServiceType,
                     @Description, @Priority, @Status)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@StudentName", request.StudentName);
                cmd.Parameters.AddWithValue("@Department", request.Department);
                cmd.Parameters.AddWithValue("@ServiceType", request.ServiceType);
                cmd.Parameters.AddWithValue("@Description", request.Description);
                cmd.Parameters.AddWithValue("@Priority", request.Priority);
                cmd.Parameters.AddWithValue("@Status", "Pending");

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStatus(int id, string status)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    UPDATE ServiceRequests
                    SET Status = @Status
                    WHERE RequestId = @RequestId";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@RequestId", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SmartCampusPortal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
using SmartCampusPortal.DAL;
using SmartCampusPortal.Models;
using System.Web.Http;

namespace SmartCampusPortal.Controllers
{
    public class ServiceApiController : ApiController
    {
        ServiceRequestDAL dal = new ServiceRequestDAL();

        [HttpGet]
        public IHttpActionResult GetRequests()
        {
            return Ok(dal.GetAll());
        }

        [HttpPost]
        public IHttpActionResult CreateRequest(ServiceRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dal.Insert(request);

            return Ok("Service request created successfully");
        }

        [HttpPut]
        [Route("api/ServiceApi/UpdateStatus/{id:int}")]
        public IHttpActionResult UpdateStatus(int id, [FromBody] string status)
        {
            dal.UpdateStatus(id, status);

            return Ok("Status updated successfully");
        }
    }
}
using SmartCampusPortal.DAL;
using SmartCampusPortal.Models;
using System.Web.Mvc;

namespace SmartCampusPortal.Controllers
{
    public class ServiceRequestController : Controller
    {
        ServiceRequestDAL dal = new ServiceRequestDAL();

        public ActionResult Index()
        {
            return View(dal.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceRequest request)
        {
            if (ModelState.IsValid)
            {
                dal.Insert(request);
                return RedirectToAction("Index");
            }

            return View(request);
        }

        public ActionResult UpdateStatus(int id)
        {
            ViewBag.RequestId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(int id, string status)
        {
            dal.UpdateStatus(id, status);
            return RedirectToAction("Index");
        }
    }
}
@model IEnumerable<SmartCampusPortal.Models.ServiceRequest>

<h2>Smart Campus Service Requests</h2>

<p>
    @Html.ActionLink("Create New Request", "Create")
</p>

<table border="1">
    <tr>
        <th>Request ID</th>
        <th>Student Name</th>
        <th>Department</th>
        <th>Service Type</th>
        <th>Description</th>
        <th>Priority</th>
        <th>Status</th>
        <th>Date</th>
        <th>Action</th>
    </tr>

    @foreach (var item in Model)
    {
        <tr>
            <td>@item.RequestId</td>
            <td>@item.StudentName</td>
            <td>@item.Department</td>
            <td>@item.ServiceType</td>
            <td>@item.Description</td>
            <td>@item.Priority</td>
            <td>@item.Status</td>
            <td>@item.RequestDate</td>
            <td>
                @Html.ActionLink(
                    "Update Status",
                    "UpdateStatus",
                    new { id = item.RequestId })
            </td>
        </tr>
    }
</table>
@model SmartCampusPortal.Models.ServiceRequest

<h2>Create Campus Service Request</h2>

@using (Html.BeginForm())
{
    @Html.AntiForgeryToken()

    <p>Student Name</p>
    @Html.TextBoxFor(m => m.StudentName)
    @Html.ValidationMessageFor(m => m.StudentName)

    <p>Department</p>
    @Html.TextBoxFor(m => m.Department)
    @Html.ValidationMessageFor(m => m.Department)

    <p>Service Type</p>
    @Html.DropDownListFor(m => m.ServiceType,
        new SelectList(new[]
        {
            "IT Support",
            "Library",
            "Maintenance",
            "Transport"
        }))

    @Html.ValidationMessageFor(m => m.ServiceType)

    <p>Description</p>
    @Html.TextAreaFor(m => m.Description)
    @Html.ValidationMessageFor(m => m.Description)

    <p>Priority</p>
    @Html.DropDownListFor(m => m.Priority,
        new SelectList(new[]
        {
            "Low",
            "Medium",
            "High"
        }))

    <br />
    <br />

    <input type="submit" value="Submit Request" />
}

<p>
    @Html.ActionLink("Back to List", "Index")
</p>