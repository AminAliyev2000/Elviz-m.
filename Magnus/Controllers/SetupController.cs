using Magnus.Models;
using Magnus.Models.Regions;
using Microsoft.AspNetCore.Mvc;
using Piranha;
using Piranha.Extend.Blocks;
using System;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace StemProject.Controllers
{
    /// <summary>
    /// This controller is only used when the project is first started
    /// and no pages has been added to the database. Feel free to remove it.
    /// </summary>
    public class SetupController : Controller
    {
        private readonly IApi _api;

        public SetupController(IApi api)
        {
            _api = api;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        public string GetCulture(string code = "")
        {
            if (!String.IsNullOrEmpty(code))
            {
                CultureInfo.CurrentCulture = new CultureInfo(code);
                CultureInfo.CurrentUICulture = new CultureInfo(code);
            }
            return $"CurrentCulture:{CultureInfo.CurrentCulture.Name}, CurrentUICulture:{CultureInfo.CurrentUICulture.Name}";
        }

        //[HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        [Route("/seed")]
        public async Task<IActionResult> Seed()
        {
            // Get the default site
            var site = await _api.Sites.GetDefaultAsync();

            // Add media assets
            var bannerId = Guid.NewGuid();

            using (var stream = System.IO.File.OpenRead("seed/pexels-photo-355423.jpeg"))
            {
                await _api.Media.SaveAsync(new Piranha.Models.StreamMediaContent()
                {
                    Id = bannerId,
                    Filename = "pexels-photo-355423.jpeg",
                    Data = stream
                });
            }

            // Add the blog archived
            var blogId = Guid.NewGuid();
            var blogPage = BlogArchive.Create(_api);
            blogPage.Id = blogId;
            blogPage.SiteId = site.Id;
            blogPage.Title = "Blog Archive";
            blogPage.MetaKeywords = "Inceptos, Tristique, Pellentesque, Lorem, Vestibulum";
            blogPage.MetaDescription = "Morbi leo risus, porta ac consectetur ac, vestibulum at eros.";
            blogPage.NavigationTitle = "Blog";
            blogPage.Hero.PrimaryImage = bannerId;
            blogPage.Hero.Ingress = "Curabitur blandit tempus porttitor. Maecenas sed diam eget risus varius blandit sit amet non magna.";
            blogPage.Published = DateTime.Now;

            await _api.Pages.SaveAsync(blogPage);

            // Add a blog post
            var postId = Guid.NewGuid();
            var post = BlogPost.Create(_api);
            post.Id = postId;
            post.BlogId = blogPage.Id;
            post.Category = "Uncategorized";
            post.Tags.Add("Ornare", "Pellentesque", "Fringilla Ridiculus");
            post.Title = "Dapibus Cursus Justo";
            post.MetaKeywords = "Nullam, Mollis, Cras, Sem, Ipsum";
            post.MetaDescription = "Aenean lacinia bibendum nulla sed consectetur.";
            post.Hero.PrimaryImage = bannerId;
            post.Hero.Ingress = "Sed posuere consectetur est at lobortis. Praesent commodo cursus magna, vel scelerisque nisl consectetur et.";
            post.Blocks.Add(new HtmlBlock
            {
                Body = "<p>Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Maecenas sed diam eget risus varius blandit sit amet non magna. Integer posuere erat a ante venenatis dapibus posuere velit aliquet. Curabitur blandit tempus porttitor. Maecenas faucibus mollis interdum.</p>"
            });
            post.Published = DateTime.Now;

            await _api.Posts.SaveAsync(post);

            // Add the startpage
            var startPage = StartPage.Create(_api);
            startPage.SiteId = site.Id;
            startPage.Title = "Porta Tortor Euismod";
            startPage.MetaKeywords = "Fusce, Tristique, Nullam, Parturient, Pellentesque";
            startPage.MetaDescription = "Vestibulum id ligula porta felis euismod semper.";
            startPage.NavigationTitle = "Home";
            startPage.Blocks.Add(new HtmlBlock
            {
                Body = "<p>Nulla vitae elit libero, a pharetra augue. Curabitur blandit tempus porttitor. Nulla vitae elit libero, a pharetra augue. Donec id elit non mi porta gravida at eget metus. Curabitur blandit tempus porttitor.</p><p>Etiam porta sem malesuada magna mollis euismod. Morbi leo risus, porta ac consectetur ac, vestibulum at eros. Curabitur blandit tempus porttitor. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</p>"
            });
            startPage.Published = DateTime.Now;

            await _api.Pages.SaveAsync(startPage);

            return Redirect("~/");
        }
    }
}
