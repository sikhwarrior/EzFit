using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using EzFit.Models;
using System.Linq;

namespace EzFit
{    
    public static class TaskExtensions
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }

            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
    public class InfosDatabase
    {

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;

       

        static bool initialized = false;
  

        public InfosDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Infos).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Infos)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

      

        public Task<List<Infos>> GetInfosAsync()
        {
            return Database.Table<Infos>().ToListAsync();
        }

        //CRUD PERSO
        public Task<List<Infos>> GetNameAsync()
        {
            // SQL queries are also possibl
          
            return Database.QueryAsync<Infos>("SELECT Name FROM Infos;");
        }
        public Task<List<Infos>> GetPersoAsync()
        {
            // SQL queries are also possibl
            //return Database.QueryAsync<Infos>("SELECT * FROM Infos GROUP BY Name;");
            return Database.QueryAsync<Infos>("SELECT * FROM Infos GROUP BY ID ;");

        }


        public Task<List<Infos>> GetPoidsAsync()
        {
            // SQL queries are also possibl

            return Database.QueryAsync<Infos>("SELECT Poids FROM Infos;");
        }

        public Task<List<Infos>> GetIMGAsync()
        {
            // SQL queries are also possibl.ToListAsync();
            return Database.Table<Infos>().OrderBy(i => i.IMG).ToListAsync();
            //return Database.QueryAsync<Infos>("SELECT IMG FROM Infos;");
        }

        public Task<List<Infos>> GetTailleAsync()
        {
            // SQL queries are also possibl

            return Database.QueryAsync<Infos>("SELECT Taille FROM Infos;");
        }

        public Task<List<Infos>> GetSexAsync()
        {
            // SQL queries are also possibl

            return Database.QueryAsync<Infos>("SELECT ValSex FROM Infos;");
        }
        public Task<List<Infos>> GetStyleAsync()
        {
            // SQL queries are also possibl

            return Database.QueryAsync<Infos>("SELECT ValLifeStyle FROM Infos;");
        }
        public Task<List<Infos>> GetObjectifAsync()
        {
            // SQL queries are also possibl

            return Database.QueryAsync<Infos>("SELECT ValObjectif FROM Infos;");
        }
        public Task<Infos> GetIAsync(string id)
        {
            return Database.Table<Infos>().Where(i => i.Name == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveInfosAsync(Infos infos)
        {
            if (infos.ID != 0)
            {
                return Database.UpdateAsync(infos);
            }
            else
            {
                return Database.InsertAsync(infos);
            }
        }

        public Task<int> DeleteInfosAsync(Infos infos)
        {
            return Database.DeleteAsync(infos);
        }

        //public Task<int> UpdateInfosAsync(Infos infos)
        //{
        //    return Database.UpdateAsync(infos);
        //}

    }
}
