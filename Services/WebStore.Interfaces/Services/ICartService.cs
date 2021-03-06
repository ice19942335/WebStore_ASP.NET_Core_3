﻿using WebStore.Domain.ViewModels.Cart;

namespace WebStore.Interfaces.Services
{
    public interface ICartService
    {
        void DecrementFromCart(int id);

        void RemoveFromCart(int id);

        void RemoveAll();

        void AddToCart(int id);

        CartViewModel TransFromCart();
    }
}
