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



        //clones lekérdezése
        public Task<List<Clone>> GetClonesAsync() =>
        _database.Table<Clone>().ToListAsync();

        //clone mentése
        public Task<int> SaveCloneAsync(Clone clone) =>
            _database.InsertAsync(clone);


        //clone törlése
        public Task<int> DeleteCloneAsync(Clone clone) =>
            _database.DeleteAsync(clone);


        //összes clone törlése
        public Task DeleteAllClonesAsync() =>
            _database.DeleteAllAsync<Clone>();


        //squadok lekérdezése
        public Task<List<Squad>> GetSquadsAsync() =>
        _database.Table<Squad>().ToListAsync();


        //squad mentése
        public Task<int> SaveSquadAsync(Squad squad) =>
            _database.InsertAsync(squad);


        //squad frissítése
        public Task<int> UpdateSquadAsync(Squad squad) =>
            _database.UpdateAsync(squad);


        //squadok lekérdezése klón számmal
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


        //squad törlése
        public Task DeleteSquadAsync(Squad squad) =>
            _database.DeleteAsync(squad);

        //adott squad törlése
        public Task<int> DeleteThisSquadAsync(int squad_id) =>
            _database.Table<Squad>().Where(s => s.Id == squad_id).DeleteAsync();

        //összes squad törlése
        public Task DeleteAllSquadAsync() =>
            _database.DeleteAllAsync<Squad>();

        //battle lekérdezése
        public Task<List<Battle>> GetBattlesAsync() =>
        _database.Table<Battle>().ToListAsync();

        //battle mentése
        public Task<int> SaveBattleAsync(Battle battle) =>
            _database.InsertAsync(battle);


        //battleSquad lekérdezése
        public async Task<List<BattleSquad>> GetBattleSquadsByBattleIdAsync(int battleId) =>
            await _database.Table<BattleSquad>().Where(bs => bs.BattleId == battleId).ToListAsync();


        //battleSquad mentése
        public Task<int> SaveBattleSquadAsync(BattleSquad bs) =>
            _database.InsertAsync(bs);

    }
}
