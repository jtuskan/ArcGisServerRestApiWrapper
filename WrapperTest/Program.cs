﻿using System;
using System.IO;
using Esri.ArcGisServer.Rest;

namespace WrapperTest
{
	class Program
	{
		static bool finished = false;

		static void Main(string[] args)
		{
			var parameters = new ExportMapParameters
			{
				BoundingBox = new double[] { 
					-14011824.4072731,
					5581676.67702371,
					-12878110.4037477,
					6375398.77873677
				},
				ResponseFormat = ExportMapResponseFormat.Image,
				ImageFormat = ExportMapImageFormat.Png,
				Dpi = 300,
				Size = new int[] { 600, 800 },
				Transparent = true
			};
			var mapService = new MapService { Uri = new Uri("http://wsdot.wa.gov/geosvcs/ArcGIS/rest/services/Shared/WebBaseMapWebMercator/MapServer") };

			mapService.ExportMapCompleted += new EventHandler<MapExportCompletedEventArgs>(mapService_ExportMapCompleted);

			IAsyncResult asyncResult = mapService.BeginExportMap(parameters);

			do
			{

			} while (!finished);

		}

		static void mapService_ExportMapCompleted(object sender, MapExportCompletedEventArgs e)
		{
			using (var stream = e.ResponseStream)
			{
				using (FileStream fs = new FileStream("output.png", FileMode.Create))
				{
					stream.CopyTo(fs);
				}
			}
			finished = true;
		}
	}
}