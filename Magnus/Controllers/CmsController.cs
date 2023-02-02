using Magnus.Models;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using System;
using System.Threading.Tasks;
using Piranha.Services;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Piranha.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Magnus.Controllers
{
    public class CmsController : Controller
    {
        private readonly IApi _api;
        private readonly Piranha.AspNetCore.Identity.IDb _idb;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CmsController(IApi api,
            Piranha.AspNetCore.Identity.IDb idb,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _api = api;
            _idb = idb;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets the blog archive with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        /// <param name="year">The optional year</param>
        /// <param name="month">The optional month</param>
        /// <param name="page">The optional page</param>
        /// <param name="category">The optional category</param>
        /// <param name="tag">The optional tag</param>
        [Route("archive")]
        [Authorize(Roles = "SysAdmin,Blogger")]
        public async Task<IActionResult> Archive(Guid id, int? year = null, int? month = null, int? page = null,
            Guid? category = null, Guid? tag = null)
        {
            var model = await _api.Pages.GetByIdAsync<BlogArchive>(id);

            model.Archive = await _api.Archives.GetByIdAsync(id, page, category, tag, year, month);

            return View(model);
        }

        /// <summary>
        /// Gets the page with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        [Route("page")]
        public async Task<IActionResult> Page(Guid id)
        {
            var meta = await _api.Pages.GetByIdAsync(id);
            var typeId = meta.TypeId;

            if (typeId == nameof(EventPage))
            {
                var host = Request.Host.Value;
                var eventPage = await _api.Pages.GetByIdAsync<EventPage>(id);

                var parentPage = eventPage.ParentId != null ? await _api.Pages.GetByIdAsync<EventsPage>(eventPage.ParentId.Value) : null;
                if (eventPage.Details.OnlyForAuthorized && !User.Identity.IsAuthenticated)
                {
                    return Redirect("/login?ReturnUrl=" + eventPage.Permalink);
                }
                //else
                //if ( (parentPage.Behavior?.IsPrivate?.Value ?? false) && !User.Identity.IsAuthenticated)
                //{
                //    return Redirect("/manager/login?ReturnUrl=/");
                //}
            }

            return View(typeId, meta);
        }

        [Route("partners")]
        public async Task<IActionResult> Partners(Guid id)
        {
            var meta = await _api.Pages.GetByIdAsync<PartnersPage>(id);

            var typeId = meta.TypeId;

            return View(typeId, meta);
        }

        [Route("outcomes")]
        public async Task<IActionResult> Outcomes(Guid id)
        {
            var meta = await _api.Pages.GetByIdAsync<OutcomesPage>(id);

            var typeId = meta.TypeId;

            return View(typeId, meta);
        }

        [Route("events")]
        public async Task<IActionResult> Events(Guid id)
        {
            var meta = await _api.Pages.GetByIdAsync<EventsMainPage>(id);

            var typeId = meta.TypeId;

            return View(typeId, meta);
        }

        /// <summary>
        /// Gets the post with the given id.
        /// </summary>
        /// <param name="id">The unique post id</param>
        [Route("post")]
        [Authorize(Roles = "SysAdmin,Blogger")]
        public async Task<IActionResult> Post(Guid id)
        {
            var model = await _api.Posts.GetByIdAsync<BlogPost>(id);

            var typeId = model.TypeId;

            return View(typeId, model);
        }

        /// <summary>
        /// Gets the startpage with the given id.
        /// </summary>
        /// <param name="id">The unique page id</param>
        [Route("start")]
        public async Task<IActionResult> Start(Guid id)
        {
            var model = await _api.Pages.GetByIdAsync<StartPage>(id);

            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(Guid id)
        //{
        //    var _securityService = new SecurityService(this._api, this._idb, this._userManager);

        //    var email = Request.Form["Email"].ToString();
        //    var password = Request.Form["Password"].ToString();
        //    var context = HttpContext;

        //    var res = await _securityService.SignIn(context, email, password);

        //    return View(res);
        //}

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            ModelState.Remove("Body");
            var text = Request.Form["Body"].ToString();
            var blogId = Request.Query["blogId"].ToString();
            var title = Request.Form["Title"].ToString();
            var model = BlogPost.Create(_api);

            model.Author = User.Identity.Name;
            model.Slug = HttpUtility.UrlEncode(title);
            model.Body = text;
            model.Published = DateTime.Now;
            model.Title = title;
            model.TypeId = "BlogPost";
            model.RedirectType = 0;
            model.Id = Guid.NewGuid();
            model.BlogId = Guid.Parse(blogId);
            model.Category = "0";

            await _api.Posts.SaveAsync(model);

            //var typeId = model.TypeId;
            //return View(typeId, model);

            return RedirectToAction("Post", "Cms", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Edit()
        {
            ModelState.Remove("Body");
            var text = Request.Form["Body.Value"].ToString();
            var title = Request.Form["Title"].ToString();
            var stringId = Request.Query["postId"].ToString();
            var id = Guid.Parse(stringId);
            var model = await _api.Posts.GetByIdAsync<BlogPost>(id);
            model.Body.Value = text;
            model.Title = title;
            await _api.Posts.SaveAsync(model);

            //var typeId = model.TypeId;
            //return View(typeId, model);

            return RedirectToAction("Post", "Cms", new { id = id });
        }

        [HttpPost]
        public async Task<RedirectToActionResult> Comment()
        {
            var comment = new Comment
            {
                Name = Request.Query["userName"].ToString(),
                Text = Request.Form["COMMENT"].ToString(),
                Date = DateTimeOffset.Now.DateTime
            };
            var stringId = Request.Query["postId"].ToString();
            var id = Guid.Parse(stringId);
            var model = await _api.Posts.GetByIdAsync<BlogPost>(id);
            model.Comments.Add(comment);
            await _api.Posts.SaveAsync(model);

            return RedirectToAction("Post", "Cms", new { id = id });
        }

        [HttpGet]
        [Route("login")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {

            if (ModelState.IsValid)
            {

                var user = await _userManager.FindByNameAsync(username);
                if (user != null && await _userManager.CheckPasswordAsync(user,password))
                {
                    await _signInManager.SignInAsync(user, false);
                    return new RedirectResult(returnUrl);

                }

            }
            return View();
        }
    }
}
