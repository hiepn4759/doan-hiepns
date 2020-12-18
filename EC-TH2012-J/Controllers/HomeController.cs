﻿using WebNhaHangOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace WebNhaHangOnline.Controllers
{
    public class HomeController : Controller
    {
        private static WebGiayHangHieuEntities db = new WebGiayHangHieuEntities();

        public static List<Thanhviennhom> Ds_Group;
        public ActionResult Index()
        {
            ManagerObiect.getIntance();
            var countGioHang = ManagerObiect.getIntance().giohang.getGiohang().Count;
            for (int index = 0; index < countGioHang; index++)
            {
                ManagerObiect.getIntance().giohang.removeCart(index);
            }
            ManagerObiect.getIntance().giohang.removeCart(0);
            ManagerObiect.getIntance().giohang.removeCart(1);
            ManagerObiect.getIntance().giohang.removeCart(2);
            ManagerObiect.getIntance().giohang.removeCart(3);
            ManagerObiect.getIntance().giohang.removeCart(4);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Thongtinnhom()
        {
            if (Ds_Group == null)
            {
                Ds_Group = new List<Thanhviennhom>();
                Ds_Group.Add(new Thanhviennhom
                {
                    MSSV = "B16DCCN138",
                    Hoten = "Nguyễn Sỹ Hiệp",
                    LinkFacebook = "https://www.facebook.com/hjep.nguyen.792"
                });
                Ds_Group.Add(new Thanhviennhom
                {
                    MSSV = "B16DCCN",
                    Hoten = "Nguyễn Văn Thắng",
                    LinkFacebook = "https://www.facebook.com/hjep.nguyen.792"
                });
                Ds_Group.Add(new Thanhviennhom
                {
                    MSSV = "B16DCCN",
                    Hoten = "Ngô Trường Giang",
                    LinkFacebook = "https://www.facebook.com/hjep.nguyen.792"
                });
                Ds_Group.Add(new Thanhviennhom
                {
                    MSSV = "B16DCCN",
                    Hoten = "Dương Thị Mơ",
                    LinkFacebook = "https://www.facebook.com/hjep.nguyen.792"
                });
                Ds_Group.Add(new Thanhviennhom
                {
                    MSSV = "B16DCCN",
                    Hoten = "Nguyễn Quang Linh",
                    LinkFacebook = "https://www.facebook.com/hjep.nguyen.792"
                });
            }
            return View(Ds_Group);
        }
        
        public ActionResult Cart()
        {
            return View(ManagerObiect.getIntance().giohang);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        //Đơn hàng
        public ActionResult Xemdonhang(string maKH)
        {
            List<DonhangKHModel> temp = new List<DonhangKHModel>();
            if (maKH.Length != 0)
            {
                DonhangKHModel dh = new DonhangKHModel();
                temp = dh.Xemdonhang(maKH);
            }
            return View(temp);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        public ActionResult Huydonhang(string maDH)
        {
            DonhangKHModel dh = new DonhangKHModel();
            dh.HuyDH(maDH);
            var donhang = dh.Xemdonhang(User.Identity.GetUserId());
            return View(donhang);
        }
        public ActionResult Checkout()
        {
            if (Request.IsAuthenticated)
            {
                DonhangKHModel dh = new DonhangKHModel();
                dh.nguoiMua = dh.Xemttnguoidung(User.Identity.GetUserId());
                Donhangtongquan dhtq = new Donhangtongquan()
                {
                    buyer = dh.nguoiMua.HoTen,
                    seller = dh.nguoiMua.HoTen,
                    phoneNumber = dh.nguoiMua.PhoneNumber,
                    address = dh.nguoiMua.DiaChi
                };
                return View(dhtq);
            }
            else
            {
                //DonhangKHModel dh = new DonhangKHModel();
                //dh.nguoiMua = dh.Xemttnguoidung(User.Identity.GetUserId());
                //Donhangtongquan dhtq = new Donhangtongquan()
                //{
                //    buyer = "",
                //    seller = "",
                //    phoneNumber = "",
                //    address = ""
                //};
                return View();

                //return RedirectToAction("Authentication", "Account", new { returnUrl = "/Home/Checkout" });
            }
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Checkout(Donhangtongquan dh)
        {
            if (User.Identity.IsAuthenticated)
            {
                DonhangKHModel dhmodel = new DonhangKHModel();
                dhmodel.Luudonhang(dh, User.Identity.GetUserId(), ManagerObiect.getIntance().giohang);
                var countGioHang = ManagerObiect.getIntance().giohang.getGiohang().Count;
                for (int index = 0; index < countGioHang; index++)
                {
                    ManagerObiect.getIntance().giohang.removeCart(index);
                }
                ManagerObiect.getIntance().giohang.removeCart(0);
                ManagerObiect.getIntance().giohang.removeCart(1);
                ManagerObiect.getIntance().giohang.removeCart(2);
                ManagerObiect.getIntance().giohang.removeCart(3);
                ManagerObiect.getIntance().giohang.removeCart(4);
                return RedirectToAction("MuaHangThanhCong", "Home");
            }
            else
            {
                DonhangKHModel dhmodel = new DonhangKHModel();
                dhmodel.Luudonhang_(dh, ManagerObiect.getIntance().giohang);
                var countGioHang = ManagerObiect.getIntance().giohang.getGiohang().Count;
                for (int index = 0; index < countGioHang; index++)
                {
                    ManagerObiect.getIntance().giohang.removeCart(index);
                }
                ManagerObiect.getIntance().giohang.removeCart(0);
                ManagerObiect.getIntance().giohang.removeCart(1);
                ManagerObiect.getIntance().giohang.removeCart(2);
                ManagerObiect.getIntance().giohang.removeCart(3);
                ManagerObiect.getIntance().giohang.removeCart(4);
                return RedirectToAction("MuaHangThanhCong_", "Home");
                //return RedirectToAction("Checkout", "Home");
            }
        }

        public ActionResult MuaHangThanhCong_()
        {      
            return View();
        }

        public ActionResult MuaHangThanhCong()
        {
            var userName = User.Identity.Name;        
            using (WebGiayHangHieuEntities db = new WebGiayHangHieuEntities())
            {
                var listUsers_ = db.AspNetUsers.Where(x => x.UserName == userName).ToList();
                var IdUser_ = listUsers_[0].Id;

                var PointUser = listUsers_[0].AccessFailedCount;
                ViewBag.PointUser = PointUser;
            }
            return View();
        }




        public ActionResult MainMenu()
        {
            MainMenuModel mnmodel = new MainMenuModel();
            var menulist = mnmodel.GetMenuList();
            return PartialView("_MainMenuPartial", menulist);
        }

        public ActionResult SPNoiBat(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPHot();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPMoiNhap(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPMoiNhap();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPKhuyenMai(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPKhuyenMai();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPBanChay()
        {
            SanPhamModel sp = new SanPhamModel();
            IQueryable<SanPham> splist = sp.SPBanChay(7);      
            if (splist.Any())
                return PartialView("_BestSellerPartial", splist.ToList());
            else
                return null;
        }
        public ActionResult SPMoiXem()
        {
            return PartialView("_RecentlyViewPartial", ManagerObiect.getIntance().Laydanhsachsanphammoixem());
        }

    }
}