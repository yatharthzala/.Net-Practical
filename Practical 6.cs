using System.Web.Mvc; using WebApplication2.Models;
namespace
WebApplication2.Controllers
{
public class ProductController : Controller {
// Product Catalog public ActionResult Index()
{ return View(); }
// Product Details
public ActionResult Details(int id)
{
if (id == 1)
{
ViewBag.ProductId =1;
ViewBag.ProductName = "Laptop";
ViewBag.Description
= "HP Laptop with 16GB RAM and
512GB SSD";
ViewBag.Price = 80000; }
else if (id == 2)
{
ViewBag.ProductId =2;
ViewBag.ProductName = "Mobile";
ViewBag.Description
= "Samsung Mobile with 8GB RAM and
128GB Storage";
ViewBag.Price = 30000;
}
else if (id == 3)
{
ViewBag.ProductId = 3;
ViewBag.ProductName = "Headphones";
ViewBag.Description
= "Wireless Headphones";
ViewBag.Price =5000;
}
return View();
}
}
}
using System; using System.Collections.Generic; using System.Linq; using System.Web;
namespace WebApplication2.Models
{ public class Products
{
public int Id { get; set; }
public string Name { get;
set; }
public string Description
{ get; set; }
}
}
@{
	ViewBag.Title	=	"Product
Details";
}
<style>
.details-box { border: 1px solid #ccc; width: 500px; padding: 25px; border-radius: 8px; box-shadow: 0 2px 5px #ccc; background-color: #f9f9f9;
}
.price { color: green; font-size: 20px; font-weight: bold;
}
.back-button { padding: 8px 12px; background-color: #555; color: white; text-decoration: none; border-radius: 5px;
}
</style>
<h2>Product Details</h2>
<div class="details-box">
<h3>@ViewBag.ProductName</h3>
<p>
	<b>Product	ID:</b>
@ViewBag.ProductId
</p>
<p>
<b>Description:</b>
@ViewBag.Description
</p>
<p class="price">
Price: ₹@ViewBag.Price
</p>
<br />
<a class="back-button"
href="@Url.Action("Index",
"Product")">
Back to Products
</a>
</div>
@model
WebApplication2.Models.Products @{
ViewBag.Title = "Products";
}
<style>
.product-box { border: 1px solid #ccc; width: 250px; padding: 20px; margin: 15px; display: inline-block; vertical-align: top; border-radius: 8px; box-shadow: 0 2px 5px #ccc; background-color: #f9f9f9; }
.product-box h3 { color: #333;
}
.price { color: green; font-weight: bold;
}
.details-button { display: inline-block; padding: 8px 12px; background-color: #007bff; color: white; text-decoration: none; border-radius: 5px;
}
.details-button:hover {
background-color:
#0056b3;
}
</style>
<h2>Product Catalog</h2>
<hr />
<div class="product-box">
<h3>Laptop</h3>
<p>
HP Laptop with 16GB RAM and
512GB SSD
</p>
<p class="price">
Price: ₹80000
</p>
<a class="details-button"
href="@Url.Action("Details",
"Product", new { id = 1 })">
View Details
</a>
</div>
<div class="product-box">
<h3>Mobile</h3>
<p>
Samsung Mobile with 8GB RAM and 128GB Storage
</p>
<p class="price">
Price: ₹30000
</p>
<a class="details-button" href="@Url.Action("Details",
"Product", new { id = 2 })"> View Details
</a>
</div>
<div class="product-box">
<h3>Headphones</h3>
<p>
	Wireless	Bluetooth
Headphones
</p>
<p class="price">
Price: ₹5000
</p>
<a class="details-button"
href="@Url.Action("Details",
"Product", new { id = 3 })">
View Details
</a>
</div>
