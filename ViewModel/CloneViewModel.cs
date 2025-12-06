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
            if (string.IsNullOrWhiteSpace(Clone?.Name) || string.IsNullOrWhiteSpace(Clone?.Rank) || Clone?.Squad_id <= 0 ||
                string.IsNullOrWhiteSpace(Clone?.Image))
                return;

            var squad = (await database1.GetSquadsAsync())
                .FirstOrDefault(s => s.Id == Clone.Squad_id);

            if (squad == null)
            {
                squad = new Squad { Name = $"Squad {Clone.Squad_id}", Id = Clone.Squad_id, CloneCount = 1 };
                await database1.SaveSquadAsync(squad);
            }
            else 
            { 
                var allClones = await database1.GetClonesAsync();
                squad.CloneCount = allClones.Count(c => c.Squad_id == squad.Id);
                await database1.UpdateSquadAsync(squad);
            }

            await database1.SaveCloneAsync(Clone);
            Clones.Add(Clone);
            Clone = new Clone();

        }


        [RelayCommand]
        public async Task DeleteCloneAsync()
        {
            if (Clone == null || Clone.Id ==0) return;


            await database1.DeleteCloneAsync(Clone);
            
            var squad = (await database1.GetSquadsAsync())
                .FirstOrDefault(s => s.Id == Clone.Squad_id);

            var allClones = await database1.GetClonesAsync();
            squad.CloneCount = allClones.Count(c => c.Squad_id == squad.Id);


            if (squad.CloneCount == 0)
            {
                await database1.DeleteSquadAsync(squad);
            }

            Clones.Remove(Clone);

            Clone = new Clone();
        }


        [RelayCommand]
        public async Task PickImageAsync()
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick a clone image",
                FileTypes = FilePickerFileType.Images
            });

            if (result != null)
                await SaveImageToLocalAsync(result);
        }


        [RelayCommand]
        public async Task CaptureImageAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();

                if (photo != null)
                    await SaveImageToLocalAsync(photo);
            }
            catch (FeatureNotSupportedException)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Camera not supported", "OK");
            }
        }


        private async Task SaveImageToLocalAsync(FileResult file)
        {
            var newFile = Path.Combine(FileSystem.AppDataDirectory, file.FileName);

            using (var source = await file.OpenReadAsync())
            using (var dest = File.OpenWrite(newFile))
            {
                await source.CopyToAsync(dest);
            }

            Clone.Image = newFile;
            OnPropertyChanged(nameof(Clone));
        }


        [RelayCommand]
        public async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
