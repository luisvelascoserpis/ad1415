using Gtk;
using MySql.Data.MySqlClient;
using System;

public partial class MainWindow: Gtk.Window
{	
	private MySqlConnection mySqlConnection;
	private ListStore listStore;

	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		deleteAction.Sensitive = false;

		mySqlConnection = new MySqlConnection (
			"DataSource=localhost;Database=dbprueba;User ID=root;Password=sistemas"
		);

		mySqlConnection.Open ();

		treeView.AppendColumn ("id", new CellRendererText (), "text", 0);
		treeView.AppendColumn ("nombre", new CellRendererText (), "text", 1);
		listStore = new ListStore (typeof(ulong), typeof(string));
		treeView.Model = listStore;

		fillListStore ();

		treeView.Selection.Changed += selectionChanged;
	}

	private void selectionChanged (object sender, EventArgs e) {
		Console.WriteLine ("selectionChanged");
		deleteAction.Sensitive = treeView.Selection.CountSelectedRows () > 0;
	}

	private void fillListStore() {
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = "select * from categoria";

		MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader ();
		while (mySqlDataReader.Read()) {
			object id = mySqlDataReader ["id"];
			object nombre = mySqlDataReader ["nombre"];
			listStore.AppendValues (id, nombre);
		}
		mySqlDataReader.Close ();
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		mySqlConnection.Close ();
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnAddActionActivated (object sender, EventArgs e)
	{
		string insertSql = string.Format(
			"insert into categoria (nombre) values ('{0}')",
			"Nuevo " + DateTime.Now
		);
		Console.WriteLine ("insertSql={0}", insertSql);
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = insertSql;

		mySqlCommand.ExecuteNonQuery ();
	}

	protected void OnRefreshActionActivated (object sender, EventArgs e)
	{
		listStore.Clear ();
		fillListStore ();
	}

	protected void OnDeleteActionActivated (object sender, EventArgs e)
	{
		MessageDialog messageDialog = new MessageDialog (
			this,
			DialogFlags.Modal,
			MessageType.Question,
			ButtonsType.YesNo,
			"¿Quieres eliminar el registro?"
		);
		messageDialog.Title = Title;
		ResponseType response = (ResponseType) messageDialog.Run ();
		messageDialog.Destroy ();

		if (response != ResponseType.Yes)
			return;

		TreeIter treeIter;
		treeView.Selection.GetSelected (out treeIter);
		object id = listStore.GetValue (treeIter, 0);
		string deleteSql = string.Format ("delete from categoria where id={0}", id);
		MySqlCommand mySqlCommand = mySqlConnection.CreateCommand ();
		mySqlCommand.CommandText = deleteSql;

		mySqlCommand.ExecuteNonQuery ();
	}
}
