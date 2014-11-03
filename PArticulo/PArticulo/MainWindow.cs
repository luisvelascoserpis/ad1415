using Gtk;
using System;
using System.Data;

using SerpisAd;

public partial class MainWindow: Gtk.Window
{	
	private IDbConnection dbConnection;
	private ListStore articuloListStore;
	private ListStore categoriaListStore;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		articuloDeleteAction.Sensitive = false;
		categoriaDeleteAction.Sensitive = false;
		articuloEditAction.Sensitive = false;
		categoriaEditAction.Sensitive = false;

		dbConnection = App.Instance.DbConnection;

		articuloTreeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		articuloTreeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		articuloTreeView.AppendColumn ("categoria", new CellRendererText (), "text", 2);
		//articuloTreeView.AppendColumn ("precio", new CellRendererText (), "text", 3);
		articuloTreeView.AppendColumn ("precio", new CellRendererText (), 
		    new TreeCellDataFunc (delegate(TreeViewColumn tree_column, CellRenderer cell, 
		        TreeModel tree_model, TreeIter iter) {
				object value = tree_model.GetValue(iter, 3);
				((CellRendererText)cell).Text = value != DBNull.Value ? value.ToString() : "null";
			})
		);
		articuloListStore = new ListStore (typeof(ulong), typeof(string), 
		                                   typeof(string), typeof(decimal));
		articuloTreeView.Model = articuloListStore;

		categoriaTreeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		categoriaTreeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		categoriaListStore = new ListStore (typeof(ulong), typeof(string));
		categoriaTreeView.Model = categoriaListStore;

		fillArticuloListStore ();
		fillCategoriaListStore ();

		articuloTreeView.Selection.Changed += articuloSelectionChanged;
		categoriaTreeView.Selection.Changed += categoriaSelectionChanged;

		articuloRefreshAction.Activated += delegate {
			articuloListStore.Clear();
			fillArticuloListStore();
		};

		categoriaRefreshAction.Activated += delegate {
			categoriaListStore.Clear();
			fillCategoriaListStore();
		};

		//TODO resto de actions
	}

	private void articuloSelectionChanged (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		bool hasSelected = articuloTreeView.Selection.CountSelectedRows () > 0;
		articuloDeleteAction.Sensitive = hasSelected;
		articuloEditAction.Sensitive = hasSelected;
	}

	private void categoriaSelectionChanged (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		bool hasSelected = categoriaTreeView.Selection.CountSelectedRows () > 0;
		categoriaDeleteAction.Sensitive = hasSelected;
		categoriaEditAction.Sensitive = hasSelected;
	}

	private void fillArticuloListStore() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select a.id, a.nombre, c.nombre as categoria, a.precio" +
			" from articulo a left join categoria c on (a.categoria = c.id)";

		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			object categoria = dataReader ["categoria"].ToString();
			object precio = dataReader ["precio"];
			articuloListStore.AppendValues (id, nombre, categoria, precio);
		}
		dataReader.Close ();
	}

	private void fillCategoriaListStore() {
		IDbCommand dbCommand = dbConnection.CreateCommand ();
		dbCommand.CommandText = "select * from categoria";

		IDataReader dataReader = dbCommand.ExecuteReader ();
		while (dataReader.Read()) {
			object id = dataReader ["id"];
			object nombre = dataReader ["nombre"];
			categoriaListStore.AppendValues (id, nombre);
		}
		dataReader.Close ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
