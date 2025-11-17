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

        
        private async Task LoadBattlesAsync()
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

            
        }
    }
}
