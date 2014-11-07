using Gtk;
using System;
using System.Data;

using SerpisAd;

namespace PArticulo
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class ListArticuloView : Gtk.Bin
	{
		private IDbConnection dbConnection;
		private ListStore listStore;

		public ListArticuloView ()
		{
			this.Build ();
			deleteAction.Sensitive = false;
			editAction.Sensitive = false;

			dbConnection = App.Instance.DbConnection;

			treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
			treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
			treeView.AppendColumn ("categoria", new CellRendererText (), "text", 2);
			//articuloTreeView.AppendColumn ("precio", new CellRendererText (), "text", 3);
			treeView.AppendColumn ("precio", new CellRendererText (), 
			                               new TreeCellDataFunc (delegate(TreeViewColumn tree_column, CellRenderer cell, 
			                               TreeModel tree_model, TreeIter iter) {
				object value = tree_model.GetValue(iter, 3);
				((CellRendererText)cell).Text = value != DBNull.Value ? value.ToString() : "null";
			})
			                               );
			listStore = new ListStore (typeof(ulong), typeof(string), 
			                                   typeof(string), typeof(decimal));
			treeView.Model = listStore;

			fillListStore ();

			treeView.Selection.Changed += selectionChanged;

			refreshAction.Activated += delegate {
				listStore.Clear();
				fillListStore();
			};

			editAction.Activated += delegate {
				new ArticuloView();

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
			dbCommand.CommandText = "select a.id, a.nombre, c.nombre as categoria, a.precio" +
				" from articulo a left join categoria c on (a.categoria = c.id)";

			IDataReader dataReader = dbCommand.ExecuteReader ();
			while (dataReader.Read()) {
				object id = dataReader ["id"];
				object nombre = dataReader ["nombre"];
				object categoria = dataReader ["categoria"].ToString();
				object precio = dataReader ["precio"];
				listStore.AppendValues (id, nombre, categoria, precio);
			}
			dataReader.Close ();
		}

	}
}

