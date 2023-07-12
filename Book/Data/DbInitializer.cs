using Book.DAL.Context;
using Book.DAL.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Data
{
    public class DbInitializer
    {
        private readonly BookDB _db;
        private readonly ILogger<DbInitializer> _Logger;

        public DbInitializer(BookDB db, ILogger<DbInitializer> Logger)
        {
            _db = db;
            _Logger = Logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation("Инициализация БД..");

            _Logger.LogInformation("Удаление существующей БД..");
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _Logger.LogInformation($"Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);

            //_db.Database.EnsureCreated();
            _Logger.LogInformation("Миграция БД..");
            await _db.Database.MigrateAsync();
            _Logger.LogInformation($"Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

            if (await _db.BooksElems.AnyAsync()) return;

            await InitializerCategories();
            await InitializerBooks();
            await InitializerSellerss();
            await InitializerBuyerss();
            await InitializerDeal();

            _Logger.LogInformation($"Инициализация БД выполнено за {0} с", timer.Elapsed.TotalSeconds);
        }



        private const int __CategoriesCount = 10;

        private Category[] _Categories;

        private async Task InitializerCategories()
        {

            var timer = Stopwatch.StartNew();
            _Logger.LogInformation($"Инициализация категорий");


            _Categories = new Category[__CategoriesCount];
            for (int i = 0; i < __CategoriesCount; i++)
            {
                _Categories[i] = new Category
                {
                   Name = $"Категория {i + 1}"
                };
            }

            await _db.Categories.AddRangeAsync(_Categories);
            await _db.SaveChangesAsync();

            _Logger.LogInformation($"Инициализация категорий выполнена за {0} мс", timer.ElapsedMilliseconds);

        }


        private const int __BookCount = 10;

        private BookElem[] _Books;

        private async Task InitializerBooks()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation($"Инициализация книг");


            var rnd = new Random();
            _Books = Enumerable.Range(1, __BookCount).Select(i => new BookElem
            {
                Name = $"Книга {i}",
                Category = _Categories[0]
            })
            .ToArray();

            await _db.BooksElems.AddRangeAsync(_Books);
            await _db.SaveChangesAsync();

            _Logger.LogInformation($"Инициализация книг выполнена за {0} мс", timer.ElapsedMilliseconds);

        }


        private const int __SellersCount = 10;

        private Seller[] _Sellers;

        private async Task InitializerSellerss()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation($"Инициализация продавцов");


            var rnd = new Random();
            _Sellers = Enumerable.Range(1, __SellersCount).Select(i => new Seller
            {
                Name = $"Продавец - Имя {i}",
                Surname = $"Продавец - Фамилия {i}",
                Patronymic = $"Продавец - Отчество {i}",
            })
            .ToArray();

            await _db.Sellers.AddRangeAsync(_Sellers);
            await _db.SaveChangesAsync();

            _Logger.LogInformation($"Инициализация продавцов выполнена за {0} мс", timer.ElapsedMilliseconds);

        }

        private const int __BuyersCount = 10;

        private Buyer[] _Buyers;

        private async Task InitializerBuyerss()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation($"Инициализация покупателей");

            var rnd = new Random();
            _Buyers = Enumerable.Range(1, __BuyersCount).Select(i => new Buyer
            {
                Name = $"Покупатель - Имя {i}",
                Surname = $"Покупатель - Фамилия {i}",
                Patronymic = $"Покупатель - Отчество {i}",
            })
            .ToArray();

            await _db.Buyers.AddRangeAsync(_Buyers);
            await _db.SaveChangesAsync();

            _Logger.LogInformation($"Инициализация покупателей выполнена за {0} мс", timer.ElapsedMilliseconds);

        }


        private const int __DealsCount = 1000;

        private async Task InitializerDeal()
        {
            var timer = Stopwatch.StartNew();
            _Logger.LogInformation($"Инициализация сделок");

            var rnd = new Random();

            var deals = Enumerable.Range(1, __DealsCount).Select(s => new Deal()
            {
                BookElem = _Books[0],
                Seller = _Sellers[0],
                Buyer = _Buyers[0],
                Price = (decimal)(rnd.NextDouble() * 4000 + 700)

            });

            await _db.Deals.AddRangeAsync(deals);
            await _db.SaveChangesAsync();

            _Logger.LogInformation($"Инициализация сделок выполнена за {0} мс", timer.ElapsedMilliseconds);

        }


    }
}
