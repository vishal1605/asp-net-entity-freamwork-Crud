using firstEntityFramework.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace firstEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        private readonly TblUserContext _userContext;
        public HomeController(TblUserContext userContext)
        {
            _userContext=userContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public RedirectResult LoginProcess(TblUser user)
        {
            TblUser u = _userContext.tblUser.FirstOrDefault(u => u.Name!.ToLower().Equals(user.Name!.ToLower()) && u.Psw!.ToLower().Equals(user.Psw!.ToLower()) && u.BtDeleted == 0)!;
            if (u == null)
                return Redirect("Login");
            return Redirect("Dashboard/"+u.Name);
        }

        public IActionResult Dashboard(string name)
        {
            Console.WriteLine(name);
            return View();
        }

        public JsonResult AllData()
        {
            List<TblUser> list = _userContext.tblUser.Include(e => e.Departments).ToList();
            //List<TblUser> list = _userContext.tblUser.FromSqlRaw<TblUser>("select * from tbluser where btdeleted = 0").ToList();
            //List<TblUser> list = _userContext.tblUser.FromSql<TblUser>($"select * from tbluser where btdeleted = 0").ToList();
            return new JsonResult(list);
        }

        public JsonResult Insert(TblUser user)
        {
            user.EntryDateTime = DateTime.Now;
            _userContext.tblUser.Add(user);
            _userContext.SaveChanges();
            return new JsonResult(user);
        }

        public JsonResult Update(TblUser user)
        {
            var result = _userContext.tblUser.SingleOrDefault(b => b.UID == user.UID);
            if (result != null)
            {
                result.Email = user.Email;
                result.Name = user.Name;
                result.Psw = user.Psw;
                result.UpdateDate = DateTime.Now;
                _userContext.SaveChanges();
            }
            return new JsonResult(result);
        }

        public JsonResult Delete(TblUser user)
        {
            var result = _userContext.tblUser.SingleOrDefault(b => b.UID == user.UID);
            if (result != null)
            {
                result.BtDeleted = user.BtDeleted;
                _userContext.SaveChanges();
            }
            return new JsonResult(result);
        }

        public JsonResult GetById(TblUser user)
        {
            var result = _userContext.tblUser.SingleOrDefault(b => b.UID == user.UID);
            return new JsonResult(result);
        }


    }
}