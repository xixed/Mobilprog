using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobilprog.Models;
using Mobilprog.View;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mobilprog.ViewModel
{
    public partial class BattleViewModel : ObservableObject
    {
        private readonly Database _database;

        [ObservableProperty]
        private ObservableCollection<Battle> battles = new();

        [ObservableProperty]
        private Battle selectedBattle;

        public BattleViewModel(Database database)
        {
            _database = database;
            _ = LoadBattlesAsync();
        }

        
        public async Task LoadBattlesAsync()
        {
            var list = await _database.GetBattlesAsync();
            Battles = new ObservableCollection<Battle>(list);
        }



        [RelayCommand]
        public async Task AddBattleAsync()
        {
            await Shell.Current.GoToAsync("battleadd");
        }


        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }

        
        [RelayCommand]
        public async Task RunSimulationAsync()
        {
            if (SelectedBattle == null)
                return;

            var random = new Random();

            var battleSquads = await _database.GetBattleSquadsByBattleIdAsync(SelectedBattle.Id);

            
            var republicSquads = new List<Squad>();
            var separatistSquads = new List<Squad>();

            foreach (var bs in battleSquads)
            {
                var squad = (await _database.GetSquadsAsync()).FirstOrDefault(s => s.Id == bs.SquadId);
                if (squad == null) continue;

                if (bs.Side == "Republic")
                    republicSquads.Add(squad);
                else if (bs.Side == "Separatist")
                    separatistSquads.Add(squad);
            }

            int republicCount = republicSquads.Sum(s => s.CloneCount);
            int separatistCount = separatistSquads.Sum(s => s.CloneCount);

            
            double total = republicCount + separatistCount;
            double republicChance = total == 0 ? 0.5 : (double)republicCount / total;

            bool republicWon = random.NextDouble() < republicChance;
            string winner = republicWon ? "Republic" : "Separatists";

            
            string message = $"Battle '{SelectedBattle.Name}' finished!\nWinner: {winner}\nRepublic Clones Remain: {republicCount}\nSeparatist Clones Remain: {separatistCount}";
            await App.Current.MainPage.DisplayAlert("Simulation Result", message, "OK");


            bool share = await App.Current.MainPage.DisplayAlert("Share Result", "Do you want to share the result?", "Yes", "No");
            if (share)
            {
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = message,
                    Title = "Battle Simulation Result"
                });
            }

            var battles = await _database.GetBattlesAsync();
            var battleToDelete = battles.FirstOrDefault(b => b.Id == SelectedBattle.Id);
            if (battleToDelete != null)
            {
                
                foreach (var bs in battleSquads)
                    await _database.DeleteBattleSquadAsync(bs);

                
                await _database.DeleteBattleAsync(battleToDelete);
            }

            
            SelectedBattle = null;

            
            await LoadBattlesAsync();


        }
    }
}
