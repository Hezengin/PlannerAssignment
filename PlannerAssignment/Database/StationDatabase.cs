using PlannerAssignment.Mvvm.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerAssignment.Database
{
    public class StationDatabase
    {
        private SQLiteAsyncConnection _connection;

        public StationDatabase(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
        }

        public async Task InitializeDBAsync()
        {
            await _connection.CreateTableAsync<Names>().ContinueWith((res) => Debug.WriteLine("Created table session"));
        }

        public async Task<List<Names>> GetAllStationsAsync()
        {
            await InitializeDBAsync();
            return await _connection.Table<Names>().ToListAsync();
        }

        public async Task<Names> GetStationByNameAsync(string name)
        {
            await InitializeDBAsync();
            return await _connection.Table<Names>().Where(i => i.Long == name).FirstOrDefaultAsync();
        }

        public async Task<int> SaveStationNameAsync(Station station)
        {
            await InitializeDBAsync();
            return await _connection.InsertAsync(station.Namen);
        }

        public Task<int> UpdateStationAsync(Names name) =>
            _connection.UpdateAsync(name);

        public Task<int> DeleteStationAsync(Names name) =>
            _connection.DeleteAsync(name);
    }
}
