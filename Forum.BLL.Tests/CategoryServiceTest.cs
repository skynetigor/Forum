using Forum.BLL.DTO;
using Forum.BLL.DTO.Content.Category;
using Forum.BLL.Infrastructure;
using Forum.BLL.Services;
using Forum.DAL.Entities.Categories;
using Forum.DAL.Entities.Identity.IntPk;
using Forum.DAL.Identity;
using Forum.DAL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.BLL.Tests
{
    [TestClass]
    public class CategoryServiceTest
    {
        //[TestMethod]
        //public void CreateCategory_CanActionWithAccessDinyedUser()
        //{
        //    var service = new CategoryService(new GenericRep<Category>(), new GenericRep<SubCategory>(),null);
        //    UserDTO user = new UserDTO
        //    {
        //        Role = "morda"
        //    };

        //    OperationDetails op = service.CreateCategory(user, new CategoryDTO());
        //    Assert.AreEqual(op.Succedeed, false);
        //    op = service.CreateSubCategory(user, new CategoryDTO() ,new SubCategoryDTO());
        //    Assert.AreEqual(op.Succedeed, false);
        //    op = service.UpdateCategory(user, new CategoryDTO());
        //    Assert.AreEqual(op.Succedeed, false);
        //    op = service.UpdateSubCategory(user, new SubCategoryDTO());
        //    Assert.AreEqual(op.Succedeed, false);
        //}

        //[TestMethod]
        //public void CreateCategory_CreateCategoryCreated()
        //{
        //    var service = new CategoryService(new GenericRep<Category>(), new GenericRep<SubCategory>());
        //    UserDTO user = new UserDTO
        //    {
        //        Role = "admin"
        //    };
        //    string name = "pidr";
        //    CategoryDTO category = new CategoryDTO { Name = name };
        //    OperationDetails op = service.CreateCategory(user, category);
        //    Assert.AreEqual(name, service.Categories.FirstOrDefault(t=>t.Name == name).Name);
        //}

        //[TestMethod]
        //public void CreateCategory_UpdateCategoryCreated()
        //{
        //    var service = new CategoryService(new GenericRep<Category>(), new GenericRep<SubCategory>());
        //    UserDTO user = new UserDTO
        //    {
        //        Role = "admin"
        //    };
        //    string name = "pidr";
        //    CategoryDTO category = new CategoryDTO { Name = name };
        //    SubCategoryDTO subcategory = new SubCategoryDTO
        //    {

        //    };
        //    OperationDetails op = service.CreateCategory(user, category);
        //    Assert.AreEqual(name, service.Categories.FirstOrDefault(t => t.Name == name).Name);
        //}
    }
}
