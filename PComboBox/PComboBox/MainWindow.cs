using Gtk;
using System;

using SerpisAd;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		new ComboBoxHelper (comboBox, (ulong)7, "select id, nombre from categoria");
		//new ComboBoxHelper (comboBox, null, "select id, nombre from categoria");

		propertiesAction.Activated += delegate {
			Console.WriteLine("id={0}", comboBox.GetId());
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