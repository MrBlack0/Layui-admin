using Layui_admin.BLL;
using Layui_admin.Common;
using Layui_admin.Model;
using Layui_admin.Models;
using Layui_admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace Layui_admin.Controllers
{
    public class SystemRoleController : BaseController
    {
        // GET: SystemRole
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddRole()
        {
            return View(new RoleAndRloeValues());
        }
        [HttpPost]
        public ActionResult AddRole(RoleAndRloeValues roleAndRoleVal)
        {
            var result = ResModelFactory.ResDefault();
            try
            {
                //添加Role
                SystemRole role = new SystemRole();
                role.ID = Guid.NewGuid().ToString();
                role.RoleName = roleAndRoleVal.RoleName;
                role.Description = roleAndRoleVal.Description;
                role.RoleType = 2;//超管角色只有1个，系统设定

                //添加角色权限
                List<SystemRoleValue> rolevalues = new List<SystemRoleValue>();
                foreach (var rval in roleAndRoleVal.TreeData)
                {
                    SystemRoleValue roleval = new SystemRoleValue();
                    roleval.ID = Guid.NewGuid().ToString();
                    roleval.RoleId = role.ID;
                    roleval.MenuId = rval.id;
                    roleval.Action = "View,Show,Add,Modify,Delete";
                    rolevalues.Add(roleval);
                    InitRoleVal(rolevalues, rval.children ?? new List<TreeDataViewModel>().ToArray(), role.ID);
                }

                //添加角色，角色权限
                SystemRoleService service = new SystemRoleService();
                service.AddRoleAndRoleVal(role, rolevalues);
            }
            catch (Exception ex)
            {
                result = ResModelFactory.ResError(ex.Message);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditRole(RoleAndRloeValues rolevalModel)
        {
            var result = ResModelFactory.ResDefault();
            //
            SystemRoleService service = new SystemRoleService();
            SystemRole oldRole = service.GetEntitys(p => p.ID == rolevalModel.Id).FirstOrDefault();
            oldRole.RoleName = rolevalModel.RoleName;
            oldRole.Description = rolevalModel.Description;
            //传过来的 角色权限
            List<SystemRoleValue> rolevalues = new List<SystemRoleValue>();
            if (rolevalModel.TreeData != null)
            {
                foreach (var rval in rolevalModel.TreeData)
                {
                    SystemRoleValue roleval = new SystemRoleValue();
                    roleval.ID = Guid.NewGuid().ToString();
                    roleval.RoleId = rolevalModel.Id;
                    roleval.MenuId = rval.id;
                    roleval.Action = "View,Show,Add,Modify,Delete";
                    rolevalues.Add(roleval);
                    InitRoleVal(rolevalues, rval.children ?? new List<TreeDataViewModel>().ToArray(), rolevalModel.Id);
                }
            }
            //角色是增加还是减少
            SystemRoleValueService rservice = new SystemRoleValueService();
            IQueryable<SystemRoleValue> oldroleval = rservice.GetEntitys(p => p.RoleId == rolevalModel.Id);

            List<string> oldmids = oldroleval.Select(s => s.MenuId).ToList();
            List<string> newmids = rolevalues.Select(t => t.MenuId).ToList();

            //得到增加的menuid
            List<string> add = new List<string>();
            //得到减少的menuid
            List<string> decrease = new List<string>();
            if (oldmids.Count() > 0 && newmids.Count() > 0)
            {
                decrease = oldmids.Except(newmids).ToList();
                add = newmids.Except(oldmids).ToList();
            }
            else if (oldmids.Count() > 0 && newmids.Count() <= 0)
            {
                decrease = oldmids;
            }
            else if (oldmids.Count() <= 0 && newmids.Count() > 0)
            {
                add = newmids;
            }

            //删除减少的权限
            object[] obj = new object[decrease.Count() + 1];
            string parms = string.Empty;
            string sql = string.Empty;

            if (decrease.Count > 0)
            {
                parms = SqlParameterHelper.GetParameters(decrease.ToArray(), ref obj);
                sql = $"delete from SystemRoleValue where roleid=@roleid and menuid in({parms})";
                obj[decrease.Count()] = new SqlParameter("@roleid", rolevalModel.Id);
            }
            //var add = new List<SystemRoleValue>();
            //if (add.Count > 0)
            //{
            //    add = rolevalues.Where(p => add.Contains(p.MenuId)).ToList();
            //}
            bool isok = service.EditRole(oldRole, rolevalues.Where(p => add.Contains(p.MenuId)).ToList(), sql, obj);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult List()
        {
            return View("SysRoleList");
        }
        public ActionResult GetRoleList(string rolename, int page = 1, int limit = 10)
        {
            var result = ResModelFactory.ResDefaultData<SystemRole>();
            SystemRoleService service = new SystemRoleService();
            int count = 0;
            Expression<Func<SystemRole, bool>> where = p => true;
            if (!string.IsNullOrEmpty(rolename))
            {
                where = p => p.RoleName == rolename;
            }
            var list = service.GetPages(limit, page, out count, where, o => o.RoleName, false);
            result.count = count;
            result.data = list.ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GetRoleValues(string roleId)
        {
            //var result = ResModelFactory.ResDefaultData<SystemRoleValueViewModel>();
            //角色信息
            SystemRoleService service = new SystemRoleService();
            SystemRole role = service.GetEntitys(p => p.ID == roleId).FirstOrDefault();

            //角色权限信息
            SystemRoleValueService rvalservice = new SystemRoleValueService();
            IQueryable<SystemRoleValue> rolevalues = rvalservice.GetEntitys(p => p.RoleId == roleId);

            //所有系统导航信息
            IQueryable<SystemMenu> navigations = new SystemMenuService().GetEntitys(p => true);

            RoleAndRloeValues model = new RoleAndRloeValues();
            model.Id = role.ID;
            model.RoleName = role.RoleName;
            model.Description = role.Description;

            var parentsnav = navigations.Where(p => string.IsNullOrEmpty(p.ParentID)).OrderBy(o => o.Sort);
            List<TreeDataViewModel> tree = new List<TreeDataViewModel>();
            foreach (var p in parentsnav)
            {
                //如果有菜单权限 选中并展开
                TreeDataViewModel roleval = new TreeDataViewModel();
                roleval.id = p.ID;
                roleval.title = p.MenuName;


                var parents = navigations.Where(n => n.ParentID == p.ID);
                if (parents.Count() > 0)
                {
                    roleval.spread = true;
                    roleval.children = initRoleValTree(navigations, parents, rolevalues);
                }
                else
                {
                    if (rolevalues.Where(q => q.MenuId == p.ID).Count() > 0)
                    {
                        roleval.@checked = true;
                    }
                    roleval.children = null;
                }
                tree.Add(roleval);
            }
            model.TreeData = tree;
            return View("AddRole", model);
        }


        [HttpGet]
        public ActionResult GetAllRoleValues()
        {
            var result = ResModelFactory.ResDefaultData<TreeDataViewModel>();
            //获取所有菜单
            SystemMenuService services = new SystemMenuService();
            var menus = services.GetEntitys(p => true).ToList();
            //权限树形结构
            var treedata = new List<TreeDataViewModel>();
            //
            //找出父菜单
            var parents = menus.Where(p => string.IsNullOrEmpty(p.ParentID));
            foreach (var par in parents)
            {
                var tree = new TreeDataViewModel();
                tree.title = par.MenuName;
                tree.id = par.ID;
                tree.spread = true;
                tree.@checked = true;
                if (par.IsShow.Value)
                {
                    tree.disabled = false;
                }
                else
                {
                    tree.disabled = true;
                }
                var childs = menus.Where(p => p.ParentID == p.ID);
                tree.children = GettreeData(menus, par.ID).ToArray();
                treedata.Add(tree);
            }
            result.data = treedata.ToArray();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DeleteRoles(string[] ids)
        {
            if (ids.Length <= 0)
            {
                return Json(new { code = 999, msg = "参数有误" });
            }
            SystemRoleService service = new SystemRoleService();
            object[] obj = new object[ids.Length];
            string parms = SqlParameterHelper.GetParameters(ids, ref obj);
            string sql = $"delete from SystemRole where id in({parms})";
            int n = service.ExcuteSqlParm(sql, obj);
            return Json(new { code = 0, msg = "success" });
        }


        private List<TreeDataViewModel> GettreeData(List<SystemMenu> menus, string parentId)
        {
            List<TreeDataViewModel> res = new List<TreeDataViewModel>();
            var childs = menus.Where(p => p.ParentID == parentId);
            foreach (var c in childs)
            {
                TreeDataViewModel tree = new TreeDataViewModel();
                tree.title = c.MenuName;
                tree.id = c.ID;
                tree.spread = true;
                //tree.@checked = true;
                if (c.IsShow.Value)
                {
                    tree.disabled = false;
                }
                else
                {
                    tree.disabled = true;
                }
                tree.children = GettreeData(menus, c.ID).ToArray();
                res.Add(tree);
            }
            return res;
        }

        private void InitRoleVal(List<SystemRoleValue> rolevalues, TreeDataViewModel[] childTree, string roleId)
        {
            foreach (var r in childTree)
            {
                SystemRoleValue roleval = new SystemRoleValue();
                roleval.ID = Guid.NewGuid().ToString();
                roleval.RoleId = roleId;
                roleval.MenuId = r.id;
                roleval.Action = "View,Show,Add,Modify,Delete";
                rolevalues.Add(roleval);
                InitRoleVal(rolevalues, r.children ?? new List<TreeDataViewModel>().ToArray(), roleId);
            }
            return;
        }

        private TreeDataViewModel[] initRoleValTree(IQueryable<SystemMenu> menus, IQueryable<SystemMenu> parents, IQueryable<SystemRoleValue> rolevalues)
        {
            List<TreeDataViewModel> child = new List<TreeDataViewModel>();
            var parentsnav = parents.OrderBy(o => o.Sort);
            foreach (var p in parentsnav)
            {
                TreeDataViewModel roleval = new TreeDataViewModel();
                roleval.id = p.ID;
                roleval.title = p.MenuName;

                var parents2 = menus.Where(n => n.ParentID == p.ID);
                if (parents2.Count() > 0)
                {
                    roleval.spread = true;
                    roleval.children = initRoleValTree(menus, parents2, rolevalues);
                }
                else
                {
                    if (rolevalues.Where(q => q.MenuId == p.ID).Count() > 0)
                    {
                        roleval.@checked = true;
                    }
                    roleval.children = null;
                }
                child.Add(roleval);
            }
            return child.ToArray();
        }
    }
}