using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Logging;
using Prism.Services;
using Xamarin.Forms.Maps;
using System.Collections.ObjectModel;
using PhotoMapApp.Services.Definitions;
using PhotoMapApp.Models;
using Plugin.Geolocator;

namespace PhotoMapApp.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        private IPostService _postService;
        public Map Map { get; private set; }
        private Pin _currentPositionPin;
        private ObservableCollection<Pin> _pinCollection = new ObservableCollection<Pin>();
        public ObservableCollection<Pin> PinCollection { get { return _pinCollection; } set { _pinCollection = value; OnPropertyChanged(); } }

        public MapPageViewModel(INavigationService navigationService, IPostService postService) : base(navigationService)
        {
            this._postService = postService;
            this.Map = new Map();
            this._currentPositionPin = new Pin();

            foreach (Post post in this._postService.GetPosts())
            {
                this.Map.Pins.Add(new Pin() { Position = post.GetPosition(), Type = PinType.Generic, Label = post.Name });
            }
            System.Diagnostics.Debug.WriteLine("TEST");

            Plugin.Geolocator.CrossGeolocator.Current.PositionChanged += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("TEST2");
                UpdateCurrentPositionAsync();
            };

            UpdateCurrentPositionAsync();
        }

        public async void UpdateCurrentPositionAsync() 
        { 
            var geolocatorPosition = await Plugin.Geolocator.CrossGeolocator.Current.GetPositionAsync();
            this._currentPositionPin.Position = new Position(geolocatorPosition.Latitude, geolocatorPosition.Longitude);

            if (!this.Map.Pins.Contains(this._currentPositionPin))
            {
                this.Map.Pins.Add(this._currentPositionPin);
            }
        }


    }
}
