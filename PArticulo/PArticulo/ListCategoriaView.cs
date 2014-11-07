using Gtk;
using System;
using System.Data;

using SerpisAd;

namespace PArticulo
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ListCategoriaView : Gtk.Bin
	{
		private IDbConnection dbConnection;
		private ListStore listStore;

		public ListCategoriaView ()
		{
			this.Build ();

			deleteAction.Sensitive = false;
			editAction.Sensitive = false;

			dbConnection = App.Instance.DbConnection;


			treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
			treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
			listStore = new ListStore (typeof(ulong), typeof(string));
			treeView.Model = listStore;

			fillListStore ();

			treeView.Selection.Changed += selectionChanged;

			refreshAction.Activated += delegate {
				listStore.Clear();
				fillListStore();
			};

			//TODO resto de actions

		}

		private void selectionChanged (object sender, EventArgs e) {
			Console.WriteLine ("selectionChanged");
			bool hasSelected = treeView.Selection.CountSelectedRows () > 0;
			deleteAction.Sensitive = hasSelected;
			editAction.Sensitive = hasSelected;
		}

		private void fillListStore() {
			IDbCommand dbCommand = dbConnection.CreateCommand ();
			dbCommand.CommandText = "select * from categoria";

			IDataReader dataReader = dbCommand.ExecuteReader ();
			while (dataReader.Read()) {
				object id = dataReader ["id"];
				object nombre = dataReader ["nombre"];
				listStore.AppendValues (id, nombre);
			}
			dataReader.Close ();
		}

	}
}

