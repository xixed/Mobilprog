using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mobilprog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilprog.ViewModel
{
    [QueryProperty(nameof(SquadId), "SquadId")]
    public partial class SquadsViewModel : ObservableObject
    {
        private readonly Database _database;

        [ObservableProperty]
        private int squadId;

        [ObservableProperty]
        private Squad squad;

        [ObservableProperty]
        private ObservableCollection<Clone> clones;

        [ObservableProperty]
        private string cloneCountText;

        public SquadsViewModel(Database database)
        {
            _database = database;
            Clones = new ObservableCollection<Clone>();
        }

        partial void OnSquadIdChanged(int value)
        {
            LoadSquadDetailsAsync(value);
        }

        public async Task LoadSquadDetailsAsync(int squadId)
        {
            var allSquads = await _database.GetSquadsAsync();
            Squad = allSquads.FirstOrDefault(s => s.Id == squadId);

            var allClones = await _database.GetClonesAsync();
            var squadClones = allClones.Where(c => c.Squad_id == squadId).ToList();

            Clones = new ObservableCollection<Clone>(squadClones);
            CloneCountText = $"Clones in squad: {Clones.Count}";
        }

        [RelayCommand]
        public async Task DeleteAllClonesAsync()
        {
            if (Squad == null)
                return;

            var allClones = await _database.GetClonesAsync();
            var squadClones = allClones.Where(c => c.Squad_id == Squad.Id);

            foreach (var clone in squadClones)
            {
                await _database.DeleteCloneAsync(clone);
            }

            Clones.Clear();
            CloneCountText = "Clones in squad: 0";
        }

        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

