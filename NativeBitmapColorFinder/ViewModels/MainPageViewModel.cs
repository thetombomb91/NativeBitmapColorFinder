using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NativeBitmapColorFinder.ViewModels
{
	public class MainPageViewModel : BindableBase, INavigationAware
	{
		IBitmapColor _bitmapColor;

		private string _title;
		public string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}

		public MainPageViewModel(IBitmapColor bitmapColor)
		{
			_bitmapColor = bitmapColor;
		}

		public void OnNavigatedFrom(NavigationParameters parameters)
		{

		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			if (parameters.ContainsKey("title"))
				Title = (string)parameters["title"] + " and Prism";

            _bitmapColor.Convert(Globals.colorWheel.Source);
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			
		}
	}
}

