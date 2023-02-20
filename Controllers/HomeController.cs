using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Artistesta.Models;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Microsoft.Ajax.Utilities;

namespace Artistesta.Controllers
{
    public class HomeController : Controller
    {
        ArtistestaEntities db = new ArtistestaEntities();
        public ActionResult Index()
        {
            if (Session["UserName"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var curid = Session["UserName"].ToString();
            var user = db.USER.Where(x => x.USERNAME.Equals(curid)).FirstOrDefault();
            return View(user);
        }
        
        public ActionResult Signup()
        {
            List<String> country = new List<string> {
                 "Afghanistan", "Albania", "Algeria", "Andorra", "Angola", "Anguilla", "Antigua &amp; Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia &amp; Herzegovina", "Botswana", "Brazil", "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Cape Verde", "Cayman Islands", "Chad", "Chile", "China", "Colombia", "Congo", "Cook Islands", "Costa Rica", "Cote D Ivoire", "Croatia", "Cruise Ship", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Estonia", "Ethiopia", "Falkland Islands", "Faroe Islands", "Fiji", "Finland", "France", "French Polynesia", "French West Indies", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guam", "Guatemala", "Guernsey", "Guinea", "Guinea Bissau", "Guyana", "Haiti", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Isle of Man", "Israel", "Italy", "Jamaica", "Japan", "Jersey", "Jordan", "Kazakhstan", "Kenya", "Kuwait", "Kyrgyz Republic", "Laos", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Mauritania", "Mauritius", "Mexico", "Moldova", "Monaco", "Mongolia", "Montenegro", "Montserrat", "Morocco", "Mozambique", "Namibia", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Norway", "Oman", "Pakistan", "Palestine", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russia", "Rwanda", "Saint Pierre &amp; Miquelon", "Samoa", "San Marino", "Satellite", "Saudi Arabia", "Senegal", "Serbia", "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "South Africa", "South Korea", "Spain", "Sri Lanka", "St Kitts &amp; Nevis", "St Lucia", "St Vincent", "St. Lucia", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "Timor L'Este", "Togo", "Tonga", "Trinidad &amp; Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks &amp; Caicos", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "Uruguay", "Uzbekistan", "Venezuela", "Vietnam", "Virgin Islands (US)", "Yemen", "Zambia", "Zimbabwe"
            };
            ViewBag.Country = country;
            List<String> pronouns = new List<string> { "He/His", "She/Her", "They/Their" };
            ViewBag.Pronouns = pronouns;
            return View();
        }
        [HttpPost]
        public ActionResult Signup(USER user)
        {
            if (db.USER.Any(x => x.USERNAME.Equals(user.USERNAME)))
            {
                ViewBag.Text = "Username already in user!";
                return View();
            }
            else if (db.USER.Any(x => x.EMAIL.Equals(user.EMAIL)))
            {
                ViewBag.Text = "Email already taken!";
                return View();
            }
            else if(user.PRONOUNS.Equals(null)||user.PRONOUNS.Equals("Select Gender")  )
            {
                ViewBag.Text = "Please select a Gender";
                return View();
            }
            else if (user.COUNTRY.Equals(null) || user.COUNTRY.Equals("Select Country")  )
            {
                ViewBag.Text = "Please select your country";
                return View();
            }
            else if (user.PASSWORD.Length < 6)
            {
                ViewBag.Text = "Password Must Be Six Digits Long Atleast";
                return View();
            }
            else
            {
                Random rnd = new Random();
                int otp = rnd.Next(100000, 999999);
                string curotp = otp.ToString();
                TempData["otp"] = curotp;
                Emailer em = new Emailer();
                em.To = user.EMAIL;
                em.Subject = "Verification Code";
                em.Body = $"Your Artistesta Verification Code is {curotp}";
                em.sendEmail();
                user.DP = "../Content/defaultDP.svg";
                user.COVER = "../Content/defaultCover.jpg";
                TempData["user"] = user;
                return RedirectToAction("VerifyOTP", "Home");
                //db.USER.Add(user);
                //db.SaveChanges();

                //Session["UserName"] = user.USERNAME.ToString();
                //return RedirectToAction("Index","Home");
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USER user)
        {
            var check = db.USER.Where(x => x.USERNAME.Equals(user.USERNAME) && x.PASSWORD.Equals(user.PASSWORD)).FirstOrDefault();
            if (check != null)
            {
                Session["UserName"] = user.USERNAME.ToString();
                Session["user"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Text = "Incorrect Credentials";
            }
            return View();
        }

        public ActionResult VerifyOTP()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyOTP(OTP otp)
        {
            if (otp.OTPcode.ToString() == TempData["otp"].ToString())
            {
                db.USER.Add((USER)TempData["user"]);
                db.SaveChanges();

                Session["UserName"] = ((USER)TempData["user"]).USERNAME.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.msg = "Wrong OTP!!";
                TempData["otp"] = TempData["otp"];
                return View();
            }
        }

        public ActionResult Logout()
        {
              Session["UserName"] = null;
              return RedirectToAction("Login", "Home");
        }

        public ActionResult publishArt()
        {
            return View();
        }
    }
}