using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Product;
using WebStore.Domain.Models;
using WebStore.Domain.ViewModels.Cart;
using WebStore.Interfaces.Services;
using WebStore.Services.Map;
using WebStore.Services.Map.DTO;

namespace WebStore.Services
{
    public class CookieCartService : ICartService
    {
        private readonly IProductData _productData;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _cartName;

        private Cart Cart
        {
            get
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var cookie = httpContext.Request.Cookies[_cartName];

                Cart cart = null;

                if (cookie is null)
                {
                    cart = new Cart();
                    httpContext.Response.Cookies.Append(
                            _cartName, 
                            JsonConvert.SerializeObject(cart));
                }
                else
                {
                    cart = JsonConvert.DeserializeObject<Cart>(cookie);
                    httpContext.Response.Cookies.Delete(_cartName);
                    httpContext.Response.Cookies.Append(_cartName, cookie, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(1)
                    });
                }

                return cart;
            }

            set
            {
                var httpContext = _httpContextAccessor.HttpContext;

                var json = JsonConvert.SerializeObject(value);

                httpContext.Response.Cookies.Delete(_cartName);
                httpContext.Response.Cookies.Append(_cartName, json, new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1)
                });
            }
        }
        

        public CookieCartService(IProductData productData, IHttpContextAccessor httpContextAccessor)
        {
            _productData = productData;
            _httpContextAccessor = httpContextAccessor;

            var user = _httpContextAccessor.HttpContext.User;
            var userName = user.Identity.IsAuthenticated ? user.Identity.Name : null;
            _cartName = $"cart{userName}";
        }


        public void DecrementFromCart(int id)
        {
            var cart = Cart;

            var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == id);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 0)
                    cartItem.Quantity--;
                if(cartItem.Quantity == 0)
                    cart.Items.Remove(cartItem);

                Cart = cart;
            }
        }

        public void RemoveFromCart(int id)
        {
            var cart = Cart;

            var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == id);

            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                Cart = cart;
            }
        }

        public void RemoveAll()
        {
            var cart = Cart;
            cart.Items.Clear();
            Cart = cart;
        }

        public void AddToCart(int id)
        {
            var cart = Cart;
            var cartItem = cart.Items.FirstOrDefault(item => item.ProductId == id);

            if (cartItem != null)
                cartItem.Quantity++;
            else
                cart.Items.Add(new CartItem {ProductId = id, Quantity = 1});

            Cart = cart;
        }

        public CartViewModel TransFromCart()
        {
            var products = _productData.GetProducts(new ProductFilter
            {
                Ids = Cart.Items.Select(item => item.ProductId).ToList()
            });

            var productViewModels = products.Select(p => p.CreateProduct().CreateViewModel());

            return new CartViewModel
            {
                Items = Cart.Items.ToDictionary(
                    x => productViewModels.First(p => p.Id == x.ProductId), 
                    x => x.Quantity)
            };
        }
    }
}
