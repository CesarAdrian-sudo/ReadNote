using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using ReadNote.Droid;
using ReadNote.Datos;
using Xamarin.Forms;

[assembly : Dependency(typeof(SQLiteDB))]
namespace ReadNote.Droid

{
	internal class SQLiteDB : ISQLiteDB
    {
		public SQLiteAsyncConnection GetConnection() {
			var ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
			//Se crea la Base de Datos
			var path = Path.Combine(ruta, "ReadNoteSQLite.db3");
			return new SQLiteAsyncConnection(path);
				}
	}
}