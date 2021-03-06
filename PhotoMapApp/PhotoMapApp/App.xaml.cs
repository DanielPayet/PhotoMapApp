﻿using Prism;
using Prism.Ioc;
using PhotoMapApp.ViewModels;
using PhotoMapApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotoMapApp.Services.Definitions;
using PhotoMapApp.Services.Implementations;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PhotoMapApp
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) {}

        public App(IPlatformInitializer initializer) : base(initializer) {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MenuNavigation/NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Navigation

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<PostPage, PostPageViewModel>();
            containerRegistry.RegisterForNavigation<AddPostPage, AddPostPageViewModel>();
            containerRegistry.RegisterForNavigation<MenuNavigation, MenuNavigationViewModel>();
            containerRegistry.RegisterForNavigation<ListPostPage, ListPostPageViewModel>();
            containerRegistry.RegisterForNavigation<MapPage, MapPageViewModel>();
            containerRegistry.RegisterForNavigation<NewPost, NewPostViewModel>();

            // Services

            containerRegistry.RegisterSingleton<IPostService, PostService>();
            containerRegistry.RegisterSingleton<ITagService, TagService>();
            containerRegistry.RegisterSingleton<IImageService, ImageService>();
            containerRegistry.RegisterSingleton<IDatabaseService, DatabaseService>();
            containerRegistry.RegisterSingleton<IGeolocationService, GeolocationService>();
            containerRegistry.RegisterSingleton<IMediaService, MediaService>();
        }
    }
}
