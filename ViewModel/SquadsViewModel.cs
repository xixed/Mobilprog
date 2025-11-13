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
    [QueryProperty(nameof(Squad), "Squad")]
    public partial class SquadsViewModel : ObservableObject
    {
        private readonly Database _database;

        

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

        partial void OnSquadChanged(Squad value)
        {
            if (value != null)
            {
                _database.UpdateSquadAsync(value);
                _ = LoadSquadDetailsAsync(value);
            }
        }

        public async Task LoadSquadDetailsAsync(Squad squad)
        {
            Squad = squad;
            

            var allSquads = await _database.GetSquadsAsync();
            

            var allClones = await _database.GetClonesAsync();
            var squadClones = allClones.Where(c => c.Squad_id == squad.Id).ToList();

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
            
            await _database.DeleteThisSquadAsync(Squad.Id);

            Clones.Clear();
            
        }

        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

