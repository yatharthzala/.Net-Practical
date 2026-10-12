using System.ComponentModel.DataAnnotations;

namespace FeedbackApp.Models
{
    public class Feedback
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
    }
}
using System.Web.Mvc;
using FeedbackApp.Models;

namespace FeedbackApp.Controllers
{
    public class FeedbackController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Feedback model)
        {
            if (ModelState.IsValid)
            {
                TempData["UserName"] = model.Name;
                TempData["UserRating"] = model.Rating;

                return RedirectToAction("Success");
            }

            return View(model);
        }

        public ActionResult Success()
        {
            if (TempData["UserName"] == null)
            {
                return RedirectToAction("Create");
            }

            return View();
        }
    }
}
@model FeedbackApp.Models.Feedback

@{
    ViewBag.Title = "Submit Feedback";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h3>We value your feedback</h3>

@using (Html.BeginForm("Create", "Feedback", FormMethod.Post))
{
    <div style="margin-bottom: 15px;">
        @Html.LabelFor(m => m.Name)<br />
        @Html.TextBoxFor(m => m.Name)
        <span style="color:red; font-size:12px;">
            @Html.ValidationMessageFor(m => m.Name)
        </span>
    </div>

    <div style="margin-bottom: 15px;">
        @Html.LabelFor(m => m.Rating) (1 to 5)<br />
        @Html.TextBoxFor(m => m.Rating, new
        {
            type = "number",
            min = "1",
            max = "5"
        })
        <span style="color:red; font-size:12px;">
            @Html.ValidationMessageFor(m => m.Rating)
        </span>
    </div>

    <div style="margin-bottom: 15px;">
        @Html.LabelFor(m => m.Description)<br />
        @Html.TextAreaFor(m => m.Description, new
        {
            rows = "4",
            cols = "30"
        })
        <span style="color:red; font-size:12px;">
            @Html.ValidationMessageFor(m => m.Description)
        </span>
    </div>

    <button type="submit">Submit</button>
}
@{
    ViewBag.Title = "Success";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h3 style="color: green;">Submission Successful!</h3>

<!-- This is where the Partial View is rendered -->
@Html.Partial("_SuccessMessage")

<br />

<!-- Option to add other feedback -->
@Html.ActionLink("Add Another Feedback", "Create", "Feedback")
<!-- Views/Shared/_Layout.cshtml -->
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>@ViewBag.Title</title>
</head>
<body style="font-family: Arial, sans-serif; padding: 20px;">

    <!-- Shared Header -->
    <header style="border-bottom: 2px solid #333; padding-bottom: 10px; margin-bottom: 20px;">
        <h2>My Feedback Portal</h2>
    </header>

    <!-- Page Content injected here -->
    <main>
        @RenderBody()
    </main>

    <!-- Shared Footer -->
    <footer style="border-top: 1px solid #ccc; padding-top: 10px; margin-top: 30px;">
        <p>&copy; @DateTime.Now.Year - Simple Feedback System</p>
    </footer>

</body>
</html>

<div style="background-color: #f9f9f9; padding: 15px; border: 1px solid #ddd; display: inline-block; margin-bottom: 20px;">
    <p>Thank you, <strong>@TempData["UserName"]</strong>!</p>
    <p>You rated us: <strong>@TempData["UserRating"] / 5</strong></p>
</div>