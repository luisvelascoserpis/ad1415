using System;
using System.Collections.Generic;
using Gtk;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		//Esto no va
//		treeView.AppendColumn ("precio", new CellRendererText (), "text", 0);
//		ListStore listStore = new ListStore (typeof(decimal));
//
//		object value = new decimal (1.2);
//		listStore.AppendValues(value);

		//Opción 1
//		treeView.AppendColumn ("precio", new CellRendererText (), "text", 0);
//		ListStore listStore = new ListStore (typeof(string));
//
//		object value = new decimal (1.2).ToString ();
//		listStore.AppendValues (value);

		//Opción 2
		treeView.AppendColumn ("precio", new CellRendererText (), 
			new TreeCellDataFunc (delegate(TreeViewColumn tree_column, CellRenderer cell, 
		    	TreeModel tree_model, TreeIter iter) {
				CellRendererText cellRendererText = (CellRendererText)cell;
				object value = tree_model.GetValue(iter, 0);
				cellRendererText.Text = "date cuen " + value.ToString() + " ma o meno";

			})
		);
		List<Type> types = new List<Type> ();
		types.Add(typeof(decimal));
		ListStore listStore = new ListStore (types.ToArray());
		object data = new decimal (1.2);
		listStore.AppendValues (data);

		treeView.Model = listStore;
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
