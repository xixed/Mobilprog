using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mobilprog.Models;
using Mobilprog.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mobilprog.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private Database database1;

        [ObservableProperty]
        private Squad selectedSquad;


        [ObservableProperty]
        private List<Squad> squads;

        public MainPageViewModel(Database database)
        {
            database1 = database;
            _ = LoadSquadsAsync();
        }


        [RelayCommand]
        public async Task LoadSquadsAsync()
        {
            Squads = await database1.GetSquadsWithCloneCountsAsync();
        }

        [RelayCommand]
        public async Task DeleteAllSquadAsync()
        {
            await database1.DeleteAllSquadAsync();
            await database1.DeleteAllClonesAsync();
            Squads = new List<Squad>();
        }

        

        [RelayCommand]
        public async Task GoToSquadsPageAsync()
        {

            if (SelectedSquad != null)
            {

                var parameters = new Dictionary<string, object>
                {
                    { "Squad", SelectedSquad }
                };



                await Shell.Current.GoToAsync("squad", parameters);
            }
            else
            {
                WeakReferenceMessenger.Default.Send("Select a Squad");
            }

        }


        [RelayCommand]
        public async Task GoToClonePageAsync()
        {
            await Shell.Current.GoToAsync("clone");
        }

        [RelayCommand]
        public async Task GoToBattlePageAsync()
        {
            await Shell.Current.GoToAsync("battle");
        }
    }
}
