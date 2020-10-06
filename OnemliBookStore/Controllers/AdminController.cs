using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnemliBookStore.Business.Abstract.Books;
using OnemliBookStore.Business.Abstract.Categories;
using OnemliBookStore.Domain.Extensions;
using OnemliBookStore.Entity.Dtos;
using OnemliBookStore.Entity.Models;
using OnemliBookStore.Ui.Identity;

namespace OnemliBookStore.Ui.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IBookService _bookService;
        private ICategoryService _categoryService;

        // Rollerle ilgili işlemler
        // IdentityRole classına ekstra kolon eklenmek istenirse User extend edildiği gibi rolde eklenebilir.
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<User> _userManager;

        public AdminController(IBookService bookService, ICategoryService categoryService, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _bookService = bookService;
            _categoryService = categoryService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        #region User

        [HttpGet]
        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(s => s.Name);

                ViewBag.Roles = roles;
                return View(new UserDetailDto
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    Phone = user.PhoneNumber,
                    SelectedRoles = selectedRoles
                });
            }

            return Redirect("~/admin/user/list");
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailDto model, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.Email;
                    user.EmailConfirmed = model.EmailConfirmed;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var userRoles = await _userManager.GetRolesAsync(user);
                        selectedRoles = selectedRoles ?? new string[] { };
                        await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                        await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());

                        return Redirect("/admin/user/list");
                    }
                }
                return Redirect("/admin/user/list");
            }

            return View(model);

        }

        public IActionResult UserList()
        {
            return View(_userManager.Users);
        }
        #endregion User

        #region Role

        [HttpGet]
        public async Task<IActionResult> RoleEdit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            var members = new List<User>();
            var nonMembers = new List<User>();

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    members.Add(user);
                }
                else
                {
                    nonMembers.Add(user);
                }
            }

            var model = new RoleDetails
            {
                Role = role,
                NonMembers = nonMembers,
                Members = members
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEdit model)
        {
            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { }) // null gelirse patlamasın boş stringi dönsün
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        var result = await _userManager.AddToRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }

                foreach (var userId in model.IdsToRemove ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                        {
                            foreach (var item in result.Errors)
                            {
                                ModelState.AddModelError("", item.Description);
                            }
                        }
                    }
                }
            }

            return Redirect("/admin/role/" + model.RoleId);
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        [HttpGet]
        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleDto.Name));

                if (result.Succeeded)
                {
                    return RedirectToAction("RoleList");
                }
            }

            return View(roleDto);
        }

        #endregion Role

        #region Books

        public IActionResult BookList()
        {
            return View(new BookListDto()
            {
                Books = _bookService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult BookCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BookCreate(BookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return View(bookDto);
            }
            var entity = new Book()
            {
                Name = bookDto.Name,
                Url = bookDto.Url,
                ImageUrl = bookDto.ImageUrl,
                Description = bookDto.Description,
                Price = bookDto.Price,
            };

            if (_bookService.Create(entity))
            {
                TempData.Put("message", new AlertMessage
                {
                    Title = "Kayıt Eklendi",
                    Message = "Kaydınız eklendi",
                    AlertType = "success"
                });

                return RedirectToAction("BookList");
            }

            TempData.Put("message", new AlertMessage
            {
                Title = "Hata",
                Message = _bookService.ErrorMessage,
                AlertType = "danger"
            });

            return View(bookDto);
        }

        [HttpGet]
        public IActionResult BookEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _bookService.GetByIdWithCategories((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new BookDto()
            {
                BookId = entity.BookId,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                SelectedCategories = entity.BookCategories.Select(s => s.Category).ToList(),
                Categories = _categoryService.GetAll()
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookEdit(BookDto bookDto, int[] categoryIds, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return View(bookDto);
            }

            var entity = _bookService.GetById(bookDto.BookId);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = bookDto.Name;
            entity.Url = bookDto.Url;
            entity.Description = bookDto.Description;
            entity.Price = bookDto.Price;

            if (file != null)
            {
                entity.ImageUrl = file.FileName;
                var extention = Path.GetExtension(file.FileName); // resmin uzantısı alınır.
                var randomName = string.Format($"{DateTime.Now.Ticks}{extention}"); // yüklenilen resim eski resim ile aynı isimde ise üstüne yazmasın diye benzersiz isim verilir.
                entity.ImageUrl = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);
                // Directory.GetcurrentDirectory() Ui katmanının yolunu getirir.

                using (var stream = new FileStream(path, FileMode.Create)) // wwwroot/img klasörüne dosyayı upload eder
                {
                    await file.CopyToAsync(stream);
                }
            }

            if (_bookService.Update(entity, categoryIds))
            {
                TempData.Put("message", new AlertMessage
                {
                    Title = "Kayıt Güncellendi",
                    Message = "Kaydınız başarıyla güncellendi",
                    AlertType = "success"
                });
                return RedirectToAction("BookList");
            }

            TempData.Put("message", new AlertMessage
            {
                Title = "Hata",
                Message = _bookService.ErrorMessage,
                AlertType = "danger"
            });
            return View(bookDto);
        }

        public IActionResult BookDelete(int bookId)
        {
            var entity = _bookService.GetById(bookId);
            if (entity == null)
            {
                return NotFound();
            }

            _bookService.Delete(entity);
            var msg = new AlertMessage
            {
                Message = $"{entity.Name} isimli ürün silindi.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("BookList");
        }

        #endregion Books

        #region Categories

        public IActionResult CategoryList()
        {
            return View(new CategoryListDto()
            {
                Categories = _categoryService.GetAll()
            });
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryCreate(CategoryDto category)
        {
            var entity = new Category()
            {
                Description = category.Description,
                Name = category.Name,
                Url = category.Url,
            };

            _categoryService.Create(entity);

            var msg = new AlertMessage
            {
                Message = $"{entity.Name} category added.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public IActionResult CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var entity = _categoryService.GetByIdWithProducts((int)id);

            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryDto()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Description = entity.Description,
                Books = entity.BookCategories.Select(s => s.Book).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CategoryEdit(CategoryDto dto)
        {
            var entity = _categoryService.GetById(dto.CategoryId);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = dto.Name;
            entity.Url = dto.Url;

            _categoryService.Update(entity);

            var msg = new AlertMessage
            {
                Message = $"{entity.Name} category updated",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }

        public IActionResult CategoryDelete(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if (entity == null)
            {
                return NotFound();
            }

            _categoryService.Delete(entity);
            var msg = new AlertMessage
            {
                Message = $"{entity.Name} category deleted.",
                AlertType = "success"
            };

            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int bookId, int categoryId)
        {
            _categoryService.DeleteFromCategory(bookId, categoryId);
            return Redirect("/admin/category/" + categoryId);
        }

        #endregion Categories

        //private void CreateMessage(string message, string alertType)
        //{
        //    var msg = new AlertMessage
        //    {
        //        Message = message,
        //        AlertType = alertType
        //    };

        //    // return ettiği yerde Redirectle yönlendirme yapıldıığı için TempDatayı kullanmak şart
        //    TempData["message"] = JsonConvert.SerializeObject(msg);
        //    //MEssage ve AlertType stringlerini json formata serialize ediliyor. Kullanılacak yerde deserialize edicem.

        //}
    }
}
