using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Linq;


using WebApplication4.data.Services;
using WebApplication4.data.Extensions;
using WebApplication4.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using WebApplication4.data;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication4.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookServices _service;
        private List<BookModel> cart;

     
           
        
        public BookController(IBookServices services)
        {
            _service = services;
         
            
        }
        public async Task<IActionResult> Index()
        {
            var data =await _service.GetAllAsync();
            return View(data);
        }
        public async Task<IActionResult> List()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Publication,Genre,PreferredAge,Summary,Price,Photos")] BookModel book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }
            await _service.AddAsync(book);
            return RedirectToAction("Index");
             
        }
        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("Empty");
            return View(bookDetails);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("Empty");
            return View(bookDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Name,Publication,Genre,PreferredAge,Summary,Price,Photos")] BookModel book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            book.BookId = id;
            await _service.UpdateAsync(id,book);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("Empty");
            return View(bookDetails);
        }
        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails == null) return View("Empty");
            await _service.DeleteAsync(id);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Cart()
        {
            var cart = HttpContext.Session.GetObject<List<BookModel>>("Cart");
            if (cart == null) return View("Empty");
            
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            
            var cart = HttpContext.Session.GetObject<List<BookModel>>("Cart");

            if (cart == null)
            {
                cart = new List<BookModel>();
            }

            var bookDetails = await _service.GetByIdAsync(id);
            if (bookDetails != null)
            {
                cart.Add(bookDetails);
                HttpContext.Session.SetObject("Cart", cart);

            }





            int sys = cart.Count;
            HttpContext.Session.SetInt32("NoOfCartItem", sys);

            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObject<List<BookModel>>("Cart");
            var product = cart.FirstOrDefault(p => p.BookId == id);
            if (product != null)
            {
                cart.Remove(product);
                HttpContext.Session.SetObject("Cart", cart);
            }

            int sys = cart.Count;
            HttpContext.Session.SetInt32("NoOfCartItem", sys);

            return RedirectToAction("Cart");
        }
        public int GetCartItemCount()
        {
            var cart = HttpContext.Session.GetObject<List<BookModel>>("Cart");
            var count = cart.Count;
            return count;
        }
        public IActionResult GetItemCount()
        {
            var itemCount = GetCartItemCount(); // Call the method to retrieve the cart item count
            return Ok(itemCount);
        }
        public IActionResult Login()
        {
            

            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("Password");

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> LoginUser(LoginModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var password = await _service.GetByEmailAsync(user.Email);
            if (password != null)
            {
                if (user.Password == password.Password)
                {
                    HttpContext.Session.SetString("Email", user.Email);
                    HttpContext.Session.SetString("Password", user.Password);
                    
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

            return View();
        }
        
        public async Task<IActionResult> AddUser([Bind("Name,Email,Password")] UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            HttpContext.Session.SetString("Email", user.Email);
            HttpContext.Session.SetString("Password", user.Password);

            await _service.AddUserAsync(user);
            return RedirectToAction("Login");

        }
        public async Task<IActionResult> Search(Genre genre)
        {
            
            if (genre == Genre.All)
            {
                 var data = await _service.GetAllAsync();
                return View(data);

            }
            else
            {
                 var data = await _service.GetByGenreAsync(genre);
                return View(data);

            }
        }

    }
}
