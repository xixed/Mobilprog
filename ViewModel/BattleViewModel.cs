using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobilprog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mobilprog.ViewModel
{
    public partial class BattleViewModel : ObservableObject
    {

        private readonly Database _database;

        [ObservableProperty]
        private Battle newBattle = new();

        [ObservableProperty]
        private ObservableCollection<Squad> allSquads = new();

        [ObservableProperty]
        private ObservableCollection<Squad> republicSquads = new();

        [ObservableProperty]
        private ObservableCollection<Squad> separatistSquads = new();

        [ObservableProperty]
        private Squad selectedRepublicSquad;

        [ObservableProperty]
        private Squad selectedSeparatistSquad;

        public BattleViewModel(Database database)
        {
            _database = database;
            _ = LoadSquadsAsync();
        }

        private async Task LoadSquadsAsync()
        {
            var squads = await _database.GetSquadsAsync();
            AllSquads = new ObservableCollection<Squad>(squads);
        }

        [RelayCommand]
        public void AddRepublicSquad()
        {
            if (SelectedRepublicSquad != null && !RepublicSquads.Contains(SelectedRepublicSquad))
                RepublicSquads.Add(SelectedRepublicSquad);
        }

        [RelayCommand]
        public void AddSeparatistSquad()
        {
            if (SelectedSeparatistSquad != null && !SeparatistSquads.Contains(SelectedSeparatistSquad))
                SeparatistSquads.Add(SelectedSeparatistSquad);
        }

        [RelayCommand]
        public async Task SaveBattleAsync()
        {
            if (string.IsNullOrWhiteSpace(NewBattle.Name) ||
                string.IsNullOrWhiteSpace(NewBattle.Location) ||
                NewBattle.Date == default)
                return;

            await _database.SaveBattleAsync(NewBattle);

            foreach (var squad in RepublicSquads)
            {
                await _database.SaveBattleSquadAsync(new BattleSquad
                {
                    BattleId = NewBattle.Id,
                    SquadId = squad.Id,
                    Side = "Republic"
                });
            }

            foreach (var squad in SeparatistSquads)
            {
                await _database.SaveBattleSquadAsync(new BattleSquad
                {
                    BattleId = NewBattle.Id,
                    SquadId = squad.Id,
                    Side = "Separatists"
                });
            }

            // reset
            NewBattle = new Battle();
            RepublicSquads.Clear();
            SeparatistSquads.Clear();
        }
    }
}
