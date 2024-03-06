﻿using Core.Entities;
using DataAccess.Repositories.Abstarct;
using Microsoft.AspNetCore.Identity;
using Web.Services.Abstract;
using Web.ViewModels.Basket;

namespace Web.Services.Concrete
{
    public class BasketService : IBasketService
    {
        private readonly IBasketProductRepository _basketProductRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketService(IBasketProductRepository basketProductRepository,
            IBasketRepository basketRepository,
            IProductRepository productRepository,
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _basketProductRepository = basketProductRepository;
            _basketRepository = basketRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<bool> Add(int productId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;

            var product = await _productRepository.GetAsync(productId);
            if (product == null) return false;

            var basket = await _basketRepository.GetBasketWithProducts(user.Id);

            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = user.Id,
                    CreatedAt = DateTime.Now
                };
                await _basketRepository.CreateAsync(basket);
            }

            var basketProduct = await _basketProductRepository.GetBasketProducts(productId, basket.Id);
            if (basketProduct != null)
            {
                basketProduct.Quantity++;
                await _basketProductRepository.UpdateAsync(basketProduct);
            }
            else
            {
                basketProduct = new BasketProduct
                {
                    BasketId = basket.Id,
                    ProductId = product.Id,
                    Quantity = 1,
                    CreatedAt = DateTime.Now,
                 
                };
                await _basketProductRepository.CreateAsync(basketProduct);
            }

            return true;
        }

        public async Task<bool> ClearBasket(int id)
        {
            var basket = await _basketRepository.GetAsync(id);
            if (basket == null) return false;
            await _basketRepository.DeleteAsync(basket);
            return true;
        }

        public async Task<bool> DecreaseCountAsync(int id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;
            var basketProduct = await _basketRepository.GetBasketWithProducts(user.Id);
            if (basketProduct != null)
            {
                foreach (var dbbasketProduct in basketProduct.BasketProducts)
                {
                    if (dbbasketProduct.ProductId == id)
                    {
                        if (dbbasketProduct.Quantity > 1)
                        {
                            dbbasketProduct.Quantity--;
                            await _basketRepository.UpdateAsync(basketProduct);
                        }
                    }
                }
            }
            return true;
        }

        public async Task<BasketIndexVM> GetBasketProducts()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return null;
            var basket = await _basketRepository.GetBasketWithProducts(user.Id);
            var model = new BasketIndexVM();
            if (basket != null)
            {
                foreach (var basketProduct in basket.BasketProducts)
                {
                    var basketProducts = new BasketProductVM
                    {
                        Id = basketProduct.ProductId,
                        Photoname = basketProduct.Product.MainPhoto,
                        Price = basketProduct.Product.Price,
                        Quantity = basketProduct.Quantity,
                        Title = basketProduct.Product.Title,
                    };
                    model.Count = await _basketProductRepository.GetUserBasketProductsCount(_httpContextAccessor.HttpContext.User);
                    model.BasketId = basketProduct.BasketId;
                    model.BasketProducts.Add(basketProducts);
                }
            }
            else
            {
                basket = new Basket();
            }
            return model;
        }

        public async Task<bool> IncreaseCountAsync(int id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;
            var basketProduct = await _basketRepository.GetBasketWithProducts(user.Id);
            if (basketProduct != null)
            {
                foreach (var dbbasketProduct in basketProduct.BasketProducts)
                {
                    if (dbbasketProduct.ProductId == id)
                    {
                        dbbasketProduct.Quantity++;
                        await _basketRepository.UpdateAsync(basketProduct);
                    }
                }
            }
            return true;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null) return false;

            var basketProduct = await _basketRepository.GetBasketWithProducts(user.Id);
            if (basketProduct == null) return false;

            var product = await _productRepository.GetAsync(id);
            if (product == null) return false;

            await _basketProductRepository.DeleteProductAsync(product.Id);
            return true;
        }


    }
}
