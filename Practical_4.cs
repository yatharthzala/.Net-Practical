<%@ Page Language="C#" AutoEventWireup="true"
    CodeBehind="Registration.aspx.cs"
    Inherits="Practical_4.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Student Registration</title>

    <style>

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            padding: 40px 20px;
            font-family: "Segoe UI", Arial, sans-serif;
            background: linear-gradient(135deg, #e0f2fe, #eef2ff);
            min-height: 100vh;
        }

        /* Main Container */
        .main-container {
            max-width: 1200px;
            margin: auto;
            background: white;
            border-radius: 20px;
            box-shadow: 0 15px 45px rgba(0, 0, 0, 0.12);
            overflow: hidden;
        }

        /* Header */
        .header {
            background: linear-gradient(135deg, #2563eb, #4f46e5);
            color: white;
            text-align: center;
            padding: 35px 20px;
        }

        .header-icon {
            width: 65px;
            height: 65px;
            margin: 0 auto 15px;
            border-radius: 50%;
            background: rgba(255,255,255,0.2);
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 32px;
        }

        .header h1 {
            margin: 0;
            font-size: 32px;
            font-weight: 700;
        }

        .header p {
            margin: 8px 0 0;
            font-size: 16px;
            opacity: 0.9;
        }

        /* Content */
        .content {
            padding: 40px;
        }

        /* Two columns */
        .columns {
            display: flex;
            gap: 40px;
            align-items: flex-start;
        }

        .form-section {
            flex: 1;
        }

        .result-section {
            flex: 1;
        }

        /* Section heading */
        .section-title {
            font-size: 22px;
            font-weight: 700;
            color: #1e293b;
            margin-bottom: 25px;
            padding-bottom: 12px;
            border-bottom: 2px solid #e2e8f0;
        }

        /* Form row */
        .form-row {
            display: flex;
            align-items: center;
            margin-bottom: 22px;
        }

        .form-label {
            width: 190px;
            min-width: 190px;
            font-size: 16px;
            font-weight: 600;
            color: #334155;
        }

        .form-input {
            flex: 1;
            height: 45px;
            border: 1px solid #cbd5e1;
            border-radius: 10px;
            padding: 0 14px;
            font-size: 15px;
            outline: none;
            transition: 0.3s;
            background: #f8fafc;
        }

        .form-input:focus {
            border-color: #2563eb;
            background: white;
            box-shadow: 0 0 0 3px rgba(37, 99, 235, 0.12);
        }

        /* Gender */
        .gender-options {
            display: flex;
            gap: 25px;
            align-items: center;
        }

        .radio-item {
            font-size: 15px;
            color: #334155;
        }

        /* Hobbies */
        .hobby-options {
            display: flex;
            gap: 22px;
            align-items: center;
        }

        .checkbox-item {
            font-size: 15px;
            color: #334155;
        }

        /* Image */
        .image-box {
            text-align: center;
            margin-bottom: 25px;
        }

        .student-image {
            width: 350px;
            height: 180px;
            object-fit: cover;
            border-radius: 15px;
            border: 4px solid #e2e8f0;
            box-shadow: 0 8px 20px rgba(0,0,0,0.12);
        }

        /* Submit button */
        .submit-button {
            width: 100%;
            height: 50px;
            border: none;
            border-radius: 10px;
            background: linear-gradient(135deg, #2563eb, #4f46e5);
            color: white;
            font-size: 17px;
            font-weight: 600;
            cursor: pointer;
            transition: 0.3s;
            margin-top: 10px;
        }

        .submit-button:hover {
            transform: translateY(-2px);
            box-shadow: 0 8px 20px rgba(37, 99, 235, 0.3);
        }

        /* Result Card */
        .result-card {
            background: #f8fafc;
            border: 1px solid #e2e8f0;
            border-radius: 16px;
            padding: 25px;
        }

        .success-message {
            background: #dcfce7;
            border: 1px solid #86efac;
            color: #15803d;
            padding: 15px;
            border-radius: 10px;
            text-align: center;
            font-weight: 700;
            margin-bottom: 25px;
        }

        .result-image {
            text-align: center;
            margin-bottom: 20px;
        }

        .result-image img {
            width: 150px;
            height: 150px;
            object-fit: cover;
            border-radius: 50%;
            border: 5px solid white;
            box-shadow: 0 8px 20px rgba(0,0,0,0.15);
        }

        .result-row {
            display: flex;
            padding: 13px 0;
            border-bottom: 1px solid #e2e8f0;
        }

        .result-label {
            width: 150px;
            font-weight: 700;
            color: #475569;
        }

        .result-value {
            flex: 1;
            color: #0f172a;
            font-weight: 500;
        }

        .empty-result {
            background: #f8fafc;
            border: 2px dashed #cbd5e1;
            border-radius: 15px;
            padding: 60px 25px;
            text-align: center;
            color: #64748b;
        }

        .empty-result-icon {
            font-size: 45px;
            margin-bottom: 15px;
        }

        /* Responsive */
        @media (max-width: 850px) {

            .columns {
                flex-direction: column;
            }

            .form-section,
            .result-section {
                width: 100%;
            }

        }

        @media (max-width: 600px) {

            body {
                padding: 15px;
            }

            .content {
                padding: 25px 18px;
            }

            .form-row {
                display: block;
            }

            .form-label {
                display: block;
                width: 100%;
                margin-bottom: 8px;
            }

            .form-input {
                width: 100%;
            }

            .header h1 {
                font-size: 26px;
            }

            .result-row {
                display: block;
            }

            .result-label {
                width: 100%;
                display: block;
                margin-bottom: 5px;
            }
        }

    </style>

</head>

<body>

<form id="form1" runat="server">

    <div class="main-container">

        <!-- HEADER -->

        <div class="header">

            <div class="header-icon">
                🎓
            </div>

            <h1>Student Registration</h1>

            <p>Please enter your details below</p>

        </div>


        <div class="content">

            <div class="columns">

                <!-- ================= FORM LEFT SIDE ================= -->

                <div class="form-section">

                    <div class="section-title">
                        📝 Registration Form
                    </div>


                    <!-- FULL NAME -->

                    <div class="form-row">

                        <asp:Label
                            ID="lblFullName"
                            runat="server"
                            Text="FULL NAME :-"
                            CssClass="form-label">
                        </asp:Label>

                        <asp:TextBox
                            ID="txtFullName"
                            runat="server"
                            CssClass="form-input"
                            placeholder="Enter your full name">
                        </asp:TextBox>

                    </div>


                    <!-- ENROLLMENT -->

                    <div class="form-row">

                        <asp:Label
                            ID="lblEnrollment"
                            runat="server"
                            Text="ENROLLMENT NO :-"
                            CssClass="form-label">
                        </asp:Label>

                        <asp:TextBox
                            ID="txtEnrollment"
                            runat="server"
                            CssClass="form-input"
                            placeholder="Enter enrollment number">
                        </asp:TextBox>

                    </div>


                    <!-- GR NUMBER -->

                    <div class="form-row">

                        <asp:Label
                            ID="lblGR"
                            runat="server"
                            Text="GR NUMBER :-"
                            CssClass="form-label">
                        </asp:Label>

                        <asp:TextBox
                            ID="txtGR"
                            runat="server"
                            CssClass="form-input"
                            placeholder="Enter GR number">
                        </asp:TextBox>

                    </div>


                    <!-- GENDER -->

                    <div class="form-row">

                        <asp:Label
                            ID="lblGender"
                            runat="server"
                            Text="GENDER :-"
                            CssClass="form-label">
                        </asp:Label>

                        <div class="gender-options">

                            <asp:RadioButton
                                ID="rbMale"
                                runat="server"
                                Text=" Male"
                                GroupName="Gender"
                                CssClass="radio-item" />

                            <asp:RadioButton
                                ID="rbFemale"
                                runat="server"
                                Text=" Female"
                                GroupName="Gender"
                                CssClass="radio-item" />

                        </div>

                    </div>


                    <!-- HOBBIES -->

                    <div class="form-row">

                        <asp:Label
                            ID="lblHobbies"
                            runat="server"
                            Text="HOBBIES :-"
                            CssClass="form-label">
                        </asp:Label>

                        <div class="hobby-options">

                            <asp:CheckBox
                                ID="chkReading"
                                runat="server"
                                Text=" Reading"
                                CssClass="checkbox-item" />

                            <asp:CheckBox
                                ID="chkSports"
                                runat="server"
                                Text=" Sports"
                                CssClass="checkbox-item" />

                            <asp:CheckBox
                                ID="chkMusic"
                                runat="server"
                                Text=" Music"
                                CssClass="checkbox-item" />

                        </div>

                    </div>


                    <!-- IMAGE -->

                    <div class="form-row">

                        <asp:Label
                            ID="lblImage"
                            runat="server"
                            Text="STUDENT IMAGE :-"
                            CssClass="form-label">
                        </asp:Label>

                        <asp:Image
                            ID="imgStudent"
                            runat="server"
                            ImageUrl="~/event_image.jpg"
                            AlternateText="Student Image"
                            CssClass="student-image">
                        </asp:Image>

                    </div>


                    <!-- SUBMIT -->

                    <asp:Button
                        ID="btnSubmit"
                        runat="server"
                        Text="✓  Submit Registration"
                        CssClass="submit-button"
                        OnClick="btnSubmit_Click" />

                </div>


                <!-- ================= RESULT RIGHT SIDE ================= -->

                <div class="result-section">

                    <div class="section-title">
                        👤 Registration Details
                    </div>


                    <!-- EMPTY MESSAGE -->

                    <asp:Panel
                        ID="pnlEmpty"
                        runat="server"
                        CssClass="empty-result">

                        <div class="empty-result-icon">
                            📋
                        </div>

                        <h3>No Registration Yet</h3>

                        <p>
                            Fill the registration form and click
                            <b>Submit Registration</b>.
                        </p>

                    </asp:Panel>


                    <!-- RESULT PANEL -->

                    <asp:Panel
                        ID="pnlResult"
                        runat="server"
                        Visible="false"
                        CssClass="result-card">


                        <div class="success-message">

                            ✓ Registration Successful!

                        </div>


                        <!-- NAME -->

                        <div class="result-row">

                            <div class="result-label">
                                Full Name
                            </div>

                            <div class="result-value">

                                <asp:Label
                                    ID="lblResultName"
                                    runat="server">
                                </asp:Label>

                            </div>

                        </div>


                        <!-- ENROLLMENT -->

                        <div class="result-row">

                            <div class="result-label">
                                Enrollment No.
                            </div>

                            <div class="result-value">

                                <asp:Label
                                    ID="lblResultEnrollment"
                                    runat="server">
                                </asp:Label>

                            </div>

                        </div>


                        <!-- GR -->

                        <div class="result-row">

                            <div class="result-label">
                                GR Number
                            </div>

                            <div class="result-value">

                                <asp:Label
                                    ID="lblResultGR"
                                    runat="server">
                                </asp:Label>

                            </div>

                        </div>


                        <!-- GENDER -->

                        <div class="result-row">

                            <div class="result-label">
                                Gender
                            </div>

                            <div class="result-value">

                                <asp:Label
                                    ID="lblResultGender"
                                    runat="server">
                                </asp:Label>

                            </div>

                        </div>


                        <!-- HOBBIES -->

                        <div class="result-row">

                            <div class="result-label">
                                Hobbies
                            </div>

                            <div class="result-value">

                                <asp:Label
                                    ID="lblResultHobbies"
                                    runat="server">
                                </asp:Label>

                            </div>

                        </div>

                    </asp:Panel>

                </div>

            </div>

        </div>

    </div>

</form>

</body>

</html>