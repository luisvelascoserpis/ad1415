using Gtk;
using System;
using System.Collections.Generic;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		List<Categoria> categorias = new List<Categoria> ();
		categorias.Add (new Categoria (1, "Uno"));
		categorias.Add (new Categoria (2, "Dos"));
		categorias.Add (new Categoria (3, "Tres"));
		categorias.Add (new Categoria (4, "Cuatro"));

		int categoriaId = -1;

		CellRendererText cellRendererText = new CellRendererText ();
		comboBox.PackStart (cellRendererText, false);
		comboBox.AddAttribute (cellRendererText, "text", 1);

		ListStore listStore = new ListStore (typeof(int), typeof(string));
		TreeIter initialTreeIter = listStore.AppendValues (0, "<sin asignar>");

		foreach (Categoria categoria in categorias)
			listStore.AppendValues (categoria.Id, categoria.Nombre);

		comboBox.Model = listStore;

		comboBox.SetActiveIter (initialTreeIter);

		TreeIter currentTreeIter;
		listStore.GetIterFirst (out currentTreeIter);
		do {
			if (categoriaId.Equals(listStore.GetValue(currentTreeIter, 0)) ){
				comboBox.SetActiveIter (currentTreeIter);
				break;
			}
		} while (listStore.IterNext (ref currentTreeIter));


		propertiesAction.Activated += delegate {
			TreeIter treeIter;
			bool activeIter = comboBox.GetActiveIter(out treeIter);
			object id = activeIter ? listStore.GetValue(treeIter, 0) : 0;
			Console.WriteLine("id={0}", id);

		};

	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

public class Categoria {
	public Categoria(int id, string nombre) {
		Id = id;
		Nombre = nombre;
	}
	public int Id { get; private set;}
	public string Nombre { get; private set;}

}