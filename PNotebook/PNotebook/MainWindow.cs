using System;
using Gtk;

using PNotebook;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (): base (Gtk.WindowType.Toplevel)
	{
		Build ();

		ArtculoAction.Activated += delegate {
			addPage (new MyTreeView(),  "Artículo");
		};

		CategoraAction.Activated += delegate {
			addPage (new MyTreeView(), "Categoría");
		};

		notebook.SwitchPage += delegate {
			Console.WriteLine("notebook.CurrentPage = {0}", notebook.CurrentPage);
		};

	}

	private void addPage (Widget widget, string label) {
		HBox hBox = new HBox ();
		hBox.Add (new Label (label));
		Button button = new Button (new Image(Stock.Cancel, IconSize.Button) );
		hBox.Add (button);
		hBox.ShowAll ();
		notebook.AppendPage (widget,  hBox);

		button.Clicked += delegate {
			widget.Destroy();
		};
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
