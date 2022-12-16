using firstEntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

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

        public JsonResult AllData()
        {
            List<TblUser> list = _userContext.tblUser.Where(u=>u.BtDeleted.Equals(0)).ToList();
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