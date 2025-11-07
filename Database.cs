using Mobilprog.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilprog
{
    public class Database
    {
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Clone>().Wait();
            _database.CreateTableAsync<Squad>().Wait();
            _database.CreateTableAsync<Battle>().Wait();
            _database.CreateTableAsync<BattleSquad>().Wait();
        }

        public Task<List<Clone>> GetClonesAsync() =>
        _database.Table<Clone>().ToListAsync();

        public Task<int> SaveCloneAsync(Clone clone) =>
            _database.InsertAsync(clone);

        public Task<int> DeleteCloneAsync(Clone clone) =>
            _database.DeleteAsync(clone);

        public Task DeleteAllClonesAsync() =>
            _database.DeleteAllAsync<Clone>();

        public Task<List<Squad>> GetSquadsAsync() =>
        _database.Table<Squad>().ToListAsync();

        public Task<int> SaveSquadAsync(Squad squad) =>
            _database.InsertAsync(squad);

        public Task<int> UpdateSquadAsync(Squad squad) =>
            _database.UpdateAsync(squad);

        public async Task<List<Squad>> GetSquadsWithCloneCountsAsync()
        {
            var squads = await _database.Table<Squad>().ToListAsync();
            var clones = await _database.Table<Clone>().ToListAsync();

            foreach (var squad in squads)
            {
                squad.CloneCount = clones.Count(c => c.Squad_id == squad.Id);
            }

            return squads;
        }

        public Task<int> DeleteSquadAsync(Squad squad) =>
            _database.DeleteAsync(squad);

        public Task DeleteAllSquadAsync() =>
            _database.DeleteAllAsync<Squad>();

        public Task<List<Battle>> GetBattlesAsync() =>
        _database.Table<Battle>().ToListAsync();

        public Task<int> SaveBattleAsync(Battle battle) =>
            _database.InsertAsync(battle);

        public Task<List<BattleSquad>> GetBattleSquadsAsync() =>
        _database.Table<BattleSquad>().ToListAsync();

        public Task<int> SaveBattleSquadAsync(BattleSquad bs) =>
            _database.InsertAsync(bs);

    }
}
