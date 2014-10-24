using Gtk;
using System;


namespace PNotebook
{
	public class MyTreeView : TreeView
	{
		public MyTreeView ()
		{
			AppendColumn ("id", new CellRendererText (), "text", 0);
			AppendColumn ("nombre", new CellRendererText (), "text", 1);
			Model = new ListStore (typeof(long), typeof(string));

			((ListStore)Model).AppendValues (1L, "Uno");
			((ListStore)Model).AppendValues (2L, "Dos");
			((ListStore)Model).AppendValues (3L, "Tres");
			Visible = true;
		}
	}
}

