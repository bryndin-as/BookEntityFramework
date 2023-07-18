using Book.DAL.Context;
using Book.DAL.Entityes.Base;
using Book.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.DAL
{
    //Базовая реализация репозитория
    public class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly BookDB _dB;
        private readonly DbSet<T> _Set; // свойство набора данных из текущего репозитория

        public bool AutoSavaChanges { get; set; } = true;

        public DbRepository(BookDB dB)

        {
            _dB = dB;
            _Set = _dB.Set<T>();
        }



        public virtual IQueryable<T> Items => _Set;

        #region GET
        public T Get(int id)
        {
            return Items.SingleOrDefault(item => item.Id == id);
        }

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default)
        {
            return await Items.SingleOrDefaultAsync(item => item.Id == id, Cancel).ConfigureAwait(false);
        }

        #endregion

        #region ADD
        public T Add(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _dB.Entry(item).State = EntityState.Added;
            if (AutoSavaChanges) // нужно для того, чтобы если пачкой закидывать данные не каждый объект синхронизировался, а после всех объектов 1 раз
            {
                _dB.SaveChanges();
            }

            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _dB.Entry(item).State = EntityState.Added;
            if (AutoSavaChanges) // нужно для того, чтобы если пачкой закидывать данные не каждый объект синхронизировался, а после всех объектов 1 раз
            {
                await _dB.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }

            return item;

        }

        #endregion

        #region Update
        public void Update(T item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _dB.Entry(item).State = EntityState.Modified;
            if (AutoSavaChanges) // нужно для того, чтобы если пачкой закидывать данные не каждый объект синхронизировался, а после всех объектов 1 раз
            {
                _dB.SaveChanges();
            }
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _dB.Entry(item).State = EntityState.Modified;
            if (AutoSavaChanges) // нужно для того, чтобы если пачкой закидывать данные не каждый объект синхронизировался, а после всех объектов 1 раз
            {
                await _dB.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }

        }

        #endregion

        #region MyRegion
        public void Remove(int id)
        {

            //Если большая сущность, то все данные будут переданы из БД в приложение, что не очень хорошо

            //var item = Get(id);
            //if (item is null) return;
            //_dB.Entry(item);
            //if (AutoSavaChanges) // нужно для того, чтобы если пачкой закидывать данные не каждый объект синхронизировался, а после всех объектов 1 раз
            //{
            //    _dB.SaveChanges();
            //}

            _dB.Remove(new T { Id = id }); // Можем не узнать, что сущность была удалена, т.к. будет загружен только первичный ключ
            if (AutoSavaChanges) // нужно для того, чтобы если пачкой закидывать данные не каждый объект синхронизировался, а после всех объектов 1 раз
            {
                _dB.SaveChanges();
            }

        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {

            _dB.Remove(new T { Id = id });
            if (AutoSavaChanges) // нужно для того, чтобы если пачкой закидывать данные не каждый объект синхронизировался, а после всех объектов 1 раз
            {
                await _dB.SaveChangesAsync(Cancel).ConfigureAwait(false);
            }
        }
        #endregion
    }
}
