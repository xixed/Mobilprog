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
    public partial class CloneViewModel : ObservableObject
    {
        [ObservableProperty]
        private Clone clone = new Clone();



        [ObservableProperty]
        private ObservableCollection<Clone> clones = new();


        private Database database1;

        public CloneViewModel(Database database)
        {
            database1 = database;
            _ = LoadClonesAsync();
        }

        private async Task LoadClonesAsync()
        {
            var list = await database1.GetClonesAsync();
            Clones = new ObservableCollection<Clone>(list);
        }

        [RelayCommand]
        public async Task AddCloneAsync()
        {
            if (string.IsNullOrWhiteSpace(Clone?.Name) || string.IsNullOrWhiteSpace(Clone?.Rank))
                return;

            await database1.SaveCloneAsync(Clone);
            
            Clones.Add(Clone);

            Clone = new Clone();

        }


        [RelayCommand]
        public async Task DeleteCloneAsync()
        {
            await database1.DeleteCloneAsync(Clone);

            Clones.Remove(Clone);

            Clone = new Clone();
        }

    }
}
